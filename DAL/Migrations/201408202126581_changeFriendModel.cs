namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeFriendModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "FriendId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "FriendId");
        }
    }
}
