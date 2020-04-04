using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryOnlineSystem.Models
{
    [Table("CommentReply")]//Create CommentReply
    public class CommentReply
    {
        [Key]
        public virtual int CommentReplyID { get; set; }
        public virtual int CommentID { get; set; }
        public virtual string AuthorID { get; set; }
        public virtual int PersonID { get; set; }
        public virtual int BookID { get; set; }//added this
        public virtual int PostID { get; set; }
        public virtual string Content { get; set; }
    }
}