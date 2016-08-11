namespace SiteBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "PageNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "PageNumber");
        }
    }
}
