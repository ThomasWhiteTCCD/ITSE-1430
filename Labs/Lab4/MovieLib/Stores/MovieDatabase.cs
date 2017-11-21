
/* Name: Thomas White
 * Class: ITSE 1430
 * Project: 4th Programming Assignment
 * Class Time: 5:00 pm
 * Date: 11/19/2017
 */

using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MovieLib.Stores 
{
    /// <summary>Provides a base implementation of <see cref="IMovieDatabase"/>.</summary>
    public abstract class MovieDatabase : IMovieDatabase 
    {
        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="movie"/> title already exists.</exception>
        public Movie Add(Movie movie)
        {                                                         
            // Try catch for validation
            try
            {
                // Validate
                ObjectValidator.Validate(movie);

                // If movie is null
                if (movie == null)
                    throw new ArgumentNullException(nameof(movie), "Movie was null");

                // Check if movie title already exists
                foreach (var tempMovie in GetAllCore())
                {
                    if (tempMovie.Title == movie.Title)
                    {
                        throw new InvalidOperationException("Movie title already exists");
                    }
                }
                return AddCore(movie);
            }
            catch (ArgumentException e)
            {
                throw;
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
            catch (Exception e)
            {
                //Throw different exception
                throw new Exception("Add failed", e);
            };
        }

        /// <summary>Get a specific movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="id"/> is out of range.</exception>
        public Movie Get(int id)
        {
            try
            {
                //Validate
                if (id <= 0)
                    throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0.");

                return GetCore(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw;
            }                        
        }

        /// <summary>Gets all movies.</summary>
        /// <returns>The movies.</returns>
        public IEnumerable<Movie> GetAll()
        {
            return GetAllCore();
        }

        /// <summary>Removes the movie.</summary>
        /// <param name="id">The movie to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> must be greater than or equal to 0.</exception> 
        public void Remove(int id)
        {
            try
            {
                // Validate
                if (id <= 0)
                    throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0.");

                RemoveCore(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw;
            }                        
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="Exception">Movie not found.</exception>
        public Movie Update(Movie movie)
        {
            // Try catch for validation
            try
            {
                // Movie not valid
                ObjectValidator.Validate(movie);

                if (movie == null)
                    throw new ArgumentNullException(nameof(movie));

                // Title already exists
                foreach (var tempMovie in GetAllCore())
                {
                    if (tempMovie.Title.ToLower() == movie.Title.ToLower() && 
                        tempMovie.Description != movie.Description)
                    {
                        throw new InvalidOperationException("Movie title already exists");
                    }
                }

                // Movie doesn't exist
                var existing = GetCore(movie.Id) ?? throw new Exception("Movie not found.");

                return UpdateCore(existing, movie);
            }
            catch (ArgumentNullException e)
            {
                throw;
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
            catch(Exception e)
            {
                throw;
            }           
        }

        #region Protected Members

        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected abstract Movie AddCore(Movie movie);

        /// <summary>Get a movie given its ID.</summary>
        /// <param name="id">The ID.</param>
        /// <returns>The movie, if any.</returns>
        protected abstract Movie GetCore(int id);

        protected abstract IEnumerable<Movie> GetAllCore();

        /// <summary>Removes a movie given its ID.</summary>
        /// <param name="id">The ID.</param>
        protected abstract void RemoveCore(int id);

        /// <summary>Updates a movie.</summary>
        /// <param name="existing">The existing movie.</param>
        /// <param name="newItem">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        protected abstract Movie UpdateCore(Movie existing, Movie newItem);

        #endregion
    }
}
