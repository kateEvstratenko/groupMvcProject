using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using DAL.Interfaces;
using DAL.Models;

namespace DAL
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public UnitOfWork()
            : base("DefaultConnection")
        {

        }

        private Repository<User> userRepository;
        private Repository<Role> roleRepository;
        private Repository<Friend> friendRepository;
        private Repository<WishList> wishListRepository;
        private Repository<Gift> giftRepository;
        private Repository<Tag> tagRepository;
        private Repository<View> viewRepository;
        private Repository<Vote> voteRepository;
        private Repository<Comment> commentRepository;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public IRepository<User> UserRepository
        {
            get { return userRepository ?? (userRepository = new Repository<User>(Users, this)); }
        }
        public IRepository<Role> RoleRepository
        {
            get { return roleRepository ?? (roleRepository = new Repository<Role>(Roles, this)); }
        }
        public IRepository<Friend> FriendRepository
        {
            get { return friendRepository ?? (friendRepository = new Repository<Friend>(Friends, this)); }
        }
        public IRepository<WishList> WishListRepository
        {
            get { return wishListRepository ?? (wishListRepository = new Repository<WishList>(WishLists, this)); }
        }
        public IRepository<Gift> GiftRepository
        {
            get { return giftRepository ?? (giftRepository = new Repository<Gift>(Gifts, this)); }
        }
        public IRepository<Tag> TagRepository
        {
            get { return tagRepository ?? (tagRepository = new Repository<Tag>(Tags, this)); }
        }
        public IRepository<View> ViewRepository
        {
            get { return viewRepository ?? (viewRepository = new Repository<View>(Views, this)); }
        }
        public IRepository<Vote> VoteRepository
        {
            get { return voteRepository ?? (voteRepository = new Repository<Vote>(Votes, this)); }
        }
        public IRepository<Comment> CommentRepository
        {
            get { return commentRepository ?? (commentRepository = new Repository<Comment>(Comments, this)); }
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}