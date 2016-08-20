namespace SiteBuilder.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SiteBuilder.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SiteBuilder.Models.ApplicationDbContext context)
        {
            context.TypeMenus.AddOrUpdate(c => c.Id,
                new TypeMenu() { Id = 1, Name = "horizontal" },
                new TypeMenu() { Id = 2, Name = "vertical" }
            );

            context.ContentTypes.AddOrUpdate(c => c.Id,
                new ContentType() { Id = 1, Name = "Image" },
                new ContentType() { Id = 2, Name = "Video" },
                new ContentType() { Id = 3, Name = "Markdown" }
            );

            context.Templates.AddOrUpdate(c => c.Id,
                new Template() { Id = 1, Name = "Template1", CountBlocks = 3 },
                new Template() { Id = 2, Name = "Template2", CountBlocks = 3 },
                new Template() { Id = 3, Name = "Template3", CountBlocks = 4 }
            );

            context.Tags.AddOrUpdate(c => c.Id,
                new Tag() { Id = 1, Name = "Auto" },
                new Tag() { Id = 2, Name = "Fashion" },
                new Tag() { Id = 3, Name = "Food" },
                new Tag() { Id = 4, Name = "Movie" },
                new Tag() { Id = 5, Name = "Science" },
                new Tag() { Id = 6, Name = "Technology" },
                new Tag() { Id = 7, Name = "Games" },
                new Tag() { Id = 8, Name = "Gadgets" },
                new Tag() { Id = 9, Name = "Films" },
                new Tag() { Id = 10, Name = "Photo" },
                new Tag() { Id = 11, Name = "Life" },
                new Tag() { Id = 12, Name = "Geek" },
                new Tag() { Id = 13, Name = "Electronic" },
                new Tag() { Id = 14, Name = "Work" },
                new Tag() { Id = 15, Name = "Funny" },
                new Tag() { Id = 16, Name = "Dogs" },
                new Tag() { Id = 17, Name = "Cats" },
                new Tag() { Id = 18, Name = "Books" },
                new Tag() { Id = 19, Name = "Nature" },
                new Tag() { Id = 20, Name = "People" }
            );

            context.AchievementTypes.AddOrUpdate(c => c.Id,
                new AchievementType() { Id = 1, Text = "First comment", Image = "https://cdn1.iconfinder.com/data/icons/hawcons/32/700405-icon-35-medal-64.png" },
                new AchievementType() { Id = 2, Text = "First vote", Image = "https://cdn1.iconfinder.com/data/icons/hawcons/32/700386-icon-17-medal-64.png" }
            );

            context.StyleTypes.AddOrUpdate(c => c.Id,
                new StyleType() { Id = 1, Name = "white" },
                new StyleType() { Id = 2, Name = "black" },
                new StyleType() { Id = 3, Name = "ginger" }
            );
        }
    }
}
