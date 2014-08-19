namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GiftWishListManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gifts", "WishListId", "dbo.WishLists");
            DropIndex("dbo.Gifts", new[] { "WishListId" });
            CreateTable(
                "dbo.WishListGifts",
                c => new
                    {
                        WishList_Id = c.Int(nullable: false),
                        Gift_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WishList_Id, t.Gift_Id })
                .ForeignKey("dbo.WishLists", t => t.WishList_Id, cascadeDelete: true)
                .ForeignKey("dbo.Gifts", t => t.Gift_Id, cascadeDelete: true)
                .Index(t => t.WishList_Id)
                .Index(t => t.Gift_Id);
            
            DropColumn("dbo.Gifts", "WishListId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gifts", "WishListId", c => c.Int(nullable: false));
            DropForeignKey("dbo.WishListGifts", "Gift_Id", "dbo.Gifts");
            DropForeignKey("dbo.WishListGifts", "WishList_Id", "dbo.WishLists");
            DropIndex("dbo.WishListGifts", new[] { "Gift_Id" });
            DropIndex("dbo.WishListGifts", new[] { "WishList_Id" });
            DropTable("dbo.WishListGifts");
            CreateIndex("dbo.Gifts", "WishListId");
            AddForeignKey("dbo.Gifts", "WishListId", "dbo.WishLists", "Id", cascadeDelete: true);
        }
    }
}
