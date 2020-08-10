using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryOnlineSystem.Models
{
    [Table("Comments")]
    public class Comment : Icomments
    {
        [Key]
        public virtual int CommentId { get; set; }
        public virtual string AuthorId { get; set; }
        public virtual int PersonId { get; set; }
        public virtual int BookId { get; set; }
        public virtual int PostId { get; set; }
        public virtual string Content { get; set; }

        public virtual float UserRating { get; set; }
        public virtual List<CommentReply> CommentReply { get; set; }
        public virtual bool IsBlocked { get; set; }
        public string GetContent()
        {
            return Content;
        }
        public int GetCommentId()
        {
            return CommentId;
        }
    }
}