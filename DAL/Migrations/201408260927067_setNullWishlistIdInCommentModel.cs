namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setNullWishlistIdInCommentModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "WishListId", "dbo.WishLists");
            DropIndex("dbo.Comments", new[] { "WishListId" });
            AlterColumn("dbo.Comments", "WishListId", c => c.Int());
            CreateIndex("dbo.Comments", "WishListId");
            AddForeignKey("dbo.Comments", "WishListId", "dbo.WishLists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "WishListId", "dbo.WishLists");
            DropIndex("dbo.Comments", new[] { "WishListId" });
            AlterColumn("dbo.Comments", "WishListId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "WishListId");
            AddForeignKey("dbo.Comments", "WishListId", "dbo.WishLists", "Id", cascadeDelete: true);
        }
    }
}
