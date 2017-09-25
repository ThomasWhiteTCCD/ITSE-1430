using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Name: Thomas White
 * Class: ITSE 1430
 * Time: 5:00 pm
 * Date: 09/13/2017
 */

namespace Lab1 {
    class Program {
        static void Main(string[] args)
        {
            bool quit = false;

            do
            {
                char choice = GetInput();
                Console.Clear();
                switch (choice)
                {
                    case 'L':
                        ListMovies();
                        break;
                    case 'A':
                        AddMovie();
                        break;
                    case 'R':
                        RemoveMovie();
                        break;
                    case 'Q':
                        quit = true;
                        break;
                    default:
                        break;
                }
            } while (!quit);
        }

        private static void RemoveMovie()
        {
            Console.WriteLine("Are you sure you want to delete this movie (Y/N)?");
            bool deleteAnswer = ReadYesNo();

            switch (deleteAnswer)
            {
                case true:
                    movieDescription = "";
                    Console.WriteLine();
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadLine();
                    break;
                case false:
                    Console.WriteLine();
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadLine();
                    break;
            }
        }

        private static void AddMovie()
        {
            Console.Write("Enter a title: ");
            movieName = Console.ReadLine().Trim();

            Console.Write("Enter optional description: ");
            movieDescription = Console.ReadLine().Trim();

            Console.Write("Enter optional length (in minutes): ");
            movieLength = ReadInteger();

            Console.Write("Do you own this movie? (Y/N): ");
            movieOwned = ReadYesNo();
        }

        static char GetInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu.");
                Console.WriteLine("".PadLeft(10, '-'));
                Console.WriteLine("L)ist Movies");
                Console.WriteLine("A)dd Movie");
                Console.WriteLine("R)emove Movie");
                Console.WriteLine("Q)uit");

                string input = Console.ReadLine().Trim();

                if (input != null && input.Length != 0)
                {
                    char letter = Char.ToUpper(input[0]);
                    if (letter == 'L')
                        return 'L';
                    else if (letter == 'A')
                        return 'A';
                    else if (letter == 'R')
                        return 'R';
                    else if (letter == 'Q')
                        return 'Q';
                }

                //Error
                Console.WriteLine("Please choose a valid option.");
            };
        }

        private static void ListMovies()
        {
            if (String.IsNullOrEmpty(movieDescription))
            {
                Console.WriteLine("No movies available.");
                Console.WriteLine();
                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
            }
            else
            {
                string desc = $"{movieName}\n{movieDescription}\n" + $"Run length = " + $"{movieLength}\n{(movieOwned ? "Status = Owned" : "Status = Wishlist")}";
                Console.WriteLine(desc);
                Console.WriteLine();
                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
            }
        }

        static bool ReadYesNo()
        {
            do
            {
                string input = Console.ReadLine();

                if (!String.IsNullOrEmpty(input))
                {
                    switch (Char.ToUpper(input[0]))
                    {
                        case 'Y':
                            return true;
                        case 'N':
                            return false;
                    };
                };

                Console.Write("Enter either Y or N: ");
            } while (true);
        }

        static int ReadInteger()
        {
            do
            {
                var input = Console.ReadLine();

                if (Int32.TryParse(input, out var result) && Int32.Parse(input) >= 0)
                {
                    return result;
                }
                Console.Write("Enter a value >= 0: ");
            } while (true);
        }

        // Variables for movie properties
        static string movieName;
        static int movieLength;
        static string movieDescription;
        static bool movieOwned;
    }
}
