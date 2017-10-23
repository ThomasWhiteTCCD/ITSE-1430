using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores 
{
    /// <summary>Base Class for product database.</summary>
    public class SeedMemoryProductDatabase : MemoryProductDatabase
    {
        public SeedMemoryProductDatabase()
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
            //_products.AddRange(new [] {

            AddCore(new Product() { Id = 1, Name = "Galaxy S7", Price = 650 });
            AddCore(new Product() { Id = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true });
            AddCore(new Product() { Id = 3, Name = "Windows Phone", Price = 100 });
            AddCore(new Product() { Id = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true });
            //});         
            
            //_nextId = _products.Count + 1;
        }
       
    }
}
