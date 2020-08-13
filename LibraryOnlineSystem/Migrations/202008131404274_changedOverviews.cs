namespace LibraryOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedOverviews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookReviews", "BookId", "dbo.Books");
            DropIndex("dbo.BookReviews", new[] { "BookId" });
            AddColumn("dbo.Books", "BookReviews", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "BookReviews");
            CreateIndex("dbo.BookReviews", "BookId");
            AddForeignKey("dbo.BookReviews", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
        }
    }
}
