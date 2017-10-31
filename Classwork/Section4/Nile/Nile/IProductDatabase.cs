using System.Collections.Generic;

namespace Nile 
{
    public interface IProductDatabase 
    {
        /// <exception cref="ArgumentNullException">Product is null.</exception>
        /// <exception cref="ValidationException">Product is invalid.</exception>
        Product Add( Product product );
        Product Get( int id );
        IEnumerable<Product> GetAll();
        void Remove( int id );
        Product Update( Product product );
    }
}