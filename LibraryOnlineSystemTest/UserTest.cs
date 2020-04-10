using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryOnlineSystem.Models;
namespace LibraryOnlineSystemTest
{
    [TestClass]
  public  class UserTest
    {
        [TestMethod]
        public void UserIdPropertyOk()
        {

            //create an instance of the class we want to create
            User AUser = new User();
            //create some test data to assign to the property
            Int32 TestData = 1;
            //assign the data to the property
            AUser.UserId = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AUser.UserId, TestData);
        }

        [TestMethod]
        public void UserNamePropertyOk()
        {
            //create an instance of the class we want to create
            User AUser = new User();
            //create some test data to assign to the property
            string TestData = "Name";
            //assign the data to the property
            AUser.Name = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AUser.Name, TestData);
        }
        [TestMethod]
        public void UserSurNamePropertyOk()
        {
            //create an instance of the class we want to create
            User AUser = new User();
            //create some test data to assign to the property
            string TestData = "Surname";
            //assign the data to the property
            AUser.Surname = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AUser.Surname, TestData);
        }
        [TestMethod]
        public void UserDateOfBirthPropertyOk()
        {
            //create an instance of the class we want to create
            User AUser = new User();
            //create some test data to assign to the property
            DateTime TestData = DateTime.Now.Date;
            //assign the data to the property
            AUser.DateOfBirth = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AUser.DateOfBirth, TestData);
        }
        [TestMethod]
        public void UserRolePropertyOk()
        {
            //create an instance of the class we want to create
            User AUser = new User();
            //create some test data to assign to the property
            string TestData = "User";
            //assign the data to the property
            AUser.UserRole = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AUser.UserRole, TestData);
        }
        [TestMethod]
        public void UserEmailPropertyOk()
        {
            //create an instance of the class we want to create
            User AUser = new User();
            //create some test data to assign to the property
            string TestData = "abc@gmail.com";
            //assign the data to the property
            AUser.Email = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AUser.Email, TestData);
        }
        [TestMethod]
        public void UserHouseNoPropertyOk()
        {
            //create an instance of the class we want to create
            User AUser = new User();
            //create some test data to assign to the property
            int TestData = 35;
            //assign the data to the property
            AUser.HouseNo = TestData;
            //test to see that the two values are the same
            Assert.AreEqual(AUser.HouseNo, TestData);
        }
    }
}
