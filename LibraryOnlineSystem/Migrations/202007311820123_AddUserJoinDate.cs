namespace LibraryOnlineSystem.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddUserJoinDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "JoinDate", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "JoinDate");
        }
    }
}
