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
        /// <exception cref="ArgumentNullException">Product is null.</exception>
        /// <exception cref="ValidationException">Product is invalid.</exception>
        public Product Add( Product product )
        {
            // Validate
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product was null.");           

            // if (!ObjectValidator.TryValidate(product, out var errors))
            //     throw new ValidationException("Product was not valid.", nameof(product));
            //return null;

            ObjectValidator.Validate(product);

            try
            {
                return AddCore(product);
            } catch (Exception e)
            {
                // Throw different exception
                throw new Exception("Add failed.", e);

                // Rethrow the exception
                throw;

                // Silently ignore - almost always bad

            };                 
        }

        protected abstract Product AddCore( Product product );

        /// <summary>Get a specific product.</summary>
        /// <returns>The product, if it exists.</returns>
        public Product Get (int id)
        {
            // Validate
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be > 0.");

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
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be > 0.");

            RemoveCore(id);
        }

        protected abstract void RemoveCore(int id);

        /// <summary>Updates a product.</summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        public Product Update(Product product)
        {
            // Validate
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            //if (!ObjectValidator.TryValidate(product, out var errors))
            //    throw new ArgumentException("Product is invalid.", nameof(product));  

            ObjectValidator.Validate(product);

            // Get existing product
            var existing = GetCore(product.Id) ?? throw new Exception("Product not found.");

            //if (existing == null)
            //    throw new Exception("Product not found.");

            return UpdateCore(existing, product);
        }

        protected abstract Product UpdateCore(Product existing, Product newItem);                     
    }
}
