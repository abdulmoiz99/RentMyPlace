using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentmyPlace
{
    static class Transactions
    {
        public static void AddTrasnaction()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt"))
            {
                Console.WriteLine("Listing File Not Found");
            }
            else
            {
                #region ListingVariables
                int _listingID; string _renterName, _renterEmail, _OwnerEmail; float _rentalAmount;
                DateTime _rentalDate = DateTime.Now, _checkoutDate;
                #endregion
                Console.WriteLine("Add Listing");
                //We should change the listing ID to auto-increment
                Console.Write("Enter Listing ID:");
                _listingID = int.Parse(Console.ReadLine());
                Console.Write("Enter Renter Name:");
                _renterName = Console.ReadLine();
                Console.Write("Enter Renter Email:");
                _renterEmail = Console.ReadLine();
                Console.Write("Enter Rental Amount:");
                _rentalAmount = float.Parse(Console.ReadLine());
                Console.Write("Enter Owner's Email:");
                _OwnerEmail = Console.ReadLine();
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
                try
                {
                    sw.Write(_listingID + "\t" + _renterName + "\t" + _renterEmail + "\t" + _rentalDate.ToString("dd/MM/yyyy") + "\t" + _rentalAmount + "\t" + _OwnerEmail +"\t" +"dd/MM/yyyy"); // dd/MM/yyyy reffers to checkout date
                    sw.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sw.Flush();
                    sw.Close();
                }
            }
        }
    }
}
