namespace SiteBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sites", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Sites", "UserId");
            AddForeignKey("dbo.Sites", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sites", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Sites", new[] { "UserId" });
            AlterColumn("dbo.Sites", "UserId", c => c.String(nullable: false));
        }
    }
}
