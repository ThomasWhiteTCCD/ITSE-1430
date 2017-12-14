/*
 * ITSE 1430
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLib.Data
{
    /// <summary>Provides a base implementation of <see cref="IMovieDatabase"/>.</summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is <see langword="null"/>.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="ArgumentException">A movie with the same title already exists.</exception>
        public Movie Add ( Movie movie )
        {
            //Validate
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));
            ObjectValidator.ValidateObject(movie);

            //Movie cannot already exist
            var existing = FindByTitleCore(movie.Title);
            if (existing != null)
                throw new ArgumentException("Movie with same title already exists.", nameof(movie));

            // Thomas White - CR2: Added validation for release year
            if (movie.ReleaseYear < 1900 || movie.ReleaseYear > 2100)
                throw new ArgumentOutOfRangeException("Release year must be between 1900 and 2100.");

            // Thomas White - CR4: Added validation for movie title
            if (movie.Title.Length < 2 || movie.Title.Length > 100)
                throw new ArgumentOutOfRangeException("Movie Title must be between 2 and 100 characters.");

            //Create the new movie
            return AddCore(movie);
        }

        /// <summary>Gets a specific movie.</summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie, if found.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than or equal to zero.</exception>
        public Movie Get ( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be > 0.");

            return GetCore(id);
        }

        /// <summary>Gets all the movies.</summary>
        /// <returns>The list of movies.</returns>
        /// Thomas White - CR0: Changed Getall to GetAll
        public IEnumerable<Movie> GetAll ()
        {
            return GetAllCore();
        }

        /// <summary>Removes a movie.</summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// If the movie does not exist then nothing happens.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than or equal to zero.</exception>
        public bool Remove ( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be > 0.");

            var existing = GetCore(id);
            // Thomas White CR3: Changed to check if existing was not null 
            // to check if existing was null
            if (existing == null)
                return false;

            RemoveCore(id);
            return true;
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The updated movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is <see langword="null"/>.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="ArgumentException">
        /// A movie with the same title already exists.
        /// <para>-or-</para>
        /// The movie does not exist.
        /// </exception>        
        public Movie Update ( Movie movie )
        {
            //Validate
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));
            ObjectValidator.ValidateObject(movie);

            //Get the existing movie
            var existing = GetCore(movie.Id);
            if (existing == null)
                throw new ArgumentException("Move does not exist.");

            //Movie title cannot already exist
            existing = FindByTitleCore(movie.Title);
            if (existing != null && existing.Id != movie.Id)
                throw new ArgumentException("Movie with same title already exists.", nameof(movie));

            // Thomas White - CR2: Added validation for release year
            if (movie.ReleaseYear < 1900 || movie.ReleaseYear > 2100)
                throw new ArgumentOutOfRangeException("Release year must be between 1900 and 2100.");

            // Thomas White - CR4: Added validation for movie title
            if (movie.Title.Length < 2 || movie.Title.Length > 100)
                throw new ArgumentOutOfRangeException("Movie Title must be between 2 and 100 characters.");

            return UpdateCore(movie);
        }

        #region Protected Members

        /// <summary>Adds a new movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected abstract Movie AddCore ( Movie movie );

        /// <summary>Finds a movie by its title.</summary>
        /// <param name="title">The title.</param>
        /// <returns>The movie, if any.</returns>
        protected abstract Movie FindByTitleCore ( string title );

        /// <summary>Gets a movie given its ID.</summary>
        /// <param name="id">The ID.</param>
        /// <returns>The movie, if any.</returns>
        protected abstract Movie GetCore ( int id );

        /// <summary>Gets all the movies.</summary>
        /// <returns>The movies.</returns>
        protected abstract IEnumerable<Movie> GetAllCore ();

        /// <summary>removes a movie given its ID.</summary>
        /// <param name="id">The ID.</param>
        protected abstract void RemoveCore ( int id );

        /// <summary>Updates an existing movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        protected abstract Movie UpdateCore ( Movie movie );
        #endregion
    }
}
