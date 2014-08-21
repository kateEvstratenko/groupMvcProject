namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noRequiredLink : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WishLists", "Link", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WishLists", "Link", c => c.String(nullable: false));
        }
    }
}
