using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryOnlineSystem.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public virtual int CommentID { get; set; }
        public virtual string AuthorID { get; set; }
        public virtual int PersonID { get; set; }
        public virtual int BookID { get; set; }
        public virtual int PostID { get; set; }
        public virtual string Content { get; set; }
        public virtual float UserRating { get; set; }
        public virtual List<CommentReply> CommentReply { get; set; }
        public virtual bool isBlocked { get; set; }
    }
}