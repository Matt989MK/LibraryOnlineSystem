using LibraryOnlineSystem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryOnlineSystem
{
    public class Author
    {
        [Key]
        public int Id { get; set; }


        public string Name { get; set; }

        public string Surname { get; set; }

        public List<Book> BookList { get; set; }



        public void AddAuthor()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAuthor()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAuthor()
        {
            throw new System.NotImplementedException();
        }

        public void GetAuthorById()
        {
            throw new System.NotImplementedException();
        }

        public void GetAuthorList()
        {
            throw new System.NotImplementedException();
        }
    }
}