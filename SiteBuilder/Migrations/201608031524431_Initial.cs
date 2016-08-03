namespace SiteBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "SiteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Pages", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pages", "Name", c => c.Int(nullable: false));
            DropColumn("dbo.Pages", "SiteId");
        }
    }
}
