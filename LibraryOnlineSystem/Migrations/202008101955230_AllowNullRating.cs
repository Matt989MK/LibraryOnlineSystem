namespace LibraryOnlineSystem.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AllowNullRating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "UserRating", c => c.Single());
        }

        public override void Down()
        {
            AlterColumn("dbo.Comments", "UserRating", c => c.Single(nullable: false));
        }
    }
}
