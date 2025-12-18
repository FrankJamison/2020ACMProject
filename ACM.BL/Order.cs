using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BL
{
    public class Order
    {
        public Order() : this(0)
        {
            
        }

        public Order(int orderId)
        {
            OrderID = orderId;
            OrderItems = new List<OrderItem>();
        }

        public int CustomerID { get; set; }
        public DateTimeOffset? OrderDate { get; set; }
        public int OrderID { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int ShippingAddressID { get; set; }

        public bool Validate()
        {
            var isValid = true;

            if (OrderDate == null)
                isValid = false;

            return isValid;
        }
    }
}
