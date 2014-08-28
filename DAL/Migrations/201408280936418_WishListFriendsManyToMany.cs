namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WishListFriendsManyToMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendWishLists",
                c => new
                    {
                        Friend_Id = c.Int(nullable: false),
                        WishList_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Friend_Id, t.WishList_Id })
                .ForeignKey("dbo.Friends", t => t.Friend_Id, cascadeDelete: true)
                .ForeignKey("dbo.WishLists", t => t.WishList_Id, cascadeDelete: true)
                .Index(t => t.Friend_Id)
                .Index(t => t.WishList_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendWishLists", "WishList_Id", "dbo.WishLists");
            DropForeignKey("dbo.FriendWishLists", "Friend_Id", "dbo.Friends");
            DropIndex("dbo.FriendWishLists", new[] { "WishList_Id" });
            DropIndex("dbo.FriendWishLists", new[] { "Friend_Id" });
            DropTable("dbo.FriendWishLists");
        }
    }
}
