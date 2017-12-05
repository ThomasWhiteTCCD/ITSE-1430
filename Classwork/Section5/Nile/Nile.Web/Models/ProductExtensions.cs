using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nile.Web.Models 
{
    public static class ProductExtensions 
    {
        public static IEnumerable<ProductViewModel> ToModel( this IEnumerable<Product> source )
        {
            return from item in source
                   select item.ToModel();
        }
        public static ProductViewModel ToModel( this Product source )
        {
            return new ProductViewModel() {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Price = source.Price,
                IsDiscontinued = source.IsDiscontinued
            };
        }

        /// <summary>Converts a <see cref="ProductViewModel"/> to a <see cref="Product"/> product.</summary>
        /// <param name="source">The model.</param>
        /// <returns>The product.</returns>
        public static Product ToDomain( this ProductViewModel source )
        {
            return new Product() {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Price = source.Price,
                IsDiscontinued = source.IsDiscontinued
            };
        }
    }
}