using System;

namespace LibraryOnlineSystem.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public string NewsTitle { get; set; }

        public String NewsAuthor { get; set; }
        public string NewsContent { get; set; }
        public bool IsPinned { get; set; }
        public bool DisplayOnNews { get; set; }
        public DateTime NewsPublicationDate { get; set; }
    }
}