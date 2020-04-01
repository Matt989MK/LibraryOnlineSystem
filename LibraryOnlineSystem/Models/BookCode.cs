using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnlineSystem.Models
{
    public class BookCode
    {
        public int BookCodeId { get; set; }
        public int BookId { get; set; }
        public string BookSerialNumber { get; set; }
        public List<BookReserve> BookReserves { get; set; }
        public bool IsInLibrary { get; set; }

    }
}