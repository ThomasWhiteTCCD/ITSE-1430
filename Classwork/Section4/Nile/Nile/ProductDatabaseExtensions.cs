﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary>Provides extensions for <see cref="IProductDatabase"/>.</summary>
    public static class ProductDatabaseExtensions 
    {
        public static Product GetByName( this IProductDatabase source, string name)
        {
            foreach(var item in source.GetAll())
            {
                if (String.Compare(item.Name, name, true) == 0)
                    return item;                             
            };

            return null;
        }

        public static IEnumerable<Product> GetProductByDiscountPrice(this IProductDatabase source,
                                                                      Func<Product, decimal> priceCalculator)
        {
            //orderby priceCalculator(product)
            var products = from product in source.GetAll()
                           where product.IsDiscontinued 
                           //select new SomeType() {
                           select new {
                                Product = product, 
                                AdjustedPrice = product.IsDiscontinued ? priceCalculator(product) : product.Price
                           };

            // Instead of anonymous type
            // var tuple = Tuple.Create<Product, decimal>(new Product(), 10M);
            //(Product, AdjustedPrice) = (new Product, 10M);

            return from product in products
                   orderby product.AdjustedPrice
                   select product.Product;

        }

        //private (Product, decimal) DoSomething ()
        //{
        //    return (new Product, 10M);
        //}

        //private sealed class SomeType 
        //{
        //    public Product Product { get; set; }
        //    public decimal AdjustedPrice { get; set; }
        //}
        ///<summary>Adds seed data to a database.</summary>
        ///<param names="source">The data to seed.</param>
        public static void WithSeedData ( this IProductDatabase source)
        {
            source.Add(new Product() { Name = "Galaxy S7", Price = 650 });
            source.Add(new Product() { Name = "Galaxy Note 7", Price = 150, IsDiscontinued = true });
            source.Add(new Product() { Name = "Windows Phone", Price = 100 });
            source.Add(new Product() { Name = "iPhone X", Price = 1900, IsDiscontinued = true });
        }
    }
}
