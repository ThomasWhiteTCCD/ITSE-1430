using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores 
{
    /// <summary>Base Class for product database.</summary>
    public abstract class ProductDatabase : IProductDatabase 
    {        
        /// <summary>Adds a product.</summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        public Product Add( Product product )
        {
            // TODO: Validation
            if(product == null)           
                return null;

            // Using IValidateableObject
            if (!ObjectValidator.TryValidate(product, out var errors))
                return null;
            //if (!String.IsNullOrEmpty(product.Validate()))
            //    return null;

            // Copy product (emulate database)
            return AddCore(product);

            //var newProduct = CopyProduct(product);

            //// Adds new product to the core
            //AddCore(newProduct);

            ////TODO: Implement add
            //return CopyProduct(newProduct);
        }

        protected abstract Product AddCore( Product product );

        /// <summary>Get a specific product.</summary>
        /// <returns>The product, if it exists.</returns>
        public Product Get (int id)
        {
            // Validation
            if (id <= 0)
                return null;

            return GetCore(id);    
        }

        protected abstract Product GetCore( int id );        

        /// <summary>Gets all products.</summary>
        /// <returns>The products.</returns>
        public IEnumerable<Product> GetAll()
        {
            return GetAllCore();
            #region
            //// How many products
            //var count = 0;

            //foreach(var product in _list)
            //{
            //    if(product != null)
            //    {
            //        ++count;
            //    }
            //};

            //var items = new Product[count];
            //var index = 0;

            //foreach(var product in _list)
            //{
            //    if(product != null)
            //        items[index++] = CopyProduct(product);
            //};

            //return items;
            #endregion
        }

        protected abstract IEnumerable<Product> GetAllCore();

        /// <summary>Removes the product.</summary>
        /// <param name="product">The product to remove.</param>
        public void Remove(int id)
        {
            if (id <= 0)
                return;

            RemoveCore(id);
        }

        protected abstract void RemoveCore(int id);

        /// <summary>Updates a product.</summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        public Product Update(Product product)
        {
            // TODO: Validation
            if (product == null)
                return null;

            // Using IValidateableObject
            if (!ObjectValidator.TryValidate(product, out var errors))
                return null;
            //if (!String.IsNullOrEmpty(product.Validate()))
            //    return null;

            // Get existing product
            var existing = GetCore(product.Id);
            if (existing == null)
                return null;

            return UpdateCore(existing, product);
        }

        protected abstract Product UpdateCore(Product existing, Product newItem);                     
    }
}
