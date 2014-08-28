namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VotesChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagGifts", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagGifts", "Gift_Id", "dbo.Gifts");
            DropForeignKey("dbo.Views", "WishListId", "dbo.WishLists");
            DropIndex("dbo.Views", new[] { "WishListId" });
            DropIndex("dbo.TagGifts", new[] { "Tag_Id" });
            DropIndex("dbo.TagGifts", new[] { "Gift_Id" });
            RenameColumn(table: "dbo.Views", name: "WishListId", newName: "WishList_Id");
            AddColumn("dbo.Gifts", "ViewsCount", c => c.Int(nullable: false));
            AlterColumn("dbo.Views", "WishList_Id", c => c.Int());
            CreateIndex("dbo.Views", "WishList_Id");
            AddForeignKey("dbo.Views", "WishList_Id", "dbo.WishLists", "Id");
            DropTable("dbo.Tags");
            DropTable("dbo.TagGifts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagGifts",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Gift_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Gift_Id });
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Views", "WishList_Id", "dbo.WishLists");
            DropIndex("dbo.Views", new[] { "WishList_Id" });
            AlterColumn("dbo.Views", "WishList_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Gifts", "ViewsCount");
            RenameColumn(table: "dbo.Views", name: "WishList_Id", newName: "WishListId");
            CreateIndex("dbo.TagGifts", "Gift_Id");
            CreateIndex("dbo.TagGifts", "Tag_Id");
            CreateIndex("dbo.Views", "WishListId");
            AddForeignKey("dbo.Views", "WishListId", "dbo.WishLists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagGifts", "Gift_Id", "dbo.Gifts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagGifts", "Tag_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
