using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main( string[] args )
        {
            string answer;
            string movieTitle = "";
            string movieDescription = "";
            string movieLength = "0";
            string ownAnswer = "";
            Boolean movieStatus = false;
            Boolean movieDelete = false;


            do
            {               
                Console.WriteLine("L)ist Movies");
                Console.WriteLine("A)dd Movie");
                Console.WriteLine("R)emove Movie");
                Console.WriteLine("Q)uit");
                Console.WriteLine("What would you like to do? ");
                answer = Console.ReadLine();

                switch (answer.ToUpper())
                {
                    case "A":
                        do
                        {
                          Console.WriteLine("Enter a title: ");
                          movieTitle = Console.ReadLine();

                        } while (movieTitle.Length <= 0);

                        Console.WriteLine("Enter an option description: ");
                        movieDescription = Console.ReadLine();

                        do
                        {
                          Console.WriteLine("Enter the optional length (in minutes): ");
                          movieLength = Console.ReadLine();
                        } while (movieLength.Length <= 0);

                        do
                        {
                          Console.WriteLine("Do you own this movie? (Y/N)");
                          ownAnswer = Console.ReadLine();
                        } while (ownAnswer.ToLower() != "y" && ownAnswer.ToLower() != "n");

                        if (ownAnswer.ToLower() == "y")
                        {
                            movieStatus = true;
                        } else
                        {
                            movieStatus = false;
                        }
                    Console.WriteLine("Press enter to return to the main menu.");
                    Console.ReadLine();
                    Console.Clear();
                        break;
                    case "R":
                        Console.WriteLine("Are you sure you want to delete the movie? (Y/N)");
                        String deleteAns = Console.ReadLine();

                        if (deleteAns.ToUpper() == "Y")
                        {
                            movieTitle = "";
                        }
                    Console.WriteLine("Press enter to return to the main menu.");
                    Console.ReadLine();
                    Console.Clear();
                        break;
                    case "L":

                    if (movieTitle == "")
                    {
                        Console.WriteLine();
                        Console.WriteLine("No movies available.");
                        Console.WriteLine();
                    } else
                    {
                        Console.WriteLine();
                        Console.WriteLine(movieTitle);
                        Console.WriteLine(movieDescription);
                        Console.WriteLine("Run Length = " + movieLength);
                        Console.WriteLine("Status = " + movieStatus);
                        Console.WriteLine();
                    }
                    Console.WriteLine("Press enter to return to the main menu.");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                    case "Q":
                    Console.WriteLine("You have quit. Press enter to close program.");
                    break;
                }
            } while (answer.ToUpper() != "Q");

            Console.ReadLine();
        }

        static char GetInput ()
        {
            while (true)
            {
                Console.WriteLine("Main Menu");
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
    }
}
