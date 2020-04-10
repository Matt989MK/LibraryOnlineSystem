using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryOnlineSystem.Models;
namespace LibraryOnlineSystemTest
{
    [TestClass]

    public class BookCodeTest
    {
        [TestMethod]
        public void BookCodeIdPropertyOk()
        {

            //create an instance of the class we want to create
            BookCode bookCode= new BookCode();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            bookCode.BookCodeId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(bookCode.BookCodeId, TestData);
        }
        [TestMethod]
        public void BookIdPropertyOk()
        {

            //create an instance of the class we want to create
            BookCode bookCode = new BookCode();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            bookCode.BookId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(bookCode.BookId, TestData);
        }
        [TestMethod]
        public void BookSerialNumberPropertyOk()
        {

            //create an instance of the class we want to create
            BookCode bookCode = new BookCode();
            //create some test data to assign to the property
            string TestData = "33353";
            //assign the data to the property
            bookCode.BookSerialNumber = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(bookCode.BookSerialNumber, TestData);
        }
        [TestMethod]
        public void IsInLibraryPropertyOk()
        {

            //create an instance of the class we want to create
            BookCode bookCode = new BookCode();
            //create some test data to assign to the property
            bool TestData = true;
            //assign the data to the property
            bookCode.IsInLibrary = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(bookCode.IsInLibrary, TestData);
        }
    }
}