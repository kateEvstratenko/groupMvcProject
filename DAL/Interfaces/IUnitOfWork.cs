using DAL.Models;
using Microsoft.AspNet.Identity;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        UserManager<User, int> UserManager { get; set; }
        RoleManager<CustomRole, int> RolesManager { get; set; }
        IRepository<User> UserRepository { get; }
        IRepository<Friend> FriendRepository { get; }
        IRepository<WishList> WishListRepository { get; }
        IRepository<Gift> GiftRepository { get;}
        IRepository<Tag> TagRepository { get; }
        IRepository<View> ViewRepository { get; }
        IRepository<Vote> VoteRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Like> LikeRepository { get; }
    }
}
