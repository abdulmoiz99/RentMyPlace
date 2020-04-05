using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentmyPlace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=======RENT MY PLACE=======");
            Report.historicalCustomerRentals();

            //while (true)
            //{
            //    Console.WriteLine("\n\n=======MAIN MENU=======");

            //    Console.WriteLine("1.Listing");
            //    Console.WriteLine("2.Trasnaction");
            //    Console.WriteLine("0.Quit");
            //    int mainChoice = int.Parse(Console.ReadLine());
            //    if (mainChoice == 0)
            //    {
            //        break;
            //    }
            //    else if (mainChoice == 1)
            //    {
            //        while (true)
            //        {
            //            Console.WriteLine("1.Add Listing");
            //            Console.WriteLine("2.Modify Listing");
            //            Console.WriteLine("3.Delete Listing");
            //            Console.WriteLine("0.Back To Main Menu");
            //            int _listingChoice = int.Parse(Console.ReadLine());
            //            if (_listingChoice == 0)
            //                break;
            //            else if (_listingChoice == 1)
            //                Listing.AddListing();
            //            else if (_listingChoice == 2)
            //                Listing.ModifyListing();
            //            else if (_listingChoice == 3)
            //                Listing.DeleteListing();
            //            else Console.WriteLine("Invalid Input");

            //        }
            //    }
            //    else if (mainChoice == 2)
            //    {
            //        while (true)
            //        {
            //            Console.WriteLine("1.View Avaiable Condo’s");
            //            Console.WriteLine("2.Rent A Condo");
            //            Console.WriteLine("3.Checkout");
            //            Console.WriteLine("0.Exit");
            //            int _trasnactionChoice = int.Parse(Console.ReadLine());
            //            if (_trasnactionChoice == 0)
            //                break;
            //            else if (_trasnactionChoice == 1)
            //                Listing.DisplayAvailableListing();
            //            else if (_trasnactionChoice == 2)
            //                Transactions.AddTrasnaction();
            //            else if (_trasnactionChoice == 3)
            //                Transactions.CheckOut();
            //            else Console.WriteLine("Invalid Input");
            //        }
            //    }
            //    else Console.WriteLine("Invalid Input");
            //}
        }
    }
}
