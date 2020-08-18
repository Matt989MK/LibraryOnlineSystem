namespace LibraryOnlineSystem.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addedNews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                {
                    NewsId = c.Int(nullable: false, identity: true),
                    NewsTitle = c.String(),
                    NewsAuthor = c.String(),
                    NewsContent = c.String(),
                    IsPinned = c.Boolean(nullable: false),
                    DisplayOnNews = c.Boolean(nullable: false),
                    NewsPublicationDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.NewsId);

        }

        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
