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
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt"))
            {
                Console.WriteLine("Listing File Not Found");
            }
            else
            {
                #region ListingVariables
                int _listingID = generateID(); ; string _listingAddress, _listingEndDate, _listingOwnerEmail; float _listingLastPrice;
                #endregion
                Console.WriteLine("\nAdd Listing");
                //We should change the listing ID to auto-increment
                Console.Write("Enter Address:");
                _listingAddress = Console.ReadLine();
                Console.Write("Enter End Date:");
                _listingEndDate = Console.ReadLine();
                while (true)
                {
                    Console.Write("Enter Last Price: ");
                    if (float.TryParse(Console.ReadLine(), out _listingLastPrice))
                    {
                        break;
                    }
                    else Console.WriteLine("Please Enter Valid Price");

                }
                Console.Write("Enter Owner's Email:");
                _listingOwnerEmail = Console.ReadLine();
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt");
                try
                {
                    sw.Write(_listingID + "\t" + _listingAddress + "\t" + _listingEndDate + "\t" + _listingLastPrice + "\t" + _listingOwnerEmail + "\t" + "N"); // N refers to the listing status 
                    sw.WriteLine();
                    Console.WriteLine("Listing Added Successfully\n");
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
        public static void ModifyListing()
        {
            int _listingID;
            Console.WriteLine("\nModify Listing");
            while (true)
            {
                Console.WriteLine("Enter Listing ID");
                 _listingID = int.Parse(Console.ReadLine());
                if (checkID(_listingID) ==true)
                {
                    break;
                }
                else Console.WriteLine("No Record Found! Please Enter Valid ID");
            }
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Substring(0, line.IndexOf('\t')) == _listingID.ToString())
                {
                    using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt"))
                    {
                        for (int j = 0; j < lines.Length; j++)
                        {
                            if (i == j)
                            {
                                #region ListingVariables
                                string _listingAddress, _listingEndDate, _listingOwnerEmail, _listingStatus; float _listingLastPrice;
                                #endregion
                                //We should change the listing ID to auto-increment
                                Console.Write("Enter Address: ");
                                _listingAddress = Console.ReadLine();
                                Console.Write("Enter End Date: ");
                                _listingEndDate = Console.ReadLine();
                                while (true)
                                {
                                    Console.Write("Enter Last Price: ");
                                    if (float.TryParse(Console.ReadLine(), out _listingLastPrice))
                                    {
                                        break;
                                    }
                                    else Console.WriteLine("Please Enter Valid Price");

                                }
                                Console.Write("Enter Owner's Email: ");
                                _listingOwnerEmail = Console.ReadLine();
                                while (true)
                                {
                                    Console.WriteLine("Availability (Y/N)");
                                    _listingStatus = Console.ReadLine();
                                    if (_listingStatus == "Y" || _listingStatus == "N")
                                    {
                                        break;
                                    }
                                    else Console.WriteLine("Please Enter Valid Status");

                                }

                                writer.WriteLine(_listingID + "\t" + _listingAddress + "\t" + _listingEndDate + "\t" + _listingLastPrice + "\t" + _listingOwnerEmail + "\t" + _listingStatus);
                            }
                            else
                            {
                                writer.WriteLine(lines[j]);
                            }
                        }
                        Console.WriteLine("Your listing has been modified successfully\n.");
                    }
                    return;
                }
            }
            Console.WriteLine("Listing ID not found.");

        }

        public static void DeleteListing()
        {
            int _listingID;
            Console.WriteLine("\nDelete Listing");
            while (true)
            {
                Console.WriteLine("Enter Listing ID");
                _listingID = int.Parse(Console.ReadLine());
                if (checkID(_listingID) == true)
                {
                    break;
                }
                else Console.WriteLine("No Record Found! Please Enter Valid ID");
            }
            if (checkAvaiableity(_listingID) == false)
            {
                string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Substring(0, line.IndexOf('\t')) == _listingID.ToString())
                    {
                        using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt"))
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
                            Console.WriteLine("\nYour listing has been deleted successfully.");
                        }
                        return;
                    }
                }
            }
            else Console.WriteLine("Cannot Delete the current listing is on Rent");
        }
       
        /// <summary>
        /// Only display thoes lising where status is Y(Yes)
        /// </summary>
        public static void DisplayAvailableListing()
        {

            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt");
            Console.WriteLine("\nId\tAddress\tEndDate\tPrice\tOwner_Email");
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                if (temp[5].Equals("Y"))//temp 5 reffers to the status 
                {
                    Console.WriteLine(temp[0] + "\t" + temp[1] + "\t" + temp[2] + "\t" + temp[3] + "\t" + temp[4] + "\t");
                }
            }

        }
        public static void UpdateStatus(int ID,string status)
        {
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Substring(0, line.IndexOf('\t')) == ID.ToString())
                {
                    using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt"))
                    {
                        for (int j = 0; j < lines.Length; j++)
                        {
                            if (i == j)
                            {
                                DateTime _checkoutDate = DateTime.Now;
                                string[] temp = line.Split('\t');
                                writer.WriteLine(ID + "\t" + temp[1] + "\t" + temp[2] + "\t" + temp[2] + "\t" + temp[3] + "\t" + temp[4] + "\t" + status);
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

        }
        public static bool checkAvaiableity(int ID)
        {
            bool status = false;

            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt");
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                if (temp[0].Equals(ID.ToString()))//temp 5 reffers to the status 
                {
                    if (temp[5].Equals("Y"))
                    {
                        status = true;
                    }
                }
            }
            return status;
        }
        public static bool checkID(int ID)
        {
            bool status = false;

            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt");
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
        public static int getLineNumber(int ID)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt");
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

        public static int generateID()
        {

            int counter = 1;
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\Listing.txt");
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
