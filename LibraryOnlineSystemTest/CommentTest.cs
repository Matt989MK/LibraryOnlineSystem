using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryOnlineSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryOnlineSystemTest
{
    [TestClass]
   public class CommentTest
    {
        
        [TestMethod]
        public void AuthorIdPropertyOk()
        {

            //create an instance of the class we want to create
            Comment AComment = new Comment();
            //create some test data to assign to the property
            string TestData = "122";
            //assign the data to the property
            AComment.AuthorId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AComment.AuthorId, TestData);
        }

        [TestMethod]
        public void CommentIdPropertyOk()
        {
            //create an instance of the class we want to create
            Comment AComment = new Comment();
            //create some test data to assign to the property
            Int32 TestData = 11;
            //assign the data to the property
            AComment.CommentId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AComment.CommentId, TestData);
        }

        [TestMethod]
        public void PersonIdPropertyOk()
        {
            //create an instance of the class we want to create
            Comment AComment = new Comment();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            AComment.PersonId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AComment.PersonId, TestData);
        }

        [TestMethod]
        public void BookIdPropertyOk()
        {
            //create an instance of the class we want to create
            Comment AComment = new Comment();
            //create some test data to assign to the property
            Int32 TestData = 11;
            //assign the data to the property
            AComment.BookId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AComment.BookId, TestData);
        }

        [TestMethod]
        public void PostIdPropertyOk()
        {
            //create an instance of the class we want to create
            Comment AComment = new Comment();
            //create some test data to assign to the property
            Int32 TestData = 11;
            //assign the data to the property
            AComment.PostId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AComment.PostId, TestData);
        }

        [TestMethod]
        public void ContentPropertyOk()
        {
            //create an instance of the class we want to create
            Comment AComment = new Comment();
            //create some test data to assign to the property
            string TestData = "this is a test";
            //assign the data to the property
            AComment.Content = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AComment.Content, TestData);
        }

        [TestMethod]
        public void UserRatingPropertyOk()
        {
            //create an instance of the class we want to create
            Comment AComment = new Comment();
            //create some test data to assign to the property
            float TestData = 12;
            //assign the data to the property
            AComment.UserRating = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AComment.UserRating, TestData);
        }


    }

}