using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        IRepository<User> UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<Friend> FriendRepository { get; }
        IRepository<WishList> WishListRepository { get; }
        IRepository<Gift> GiftRepository { get;}
        IRepository<Tag> TagRepository { get; }
        IRepository<View> ViewRepository { get; }
        IRepository<Vote> VoteRepository { get; }
        IRepository<Comment> CommentRepository { get; }
    }
}
