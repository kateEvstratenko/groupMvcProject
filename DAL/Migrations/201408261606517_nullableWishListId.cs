namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableWishListId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "GiftId", "dbo.Gifts");
            DropIndex("dbo.Comments", new[] { "GiftId" });
            AlterColumn("dbo.Comments", "GiftId", c => c.Int());
            CreateIndex("dbo.Comments", "GiftId");
            AddForeignKey("dbo.Comments", "GiftId", "dbo.Gifts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "GiftId", "dbo.Gifts");
            DropIndex("dbo.Comments", new[] { "GiftId" });
            AlterColumn("dbo.Comments", "GiftId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "GiftId");
            AddForeignKey("dbo.Comments", "GiftId", "dbo.Gifts", "Id", cascadeDelete: true);
        }
    }
}
