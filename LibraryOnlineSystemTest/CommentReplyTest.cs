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
    public class CommentReplyTest
    {


        [TestMethod]
        public void InstanceOk()
        {
            //create an instance of the calss we want to create
            CommentReply AReply = new CommentReply();
            //test to see that it exists
            Assert.IsNotNull(AReply);
        }
        [TestMethod]
        public void CommentReplyIdPropertyOk()
        {
            //create an instance of the class we want to create 
            CommentReply AReply = new CommentReply();
            //create some test data to assign to the property
            int TestData = 1;
            AReply.CommentID = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AReply.CommentID, TestData);
        }
        [TestMethod]
        public void AuthorIdPropertyOk()
        {
            //create an instance of the class we want to create 
            CommentReply AReply = new CommentReply();
            //create some test data to assign to the property
            string TestData = "aaa";
            AReply.AuthorID = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AReply.AuthorID, TestData);
        }
        [TestMethod]
        public void PersonIdPropertyOk()
        {
            //create an instance of the class we want to create 
            CommentReply AReply = new CommentReply();
            //create some test data to assign to the property
            int TestData = 1;
            AReply.PersonID = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AReply.PersonID, TestData);
        }
        [TestMethod]
        public void BookIdPropertyOk()
        {
            //create an instance of the class we want to create 
            CommentReply AReply = new CommentReply();
            //create some test data to assign to the property
            int TestData = 10;
            AReply.BookID = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AReply.BookID, TestData);
        }

        [TestMethod]
        public void PostIdPropertyOk()
        {
            //create an instance of the class we want to create 
            CommentReply AReply = new CommentReply();
            //create some test data to assign to the property
            int TestData = 10;
            AReply.PostID = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AReply.PostID, TestData);
        }
        [TestMethod]
        public void ContentPropertyOk()
        {
            //create an instance of the class we want to create 
            CommentReply AReply = new CommentReply();
            //create some test data to assign to the property
            string TestData = "aaa";
            AReply.Content = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AReply.Content, TestData);
        }
    }
}

