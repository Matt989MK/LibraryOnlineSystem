using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOnlineSystem.Models
{
    public interface Icomments
    {
         string GetContent();
         int GetCommentId();
         
    }
}