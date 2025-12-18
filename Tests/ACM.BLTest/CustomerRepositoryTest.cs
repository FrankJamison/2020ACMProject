using System;
using System.Collections.Generic;
using ACM.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.BLTest
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        [TestMethod]
        public void RetrieveValid()
        {
            //-- Arrange
            var customerRepository = new CustomerRepository();

            var expected = new Customer(1)
            {
                EmailAddress = "fbaggins@hobbiton.me",
                FirstName = "Frodo",
                LastName = "Baggins"
            };

            //-- Act
            var actual = customerRepository.Retrieve(1);

            //-- Assert
            Assert.AreEqual(expected.CustomerID, actual.CustomerID);
            Assert.AreEqual(expected.EmailAddress, actual.EmailAddress);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
        }

        [TestMethod]
        public void RetrieveExistingWithAddress()
        {
            //-- Arrange
            var customerRepository = new CustomerRepository();

            var expected = new Customer(1)
            {
                EmailAddress = "fbaggins@hobbiton.me",
                FirstName = "Frodo",
                LastName = "Baggins",
                AddressList = new List<Address>()
                {
                    new Address()
                    {
                        AddressType = 1,
                        StreetLine1 = "Bag End",
                        StreetLine2 = "Bagshot Row",
                        City = "Hobbiton",
                        State = "Shire",
                        Country = "Middle Earth",
                        PostalCode = "144"
                    },
                    new Address()
                    {
                        AddressType = 2,
                        StreetLine1 = "Green Dragon",
                        City = "Bywater",
                        State = "Shire",
                        Country = "Middle Earth",
                        PostalCode = "146"
                    }
                }
            };

            //-- Act
            var actual = customerRepository.Retrieve(1);

            //-- Assert
            Assert.AreEqual(expected.CustomerID, actual.CustomerID);
            Assert.AreEqual(expected.EmailAddress, actual.EmailAddress);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);

            for (int ii = 0; ii < expected.AddressList.Count; ii++)
            {
                Assert.AreEqual(expected.AddressList[ii].AddressType, actual.AddressList[ii].AddressType);
                Assert.AreEqual(expected.AddressList[ii].StreetLine1, actual.AddressList[ii].StreetLine1);
                Assert.AreEqual(expected.AddressList[ii].StreetLine2, actual.AddressList[ii].StreetLine2);
                Assert.AreEqual(expected.AddressList[ii].City, actual.AddressList[ii].City);
                Assert.AreEqual(expected.AddressList[ii].State, actual.AddressList[ii].State);
                Assert.AreEqual(expected.AddressList[ii].Country, actual.AddressList[ii].Country);
                Assert.AreEqual(expected.AddressList[ii].PostalCode, actual.AddressList[ii].PostalCode);
            }
        }
    }
}
