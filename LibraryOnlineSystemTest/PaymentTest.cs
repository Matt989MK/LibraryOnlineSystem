using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryOnlineSystem.Models;
namespace LibraryOnlineSystemTest
{
    [TestClass]
   public class PaymentTest
    {
        [TestMethod]
        public void PaymentIdPropertyOk()
        {

            //create an instance of the class we want to create
           Payment payment = new Payment();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            payment.PaymentId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(payment.PaymentId, TestData);
        }
        [TestMethod]
        public void UserIdPropertyOk()
        {

            //create an instance of the class we want to create
            Payment payment = new Payment();
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
            Payment payment = new Payment();
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
            Payment payment = new Payment();
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
            Payment payment = new Payment();
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
            Payment payment = new Payment();
            //create some test data to assign to the property
            LibraryOnlineSystem.Models.Booking testData = new LibraryOnlineSystem.Models.Booking
            {
                BookCodeId = 1,
                BookingId = 1,
                BookId = 1
            };
            //assign the data to the property
            payment.Booking = testData;
            //test to see that the two values are the same
            Assert.AreEqual(payment.Booking, testData);
        }
    }
}
