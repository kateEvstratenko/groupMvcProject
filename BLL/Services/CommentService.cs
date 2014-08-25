using System;
using System.Collections.Generic;
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

        public void Create(DomainComment domainComment)
        {
            var comment = Mapper.Map<Comment>(domainComment);
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
            var domainComments = comments.Select(Mapper.Map<Comment, DomainComment>).AsQueryable();
            return domainComments;
        } 
    }
}
