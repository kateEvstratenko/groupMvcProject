using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class TagService: BaseService, ITagService
    {
        public TagService(IUnitOfWork uow) : base(uow) { }

        public void Create(DomainTag domainTag)
        {
            var tag = Mapper.Map<Tag>(domainTag);
            Uow.TagRepository.Insert(tag);   
            Uow.Commit();
        }

        public void Delete(int id)
        {
            Uow.TagRepository.Delete(id);
            Uow.Commit();
        }

        public void Update(DomainTag domainTag)
        {
            var tag = Mapper.Map<Tag>(domainTag);
            Uow.TagRepository.Update(tag);
            Uow.Commit();
        }

        public DomainTag Get(int id)
        {
            var tag = Uow.TagRepository.Get(id);
            var domainTag = Mapper.Map<DomainTag>(tag);
            return domainTag;
        }

        public IQueryable<DomainTag> GetAll()
        {
            var tags = Uow.TagRepository.GetAll();
            var domainTags = Mapper.Map<IQueryable<DomainTag>>(tags);
            return domainTags;
        } 
    }
}
