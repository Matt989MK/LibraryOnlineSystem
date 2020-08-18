namespace LibraryOnlineSystem.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Surname = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Books",
                c => new
                {
                    BookId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 40),
                    Genre = c.Int(nullable: false),
                    DateOfPublication = c.DateTime(nullable: false),
                    Overview = c.String(nullable: false, maxLength: 200),
                    Publisher = c.String(nullable: false, maxLength: 40),
                    Rating = c.Single(nullable: false),
                    ImageData = c.Binary(),
                    ImageMimeType = c.String(),
                    Link = c.String(),
                    Author_Id = c.Int(),
                })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.Author_Id)
                .Index(t => t.Author_Id);

            CreateTable(
                "dbo.BookCodes",
                c => new
                {
                    BookCodeId = c.Int(nullable: false, identity: true),
                    BookId = c.Int(nullable: false),
                    BookSerialNumber = c.String(nullable: false),
                    IsInLibrary = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.BookCodeId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);

            CreateTable(
                "dbo.BookReserves",
                c => new
                {
                    BookReserveId = c.Int(nullable: false, identity: true),
                    BookCodeId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    ReservationRequestTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.BookReserveId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.BookCodes", t => t.BookCodeId, cascadeDelete: true)
                .Index(t => t.BookCodeId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 30),
                    Surname = c.String(nullable: false, maxLength: 30),
                    DateOfBirth = c.DateTime(nullable: false),
                    Email = c.String(nullable: false),
                    HouseNo = c.String(nullable: false),
                    ZipCode = c.String(nullable: false),
                    UserRole = c.String(nullable: false),
                    IsBanned = c.Boolean(),
                    Password = c.String(nullable: false),
                })
                .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.Bookings",
                c => new
                {
                    BookingId = c.Int(nullable: false, identity: true),
                    BookCodeId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    BookId = c.Int(nullable: false),
                    DateCreated = c.DateTime(nullable: false),
                    DateReturned = c.DateTime(),
                })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);

            CreateTable(
                "dbo.PaymentLibraries",
                c => new
                {
                    PaymentLibraryId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    DatePaid = c.DateTime(),
                    Amount = c.Int(nullable: false),
                    Status = c.String(),
                    BookingId = c.Int(nullable: false),
                    guId = c.String(),
                })
                .PrimaryKey(t => t.PaymentLibraryId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.BookReviews",
                c => new
                {
                    BookReviewId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    BookId = c.Int(nullable: false),
                    Content = c.String(),
                    DatePosted = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.BookReviewId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);

            CreateTable(
                "dbo.Comments",
                c => new
                {
                    CommentId = c.Int(nullable: false, identity: true),
                    AuthorId = c.String(),
                    PersonId = c.Int(nullable: false),
                    BookId = c.Int(nullable: false),
                    PostId = c.Int(nullable: false),
                    Content = c.String(),
                    UserRating = c.Single(nullable: false),
                    IsBlocked = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);

            CreateTable(
                "dbo.CommentReply",
                c => new
                {
                    CommentReplyID = c.Int(nullable: false, identity: true),
                    CommentID = c.Int(nullable: false),
                    AuthorID = c.String(),
                    PersonID = c.Int(nullable: false),
                    BookID = c.Int(nullable: false),
                    PostID = c.Int(nullable: false),
                    Content = c.String(),
                })
                .PrimaryKey(t => t.CommentReplyID)
                .ForeignKey("dbo.Comments", t => t.CommentID, cascadeDelete: true)
                .Index(t => t.CommentID);

            CreateTable(
                "dbo.BookAuthors",
                c => new
                {
                    BookAuthorsId = c.Int(nullable: false, identity: true),
                    BookId = c.Int(nullable: false),
                    AuthorId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.BookAuthorsId)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.AuthorId);

            CreateTable(
                "dbo.LibraryRegulations",
                c => new
                {
                    LibraryRegulationsId = c.Int(nullable: false, identity: true),
                    BorrowTime = c.Int(nullable: false),
                    Fine = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.LibraryRegulationsId);

            CreateTable(
                "dbo.Stocks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BookId = c.Int(nullable: false),
                    Quantity = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Tokens",
                c => new
                {
                    TokenId = c.String(nullable: false, maxLength: 128),
                    Email = c.String(),
                })
                .PrimaryKey(t => t.TokenId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.BookAuthors", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Books", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.Comments", "BookId", "dbo.Books");
            DropForeignKey("dbo.CommentReply", "CommentID", "dbo.Comments");
            DropForeignKey("dbo.BookReviews", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookCodes", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookReserves", "BookCodeId", "dbo.BookCodes");
            DropForeignKey("dbo.BookReviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.BookReserves", "UserId", "dbo.Users");
            DropForeignKey("dbo.PaymentLibraries", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "BookId", "dbo.Books");
            DropIndex("dbo.BookAuthors", new[] { "AuthorId" });
            DropIndex("dbo.BookAuthors", new[] { "BookId" });
            DropIndex("dbo.CommentReply", new[] { "CommentID" });
            DropIndex("dbo.Comments", new[] { "BookId" });
            DropIndex("dbo.BookReviews", new[] { "BookId" });
            DropIndex("dbo.BookReviews", new[] { "UserId" });
            DropIndex("dbo.PaymentLibraries", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "BookId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.BookReserves", new[] { "UserId" });
            DropIndex("dbo.BookReserves", new[] { "BookCodeId" });
            DropIndex("dbo.BookCodes", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "Author_Id" });
            DropTable("dbo.Tokens");
            DropTable("dbo.Stocks");
            DropTable("dbo.LibraryRegulations");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.CommentReply");
            DropTable("dbo.Comments");
            DropTable("dbo.BookReviews");
            DropTable("dbo.PaymentLibraries");
            DropTable("dbo.Bookings");
            DropTable("dbo.Users");
            DropTable("dbo.BookReserves");
            DropTable("dbo.BookCodes");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
