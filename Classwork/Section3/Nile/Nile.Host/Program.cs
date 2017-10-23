using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Host
{
    class Program
    {
        static void Main( string[] args )
        {
            bool quit = false;

            do
            {
                char choice = GetInput();
                switch (choice)
                {
                    case 'A':
                    AddProduct();
                    break;
                    case 'L':
                    ListProducts();
                    break;
                    case 'Q':
                    quit = true;
                    break;
                    default:
                    break;
                }
            } while (!quit);
        }

        private static void AddProduct()
        {
            Console.Write("Enter product name: ");
            productName = Console.ReadLine().Trim();

            // Ensure not empty
            //if(name != null && name.Length != 0)
            //{

            //}

            Console.Write("Enter price (>0): ");
            productPrice = ReadDecimal();

            Console.Write("Enter option description: ");
            productDescription = Console.ReadLine().Trim();

            Console.Write("Is it discontinued (Y/N): ");
            productDiscontinued = ReadYesNo();
        }

        private static void ListProducts()
        {
            //Name $price [Discontinued]
            // Description
            //string msg = String.Format("{0}\t\t\t${1}\t\t{2}", productName, productPrice, productDiscontinued ? "[Discontinued" : "");
            //Console.WriteLine(msg);
            //Console.WriteLine(productDescription);
            //Console.WriteLine();

            //Console.WriteLine("{0}\t\t\t${1}\t\t{2}", productName, productPrice, productDiscontinued ? "[Discontinued" : "");

            string msg = $"{productName}\t\t\t${productPrice}\t\t{(productDiscontinued ? "[Discontinued]" : "")}";
            Console.WriteLine(msg);
            Console.WriteLine(productDescription);
        }

        static char GetInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("".PadLeft(10, '-'));
                Console.WriteLine("A)dd Product");
                Console.WriteLine("L)ist Products");
                Console.WriteLine("Q)uit");

                string input = Console.ReadLine().Trim();

                if (input != null && input.Length != 0)
                {
                    char letter = Char.ToUpper(input[0]);
                    if (letter == 'A')
                        return 'A';
                    else if (letter == 'L')
                        return 'L';
                    else if (letter == 'Q')
                        return 'Q';
                }

                //Error
                Console.WriteLine("Please choose a valid option.");
            };
        }

        /// <summary> Reads a decimal from Console.</summary>
        /// <returns>The decimal value.</returns>
        static decimal ReadDecimal ()
        {      
            do
            {
                var input = Console.ReadLine();
   
                if (Decimal.TryParse(input, out var result))
                {
                    return result;
                }
                Console.Write("Enter a valid decimal: ");
            } while (true);
        }

        /// <summary> Reads a decimal from Console.</summary>
        /// <returns>The decimal value.</returns>
        static bool ReadYesNo()
        {
            do
            {
                string input = Console.ReadLine();

                if (!String.IsNullOrEmpty(input))
                {
                    switch (Char.ToUpper(input[0]))
                    {
                        case 'Y': return true;
                        case 'N': return false;
                    };
                };

                Console.Write("Enter either Y or N: ");
            } while (true);
        }

        static string ReadString (string errorMessage, bool allowEmpty)
        {
            //if(errorMessage == null)
            //{
            // errorMessage = "Enter a valid string: ";
            //}

            // null coalesce
            errorMessage = errorMessage ?? "Enter a valid string: ";


            // else
            // errorMessage = errorMessage.Trim();

            // null conditional
            errorMessage = errorMessage?.Trim();

            do
            {
                var input = Console.ReadLine();
                if(String.IsNullOrEmpty(input) && allowEmpty)
                {
                    return "";
                }
                else if(!String.IsNullOrEmpty(input))
                {
                    return input;
                }
               
                Console.Write(errorMessage);
            } while (true);
        }

        // Variables for product attributes
        static string productName;
        static decimal productPrice;
        static string productDescription;
        static bool productDiscontinued;
    }
}
