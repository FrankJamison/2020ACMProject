using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BL
{
    public class ProductRepository
    {
        /// <summary>
        /// Retrieve one customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Product Retrieve(int productId)
        {
            // Create the instance of a product class
            // Pass in the requested ID
            Product product = new Product(productId);

            // Code that retrieves the defined product

            // Temporary hard-coded values to return
            // a populated product
            if (productId == 2)
            {
                product.ProductName = "Sunflowers";
                product.ProductDescription = "Assorted Size Set of 4 Bright Yellow Mini Sunflowers";
                product.CurrentPrice = 15.96M;
            }

            return product;
        }

        /// <summary>
        /// Saves the current product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool Save(Product product)
        {
            // Code that saves the passed product

            return true;
        }
    }
}
