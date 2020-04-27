using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnlineSystem.Models
{
    public class BookAuthors
    {
        public virtual int BookAuthorsId { get; set; }
        public virtual int BookId { get; set; }
        public virtual int AuthorId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}