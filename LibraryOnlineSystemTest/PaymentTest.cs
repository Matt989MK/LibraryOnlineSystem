using LibraryOnlineSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace LibraryOnlineSystemTest
{
    [TestClass]
    public class PaymentTest
    {
        [TestMethod]
        public void PaymentIdPropertyOk()
        {

            //create an instance of the class we want to create
            PaymentLibrary payment = new PaymentLibrary();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            payment.PaymentLibraryId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(payment.PaymentLibraryId, TestData);
        }
        [TestMethod]
        public void UserIdPropertyOk()
        {

            //create an instance of the class we want to create
            PaymentLibrary payment = new PaymentLibrary();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            payment.UserId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(payment.UserId, TestData);
        }
        [TestMethod]
        public void AmountPropertyOk()
        {

            //create an instance of the class we want to create
            PaymentLibrary payment = new PaymentLibrary();
            //create some test data to assign to the property
            Int32 TestData = 10;
            //assign the data to the property
            payment.Amount = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(payment.Amount, TestData);
        }
        [TestMethod]
        public void DatePaidPropertyOk()
        {

            //create an instance of the class we want to create
            PaymentLibrary payment = new PaymentLibrary();
            //create some test data to assign to the property
            DateTime TestData = DateTime.Now;
            //assign the data to the property
            payment.DatePaid = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(payment.DatePaid, TestData);
        }
        [TestMethod]
        public void StatusPropertyOk()
        {

            //create an instance of the class we want to create
            PaymentLibrary payment = new PaymentLibrary();
            //create some test data to assign to the property
            string TestData = "Paid";
            //assign the data to the property
            payment.Status = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(payment.Status, TestData);
        }
        [TestMethod]
        public void BookingPropertyOk()
        {

            //create an instance of the class we want to create
            PaymentLibrary payment = new PaymentLibrary();
            //create some test data to assign to the property
            Booking testData = new Booking();
            testData.BookingId = 1;
            //assign the data to the property
            payment.BookingId = testData.BookingId;
            //test to see that the two values are the same
            Assert.AreEqual(payment.BookingId, testData);
        }

        [TestMethod]
        public void GuIdPropertyOk()
        {
            PaymentLibrary payment = new PaymentLibrary();
            payment.guId = "1";
            string testData = "1";
            Assert.AreEqual(payment.guId, testData);

        }
    }
}
