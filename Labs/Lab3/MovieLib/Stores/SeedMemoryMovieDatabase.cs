using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Name: Thomas White
 * Class: ITSE 1430
 * Project: 3rd Programming Assignment
 * Class Time: 5:00 pm
 * Date: 10/18/2017
 */

namespace MovieLib.Stores 
{
    /// <summary>Provides a <see cref="MemoryMovieDatabase"/> with movies already added.</summary>
    public class SeedMemoryMovieDatabase : MemoryMovieDatabase 
    {
        /// <summary>Initializes an instance of the <see cref="SeedMemoryMovieDatabase"/> class.</summary>
        public SeedMemoryMovieDatabase()
        {            
            AddCore(new Movie() { Id = 1, Title = "Blade", Description = "A half-vampire, half-mortal man becomes a protector of the mortal race, while slaying evil vampires.", Length = 120, IsOwned = true });
            AddCore(new Movie() { Id = 2, Title = "Blade II", Description = "Blade forms an uneasy alliance with the vampire council in order to combat the Reapers, who are feeding on vampires.", Length = 125, IsOwned = true });
            AddCore(new Movie() { Id = 3, Title = "Blade: Trinity", Description = "Blade, now a wanted man by the FBI, must join forces with the Nightstalkers to face his most challenging enemy yet: Dracula.", Length = 113, IsOwned = true });
            AddCore(new Movie() { Id = 4, Title = "Silicon Valley", Description = "Follows the struggle of Richard Hendricks, a silicon valley engineer trying to build his own company called Pied Piper.", Length = 1350, IsOwned = true });
            AddCore(new Movie() { Id = 5, Title = "Mr. Robot", Description = "Follows Elliot, a young programmer working as a cyber-security engineer by day, and a vigilante hacker by night.", Length = 1140, IsOwned = true });
        }
    }
}
