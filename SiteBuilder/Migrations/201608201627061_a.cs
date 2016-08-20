namespace SiteBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AchievementTypes", "Image", c => c.String());
            DropColumn("dbo.Achievements", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Achievements", "Image", c => c.String());
            DropColumn("dbo.AchievementTypes", "Image");
        }
    }
}
