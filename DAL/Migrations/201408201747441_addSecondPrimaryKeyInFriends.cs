namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSecondPrimaryKeyInFriends : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Friends");
            AddPrimaryKey("dbo.Friends", new[] { "Id", "UserId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Friends");
            AddPrimaryKey("dbo.Friends", "Id");
        }
    }
}
