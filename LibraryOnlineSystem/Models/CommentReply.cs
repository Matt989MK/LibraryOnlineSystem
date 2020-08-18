using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryOnlineSystem.Models
{
    [Table("CommentReply")]//Create CommentReply
    public class CommentReply : Icomments
    {
        [Key]
        public virtual int CommentReplyID { get; set; }
        public virtual int CommentID { get; set; }
        public virtual string AuthorID { get; set; }
        public virtual int PersonID { get; set; }
        public virtual int BookID { get; set; }//added this
        public virtual int PostID { get; set; }
        public virtual string Content { get; set; }

        public string GetContent()
        {
            return Content;
        }
        public int GetCommentId()
        {
            return CommentID;
        }

    }
}