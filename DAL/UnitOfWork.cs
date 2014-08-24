using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class UnitOfWork : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUnitOfWork
    {
        public UnitOfWork()
            : base("DefaultConnection")
        {
            UserManager = new UserManager<User, int>(new UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(this));
            RolesManager = new RoleManager<CustomRole, int>(new RoleStore<CustomRole, int, CustomUserRole>(this));
        }

        public RoleManager<CustomRole,int>  RolesManager { get; set; }
        public UserManager<User,int> UserManager { get; set; }
        private Repository<User> userRepository;
        private Repository<Friend> friendRepository;
        private Repository<WishList> wishListRepository;
        private Repository<Gift> giftRepository;
        private Repository<Tag> tagRepository;
        private Repository<View> viewRepository;
        private Repository<Vote> voteRepository;
        private Repository<Comment> commentRepository;
        private Repository<Like> likeRepository; 


        public DbSet<Friend> Friends { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        public IRepository<User> UserRepository
        {
            get { return userRepository ?? (userRepository = new Repository<User>(Users, this)); }
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

        public IRepository<Like> LikeRepository
        {
            get { return likeRepository ?? (likeRepository = new Repository<Like>(Likes, this)); }
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