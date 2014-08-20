namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setOnePrimaryKeyInFriends : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Friends");
            AddColumn("dbo.Friends", "FriendId", c => c.Int(nullable: false));
            AlterColumn("dbo.Friends", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Friends", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Friends");
            AlterColumn("dbo.Friends", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Friends", "FriendId");
            AddPrimaryKey("dbo.Friends", new[] { "Id", "UserId" });
        }
    }
}
