using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;
using WishList.ViewModels;

namespace WishList.Controllers
{
    public class CommentController : BaseController
    {
        //
        // GET: /Comment/
        private readonly ICommentService commentService;

        public CommentController(IUserService iUserService, ICommentService iCommentService): base(iUserService)
        {
            commentService = iCommentService;
        }

        public ActionResult DisplayComments(int id, string kind)
        {

            if (kind == "gift")
            {
                return PartialView("_DisplayCommentsPartial",
                    commentService.GetAll()
                        .Where(c => c.GiftId == id)
                        .Select(Mapper.Map<DomainComment, CommentViewModel>)
                        .AsEnumerable());
            }

            return PartialView("_DisplayCommentsPartial",
               commentService.GetAll()
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
                    commentService.Create(Mapper.Map<DomainComment>(model), CurrentUser.Id,
                        "gift");
                    return PartialView("_DisplayCommentsPartial",
                        commentService.GetAll().Where(c => c.GiftId == model.GiftId)
                            .Select(Mapper.Map<DomainComment, CommentViewModel>).AsEnumerable());
                }
                else
                {
                    commentService.Create(Mapper.Map<DomainComment>(model), CurrentUser.Id,
                        "wishList");
                    return PartialView("_DisplayCommentsPartial",
                        commentService.GetAll().Where(c => c.WishListId == model.WishListId)
                            .Select(Mapper.Map<DomainComment, CommentViewModel>).AsEnumerable());
                }
            }
            ModelState.AddModelError("", @"invalid comment");
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

    }
}
