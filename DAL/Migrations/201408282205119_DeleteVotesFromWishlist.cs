namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteVotesFromWishlist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Views", "WishList_Id", "dbo.WishLists");
            DropIndex("dbo.Views", new[] { "WishList_Id" });
            DropColumn("dbo.Views", "WishList_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Views", "WishList_Id", c => c.Int());
            CreateIndex("dbo.Views", "WishList_Id");
            AddForeignKey("dbo.Views", "WishList_Id", "dbo.WishLists", "Id");
        }
    }
}
