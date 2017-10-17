using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile 
{
    /// <summary>Base Class for product database.</summary>
    public class ProductDatabase 
    {
        public ProductDatabase()
        {
            // Long way
            // var product = new Product();
            // product.Name = "Samsung Note 7";

            // Object initializer syntax
            //_products.Add(new Product() { Id = 1, Name = "Galaxy S7", Price = 650 });
            //_products.Add(new Product() { Id = 2, Name = "Samsung Note 7",Price = 150, IsDiscontinued = true });            
            //_products.Add(new Product() { Id = 3, Name = "Windows Phone", Price = 100 });          
            //_products.Add(new Product() { Id = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true });

            // Collection initializer syntax
            //_products = new List<Product>() {
            //    new Product() { Id = 1, Name = "Galaxy S7", Price = 650 },
            //    new Product() { Id = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true },
            //    new Product() { Id = 3, Name = "Windows Phone", Price = 100 },
            //    new Product() { Id = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true },
            //};

            // Collection initializer syntax with array
            _products.AddRange(new [] {
                new Product() { Id = 1, Name = "Galaxy S7", Price = 650 },
                new Product() { Id = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true },
                new Product() { Id = 3, Name = "Windows Phone", Price = 100 },
                new Product() { Id = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true },
            });
            
            _nextId = _products.Count + 1;
        }

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
            var newProduct = CopyProduct(product);

            // Add copy to the list
            _products.Add(newProduct);

            // Add unique Id
            newProduct.Id = _nextId++;

            //TODO: Implement add
            return CopyProduct(newProduct);
        }

        /// <summary>Get a specific product.</summary>
        /// <returns>The product, if it exists.</returns>
        public Product Get (int id)
        {
            // Validation
            if(id <= 0)            
                return null;

            var product = FindProduct(id);

            //TODO: Implement Get
            return (product != null) ? CopyProduct(product) : null;
        }

        /// <summary>Gets all products.</summary>
        /// <returns>The products.</returns>
        public Product[] GetAll()
        {

            var items = new Product[_products.Count];
            var index = 0;

            foreach (var product in _products)
                items[index++] = CopyProduct(product);

            return items;

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
        }

        /// <summary>Removes the product.</summary>
        /// <param name="product">The product to remove.</param>
        public void Remove(int id)
        {
            if (id <= 0)
                return;

            var product = FindProduct(id);
            if (product != null)
                _products.Remove(product);
        }

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
            var existing = FindProduct(product.Id);
            if (existing == null)
                return null;

            // Replace 
            _products.Remove(existing);

            // Copy product (emulate database)          
            var newProduct = CopyProduct(product);

            // Add copy to the list
            _products.Add(newProduct);

            //TODO: Implement add
            return CopyProduct(newProduct);
        }

        private Product CopyProduct(Product product)
        {
            if (product == null)
                return null;

            var newProduct = new Product();
            newProduct.Id = product.Id;
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.IsDiscontinued = product.IsDiscontinued;

            return newProduct;
        }

        // Find a product by ID
        private Product FindProduct(int id)
        {
            foreach (var product in _products)
            {
                if (product.Id == id)
                {
                    _products.Remove(product);
                    return product;
                };
            };

            return null;
        }

        //private Product[] _products = new Product[100];
        private List<Product> _products = new List<Product>();
        private int _nextId = 1;

    }
}
