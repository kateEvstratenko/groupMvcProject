using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using WishList.ViewModels;

namespace WishList.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/
        private readonly ICommentService commentService;

        public CommentController(ICommentService iCommentService)
        {
            commentService = iCommentService;
        }
        public ActionResult DisplayComments(int id)
        {
            var comments = commentService.GetAll().Where(c => c.GiftId == id).Select(Mapper.Map<DomainComment, CommentViewModel>).AsEnumerable();
            return PartialView("_DisplayCommentsPartial", comments);
        }
        [HttpPost]
        public ActionResult CreateComment(CreateCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                commentService.Create(Mapper.Map<DomainComment>(model));
                return PartialView("_DisplayCommentsPartial", commentService.GetAll().Where(c => c.GiftId == model.GiftId)
                    .Select(Mapper.Map<DomainComment,CommentViewModel>).AsEnumerable());
            }
            ModelState.AddModelError("","invalid comment");
            return PartialView("_CreateCommentPartial");
        }

        public ActionResult CreateComment(int id)
        {
            return PartialView("_CreateCommentPartial", new CreateCommentViewModel
            {
                GiftId = id
            });
        }
    }
}
