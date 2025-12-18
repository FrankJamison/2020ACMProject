using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BL
{
    public class OrderRepository
    {
        /// <summary>
        /// Retrieve one order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order Retrieve(int orderId)
        {
            // Create the instance of an order class
            // Pass in the requested ID
            Order order = new Order(orderId);

            // Code that retrieves the defined order

            // Temporary hard-coded values to return
            // a populated order
            if (orderId == 10)
            {
                // Use current year in hard-coded data
                order.OrderDate = new DateTimeOffset(DateTime.Now.Year, 4, 14, 10, 00, 00, new TimeSpan(7, 0, 0));
            }

            return order;
        }

        /// <summary>
        /// Saves the current order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool Save(Order order)
        {
            // Code that saves the passed product

            return true;
        }
    }
}
