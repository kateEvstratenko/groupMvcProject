﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Models;
using Microsoft.AspNet.Identity;
using WishList.ViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class CommentController : BaseController
    {
        //
        // GET: /Comment/
        private readonly ICommentService _commentService;

        public CommentController(IUserService iUserService, ICommentService iCommentService): base(iUserService)
        {
            _commentService = iCommentService;
        }
        [AllowAnonymous]
        public ActionResult GetPopularComment(int giftId)
        {
            var comments = _commentService.GetAll()
                .Where(x => x.GiftId == giftId)
                .OrderByDescending(x => _commentService.GetLikesCount(x.Id))
                .Take(1)
                .ToList();
            if (comments.Count < 1)
            {
                return new EmptyResult();
            }
            var model = Mapper.Map<IEnumerable<CommentViewModel>>(comments);

            return View("_DisplayPopularCommentsPartial", model);
        }

        [AllowAnonymous]
        public ActionResult DisplayComments(int id, string kind)
        {
            if (kind == "gift")
            {
                return PartialView("_DisplayCommentsPartial",
                    _commentService.GetAll()
                        .Where(c => c.GiftId == id)
                        .Select(Mapper.Map<DomainComment, CommentViewModel>)
                        .AsEnumerable());
            }

            return PartialView("_DisplayCommentsPartial",
               _commentService.GetAll()
                   .Where(c => c.WishListId == id)
                   .Select(Mapper.Map<DomainComment, CommentViewModel>)
                   .AsEnumerable());
        }

        [HttpPost]
        public ActionResult CreateComment(CreateCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.GiftId != 0)
                {
                    _commentService.Create(Mapper.Map<DomainComment>(model), CurrentUser.Id,
                        "gift");

                    var comments = _commentService.GetAll().Where(c => c.GiftId == model.GiftId).Include(c => c.User).ToList();
                    var commentViewModel = Mapper.Map<IEnumerable<CommentViewModel>>(comments);
                    return PartialView("_DisplayCommentsPartial", commentViewModel);
                }
                else
                {
                    _commentService.Create(Mapper.Map<DomainComment>(model), CurrentUser.Id,
                        "wishList");
                    return PartialView("_DisplayCommentsPartial",
                        _commentService.GetAll().Where(c => c.WishListId == model.WishListId)
                            .Select(Mapper.Map<DomainComment, CommentViewModel>).AsEnumerable());
                }
            }
            ModelState.AddModelError("", "invalid comment");
            return PartialView("_CreateCommentPartial");
        }

        public ActionResult CreateComment(int id, string kind)
        {
            if (kind == "gift")
            {
                return PartialView("_CreateCommentPartial", new CreateCommentViewModel
                {
                    GiftId = id
                });
            }

            return PartialView("_CreateCommentPartial", new CreateCommentViewModel
                    {
                        WishListId = id
                    });
        }
        [HttpPost]
        public ActionResult DeleteComment(int id)
        {
            _commentService.Delete(id);
            return PartialView("_DeleteCommentSuccess");
        }

        [HttpPost]
        public ActionResult UpdateComment(string id)
        {
            var comment = Mapper.Map<CommentViewModel>(_commentService.Get(Int32.Parse(id)));
            return PartialView("_UpdateCommentPartial", comment);
        }
        [HttpPost]
        public ActionResult UpdateComment(CommentViewModel model)
        {
            _commentService.Update(Mapper.Map<DomainComment>(model));
            return PartialView("_DisplaySingleCommentPartial",model);
        }

        [AllowAnonymous]
        public ActionResult GetCommentLikesCount(int id)
        {
            return PartialView("_CommentLikesCount",_commentService.GetLikesCount(id));
        }

        [HttpPost]
        [Authorize]
        public int ChangeLikesCount(string id)
        {
            return _commentService.ChangeLikesCount(id, CurrentUser.Id);
        }

        [HttpPost]
        public bool EnableLikes()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
