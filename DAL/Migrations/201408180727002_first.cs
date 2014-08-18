namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 256),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        WishListId = c.Int(nullable: false),
                        GiftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gifts", t => t.GiftId, cascadeDelete: true)
                .ForeignKey("dbo.WishLists", t => t.WishListId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WishListId)
                .Index(t => t.GiftId);
            
            CreateTable(
                "dbo.Gifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Logo = c.String(nullable: false),
                        About = c.String(nullable: false, maxLength: 256),
                        LikesCount = c.Int(nullable: false),
                        WishListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WishLists", t => t.WishListId, cascadeDelete: false)
                .Index(t => t.WishListId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Link = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        Birthday = c.DateTime(nullable: false),
                        Avatar = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Views",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GiftId = c.Int(nullable: false),
                        WishListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WishLists", t => t.WishListId, cascadeDelete: true)
                .Index(t => t.WishListId);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GiftId = c.Int(nullable: false),
                        WishListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WishLists", t => t.WishListId, cascadeDelete: true)
                .Index(t => t.WishListId);
            
            CreateTable(
                "dbo.TagGifts",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Gift_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Gift_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Gifts", t => t.Gift_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Gift_Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "WishListId", "dbo.WishLists");
            DropForeignKey("dbo.Views", "WishListId", "dbo.WishLists");
            DropForeignKey("dbo.WishLists", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Friends", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Gifts", "WishListId", "dbo.WishLists");
            DropForeignKey("dbo.Comments", "WishListId", "dbo.WishLists");
            DropForeignKey("dbo.TagGifts", "Gift_Id", "dbo.Gifts");
            DropForeignKey("dbo.TagGifts", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Comments", "GiftId", "dbo.Gifts");
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.TagGifts", new[] { "Gift_Id" });
            DropIndex("dbo.TagGifts", new[] { "Tag_Id" });
            DropIndex("dbo.Votes", new[] { "WishListId" });
            DropIndex("dbo.Views", new[] { "WishListId" });
            DropIndex("dbo.Friends", new[] { "UserId" });
            DropIndex("dbo.WishLists", new[] { "UserId" });
            DropIndex("dbo.Gifts", new[] { "WishListId" });
            DropIndex("dbo.Comments", new[] { "GiftId" });
            DropIndex("dbo.Comments", new[] { "WishListId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.RoleUsers");
            DropTable("dbo.TagGifts");
            DropTable("dbo.Votes");
            DropTable("dbo.Views");
            DropTable("dbo.Roles");
            DropTable("dbo.Friends");
            DropTable("dbo.Users");
            DropTable("dbo.WishLists");
            DropTable("dbo.Tags");
            DropTable("dbo.Gifts");
            DropTable("dbo.Comments");
        }
    }
}
