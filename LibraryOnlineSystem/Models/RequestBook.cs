
namespace LibraryOnlineSystem.Models
{
    public class RequestBook
    {
        public int RequestBookId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }

        public string Note { get; set; }
    }
}