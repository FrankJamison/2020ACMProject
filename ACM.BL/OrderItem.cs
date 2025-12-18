using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BL
{
    public class OrderItem
    {
        public OrderItem()
        {
            
        }

        public OrderItem(int orderItemId)
        {
            OrderItemID = orderItemId;
        }

        public int OrderItemID { get; set; }
        public int ProductID { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// Retrieve one order item
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns></returns>
        public OrderItem Retrieve(int orderItemId)
        {
            // Code that retrieves the defined order item

            return new OrderItem();
        }

        /// <summary>
        /// Saves the current order item.
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            // Code that saves the current order item

            return true;
        }


        public bool Validate()
        {
            var isValid = true;

            if (Quantity <= 0)
                isValid = false;

            if (ProductID <= 0)
                isValid = false;

            if (PurchasePrice == null)
                isValid = false;

            return isValid;
        }
    }


}
