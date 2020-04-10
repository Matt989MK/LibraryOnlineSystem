using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryOnlineSystem.Models;
namespace LibraryOnlineSystemTest
{
    [TestClass]
    class BookingTest
    {
        [TestMethod]
        public void BookingIdPropertyOk()
        {

            //create an instance of the class we want to create
            Booking booking=new Booking();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            booking.BookingId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(booking.BookingId, TestData);
        }
        [TestMethod]
        public void BookIdPropertyOk()
        {

            //create an instance of the class we want to create
            Booking booking = new Booking();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            booking.BookId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(booking.BookId, TestData);
        }
        [TestMethod]
        public void BookCodeIdPropertyOk()
        {

            //create an instance of the class we want to create
            Booking booking = new Booking();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            booking.BookCodeId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(booking.BookCodeId, TestData);
        }
        [TestMethod]
        public void DateCreatedPropertyOk()
        {

            //create an instance of the class we want to create
            Booking booking = new Booking();
            //create some test data to assign to the property
            DateTime TestData = DateTime.Today;
            //assign the data to the property
            booking.DateCreated = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(booking.DateCreated, TestData);
        }
        [TestMethod]
        public void DateReturnedPropertyOk()
        {

            //create an instance of the class we want to create
            Booking booking = new Booking();
            //create some test data to assign to the property
            DateTime TestData = DateTime.Today;
            //assign the data to the property
            booking.DateReturned = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(booking.DateReturned, TestData);
        }
        [TestMethod]
        public void UserIdPropertyOk()
        {

            //create an instance of the class we want to create
            Booking booking = new Booking();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            booking.userId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(booking.userId, TestData);
        }
        [TestMethod]
        public void UserPropertyOk()
        {

            //create an instance of the class we want to create
            Booking booking = new Booking();
            User TestData = new User()
            {
                UserId = 1,
                Email = "test@gmail.com",
                Name = "testName",
                Surname = "testSurname",
                DateOfBirth = DateTime.Today,
                HouseNo = 35,

            };
            //create some test data to assign to the property
           
            //assign the data to the property
            booking.User = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(booking.User, TestData);
        }
    }
}
