using System.Linq;
using AutoMapper;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class UserService: BaseService
    {
        public UserService(IUnitOfWork uow) : base(uow) { }

        public void Create(DomainUser domainUser)
        {
            var user = Mapper.Map<User>(domainUser);
            Uow.UserRepository.Insert(user);   
            Uow.Commit();
        }

        public void Delete(int id)
        {
            Uow.UserRepository.Delete(id);
            Uow.Commit();
        }

        public void Update(DomainUser domainUser)
        {
            var user = Mapper.Map<User>(domainUser);
            Uow.UserRepository.Update(user);
            Uow.Commit();
        }

        public DomainUser Get(int id)
        {
            var user = Uow.UserRepository.Get(id);
            var domainUser = Mapper.Map<DomainUser>(user);
            return domainUser;
        }

        public IQueryable<DomainUser> GetAll()
        {
            var users = Uow.UserRepository.GetAll();
            var domainUsers = Mapper.Map<IQueryable<DomainUser>>(users);
            return domainUsers;
        } 
    }
}
