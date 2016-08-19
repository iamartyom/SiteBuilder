namespace SiteBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "UserId", c => c.Int(nullable: false));
        }
    }
}
