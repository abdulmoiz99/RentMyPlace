using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RentmyPlace
{
    static class Listing
    {
        public static void AddListing()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\_Listing.txt"))
            {
                Console.WriteLine("Listing File Not Found");
            }
            else
            {
                #region ListingVariables
                int _listingID; string _listingAddress, _listingEndDate, _listingOwnerEmail; float _listingLastPrice;
                #endregion
                Console.WriteLine("Add Listing");
                //We should change the listing ID to auto-increment
                Console.Write("Enter ID:");
                _listingID = int.Parse(Console.ReadLine());
                Console.Write("Enter Address:");
                _listingAddress = Console.ReadLine();
                Console.Write("Enter End Date:");
                _listingEndDate = Console.ReadLine();
                Console.Write("Enter Last Price:");
                _listingLastPrice = float.Parse(Console.ReadLine());
                Console.Write("Enter Owner's Email:");
                _listingOwnerEmail = Console.ReadLine();
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\_Listing.txt");
                try
                {
                    sw.Write(_listingID + "\t" + _listingAddress + "\t" + _listingEndDate + "\t" + _listingLastPrice + "\t" + _listingOwnerEmail);
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
        public static void ModifyListing(string ID)
        {
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\_Listing.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Substring(0, line.IndexOf('\t')) == ID)
                {
                    using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\_Listing.txt"))
                    {
                        for (int j = 0; j < lines.Length; j++)
                        {
                            if (i == j)
                            {
                                #region ListingVariables
                                string _listingAddress, _listingEndDate, _listingOwnerEmail; float _listingLastPrice;
                                #endregion
                                Console.WriteLine("Modify Listing: ");
                                //We should change the listing ID to auto-increment
                                Console.Write("Enter Address: ");
                                _listingAddress = Console.ReadLine();
                                Console.Write("Enter End Date: ");
                                _listingEndDate = Console.ReadLine();
                                Console.Write("Enter Last Price: ");
                                _listingLastPrice = float.Parse(Console.ReadLine());
                                Console.Write("Enter Owner's Email: ");
                                _listingOwnerEmail = Console.ReadLine();
                                writer.WriteLine(ID + "\t" + _listingAddress + "\t" + _listingEndDate + "\t" + _listingLastPrice + "\t" + _listingOwnerEmail);
                            }
                            else
                            {
                                writer.WriteLine(lines[j]);
                            }
                        }
                        Console.WriteLine("Your listing has been modified successfully.");
                    }
                    return;
                }
            }
            Console.WriteLine("Listing ID not found.");
            
        }

        public static void DeleteListing(string ID)
        {

            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\_Listing.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Substring(0, line.IndexOf('\t')) == ID)
                {
                    using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\_Listing.txt"))
                    {
                        for (int j = 0; j < lines.Length; j++)
                        {
                            if (i == j)
                            {
                                continue;
                            }
                            else
                            {
                                writer.WriteLine(lines[j]);
                            }
                        }
                        Console.WriteLine("Your listing has been deleted successfully.");
                    }
                    return;
                }
            }
            Console.WriteLine("Listing ID not found.");
        }

        public static int getLineNumber(int ID)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\_Listing.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(ID.ToString()))
                {
                    // Console.WriteLine(counter.ToString() + ": " + line);
                    break;
                }
                counter++;
            }
            file.Close();
            return counter;
        }
        
        public static int  generateID()
        {
            int counter = 1;
            string line;

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\_Listing.txt");
            while ((line = file.ReadLine()) != null)
            {
                counter++;
            }
            file.Close();
            return counter;
        }
    }
}
 