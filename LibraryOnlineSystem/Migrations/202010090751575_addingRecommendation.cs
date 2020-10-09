namespace LibraryOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingRecommendation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        bookBase = c.Int(nullable: false),
                        bookRecommended = c.Int(nullable: false),
                        creation_Date = c.DateTime(nullable: false),
                        confidence = c.Single(nullable: false),
                        support = c.Single(nullable: false),
                        lift = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recommendations");
        }
    }
}
