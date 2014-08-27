using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class CommentService : BaseService, ICommentService
    {
        public CommentService(IUnitOfWork uow) : base(uow) { }

        public void Create(DomainComment domainComment, int userId, string kind)
        {
            
            domainComment.Date = DateTime.Now;
            domainComment.UserId = userId;
            var comment = Mapper.Map<Comment>(domainComment);
            /*switch (kind)
            {
                case "gift":
                {
                    comment.WishListId = null;
                    break;
                }
                case "wishList":
                {
                    comment.GiftId = null;
                    break;
                }
            }*/
            
            Uow.CommentRepository.Insert(comment);
            Uow.Commit();
        }

        public void Delete(int id)
        {
            Uow.CommentRepository.Delete(id);
            Uow.Commit();
        }

        public void Update(DomainComment domainComment)
        {
            var comment = Mapper.Map<Comment>(domainComment);
            Uow.CommentRepository.Update(comment);
            Uow.Commit();
        }

        public DomainComment Get(int id)
        {
            var comment = Uow.CommentRepository.Get(id);
            var domainComment = Mapper.Map<DomainComment>(comment);
            return domainComment;
        }

        public IQueryable<DomainComment> GetAll()
        {
            var comments = Uow.CommentRepository.GetAll();
            var domainComments = comments.Include(c => c.User).Select(Mapper.Map<Comment, DomainComment>).AsQueryable();
            return domainComments;
        } 
    }
}
