using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnlineSystem.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public string NewsContent { get; set; }
        public DateTime NewsPublicationDate { get; set; }
    }
}