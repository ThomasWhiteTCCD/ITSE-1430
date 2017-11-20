
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib 
{
    public static class MovieDatabaseExtensions 
    {
        /// <summary>Get a movie by title.</summary>
        /// <param name="source">The source.</param>
        /// <param name="title">The movie title.</param>
        /// <returns>The movie, if found.</returns>
        public static Movie GetByName(this IMovieDatabase source, string title)
        {
            foreach (var item in source.GetAll())
            {
                if (String.Compare(item.Title, title, true) == 0)
                    return item;
            };

            return null;
        }       

        /// <summary>Adds seed data to a database.</summary>
        /// <param name="source">The data to seed.</param>
        public static void WithSeedData(this IMovieDatabase source)
        {
            source.Add(new Movie() { Title = "Blade", Description = "A half-vampire, half-mortal man becomes a protector of the mortal race, while slaying evil vampires.", Length = 120, IsOwned = true });
            source.Add(new Movie() { Title = "Blade II", Description = "Blade forms an uneasy alliance with the vampire council in order to combat the Reapers, who are feeding on vampires.", Length = 125, IsOwned = true });
            source.Add(new Movie() { Title = "Blade: Trinity", Description = "Blade, now a wanted man by the FBI, must join forces with the Nightstalkers to face his most challenging enemy yet: Dracula.", Length = 113, IsOwned = true });
            source.Add(new Movie() { Title = "Silicon Valley", Description = "Follows the struggle of Richard Hendricks, a silicon valley engineer trying to build his own company called Pied Piper.", Length = 1350, IsOwned = true });
            source.Add(new Movie() { Title = "Mr. Robot", Description = "Follows Elliot, a young programmer working as a cyber-security engineer by day, and a vigilante hacker by night.", Length = 1140, IsOwned = true });
        }
    }
}
