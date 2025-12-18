using System;
using System.Collections.Generic;

namespace ACM.BL
{
    public class Customer
    {
        public Customer() : this(0)
        {
            
        }

        public Customer(int customerId)
        {
            CustomerID = customerId;
            AddressList = new List<Address>();
        }

        public List<Address> AddressList { get; set; }

        public int CustomerID { get; private set; }
        public int CustomerType { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }

        //public string LastName { get; set; }

        public string FullName
        {
            get
            {
                string fullName = LastName;

                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    if (!string.IsNullOrWhiteSpace(fullName))
                        fullName += ", ";

                    fullName += FirstName;
                }

                return fullName;
            }
        }

        public static int InstanceCount { get; set; }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }

            set { _lastName = value; }
        }

        /// <summary>
        /// Retrieve one customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer Retrieve(int customerID)
        {
            // Code that retrieves the defined customer.

            return new Customer();
        }

        /// <summary>
        /// Validates the customer data.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(LastName))
                isValid = false;

            if (string.IsNullOrWhiteSpace(EmailAddress))
                isValid = false;

            return isValid;
        }
    }
}
