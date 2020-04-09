using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryOnlineSystem;
namespace LibraryOnlineSystem.UnitTests
{
    [TestClass]
    class BookTests
    {
        [TestMethod]
        public void Can_Add_New_Book()
        {
            Book book = new Book();
        }
    }
}
