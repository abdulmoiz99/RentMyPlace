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
                int _trasctionID = generateID();
                int _listingID; string _renterName, _renterEmail, _OwnerEmail; float _rentalAmount;
                DateTime _rentalDate = DateTime.Now;
                #endregion
                Console.WriteLine("Perform Trasnactiion");
                while (true)
                {
                    Console.Write("Enter Listing ID:");
                    _listingID = int.Parse(Console.ReadLine());
                    if (Listing.checkID(_listingID) == true)
                    {
                        break;
                    }
                    else Console.WriteLine("No Record Found! Please Enter Valid Listing Id");
                }
                if (Listing.checkAvaiableity(_listingID) == false)
                {
                    Console.Write("Enter Renter Name:");
                    _renterName = Console.ReadLine();
                    Console.Write("Enter Renter Email:");
                    _renterEmail = Console.ReadLine();
                    while (true)
                    {
                        Console.Write("Enter Rental Amount:");
                        if (float.TryParse(Console.ReadLine(), out _rentalAmount))
                        {
                            break;
                        }
                        else Console.WriteLine("Please Enter Valid Amount");

                    }
                    Console.Write("Enter Owner's Email:");
                    _OwnerEmail = Console.ReadLine();

                    StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
                    try
                    {
                        sw.Write(_trasctionID + "\t" + _listingID + "\t" + _renterName + "\t" + _renterEmail + "\t" + _rentalDate.ToString("dd/MM/yyyy") + "\t" + _rentalAmount + "\t" + _OwnerEmail + "\t" + "dd/MM/yyyy"); // dd/MM/yyyy reffers to checkout date
                        sw.WriteLine();
                        Listing.UpdateStatus(_listingID, "N");
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
                else Console.WriteLine("Listing Is Already Rented");
            }
        }
        public static void CheckOut()
        {
            Console.WriteLine("Check-Out");
            int _listingID;
            while (true)
            {
                Console.Write("Enter Listing ID:");
                _listingID = int.Parse(Console.ReadLine());
                if (Listing.checkID(_listingID) == true)
                {
                    break;
                }
                else Console.WriteLine("No Record Found! Please Enter Valid Listing Id");
            }
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Substring(0, line.IndexOf('\t')) == _listingID.ToString())
                {
                    using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt"))
                    {
                        for (int j = 0; j < lines.Length; j++)
                        {
                            if (i == j)
                            {
                                DateTime _checkoutDate = DateTime.Now;
                                string[] temp = line.Split('\t');
                                writer.WriteLine(temp[0] + "\t" + temp[1] + "\t" + temp[2] + "\t" + temp[3] + "\t" + temp[4] + "\t" + temp[5] + "\t" + temp[6] + "\t" + _checkoutDate.Date.ToString("dd/MM/yyyy"));
                                Listing.UpdateStatus(_listingID, "Y");
                            }
                            else
                            {
                                writer.WriteLine(lines[j]);
                            }
                        }
                        Console.WriteLine("Renting Successful.");
                    }
                    return;
                }
            }
            Console.WriteLine("Listing ID not found.");
        }
        public static bool checkID(int ID)
        {
            bool status = false;

            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                if (temp[0].Equals(ID.ToString()))//temp 5 reffers to the status 
                {
                    status = true;
                }
            }
            return status;
        }
        public static int generateID()
        {

            int counter = 1;
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            //First we get the highest no in the ID section "temp[0]"
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                if (int.Parse(temp[0]) > counter)
                {
                    counter = int.Parse(temp[0]);
                }
            }
            //Then add 1 to the highest value to generate a new ID 
            counter++;
            return counter;
        }

    }
}

