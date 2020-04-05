using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentmyPlace
{
    static class Report
    {
        public static void individualCustomerRentals()
        {
            Console.WriteLine("Enter Customer E-Mail Address");
            string _email = Console.ReadLine();
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            int index = 0;
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                if (temp[3]==_email)
                {
                    Console.WriteLine(data[index]);
                }
                index++;
            }

        }
        public static void historicalCustomerRentals()
        {
            Console.WriteLine("All Rentals: ");
            Console.WriteLine();
            string[] names = new string[getTotalLines()];
            int index = 0;
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                names[index] =temp[2];
                index++;
            }
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data.Length - 1; j++)
                {
                    if (names[j].CompareTo(names[j + 1]) > 0)
                    {
                        string temp = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = temp;

                        temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                    else if (names[j].CompareTo(names[j + 1]) == 0) 
                    {
                        DateTime date1 = DateTime.Parse(data[j].Substring(GetNthIndex(data[j], '\t', 4) + 1, 10));
                        DateTime date2 = DateTime.Parse(data[j+1].Substring(GetNthIndex(data[j+1], '\t', 4) + 1, 10));
                        if (date1 > date2) 
                        {
                            string temp = data[j];
                            data[j] = data[j + 1];
                            data[j + 1] = temp;
                        }
                    }
                }
            }

            foreach (string name in data) 
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            string[] uniqueNames = names.Distinct().ToArray();
            for (int i = 0; i < uniqueNames.Length; i++) 
            {
                Console.WriteLine(uniqueNames[i]);
                int count = 0;
                int totalRentalAmount = 0;
                for (int j = 0; j < data.Length; j++) 
                {
                    if (names[j] == uniqueNames[i]) 
                    {
                        count++;
                        string[] tokens = data[j].Split('\t');
                        totalRentalAmount += Int32.Parse(tokens[5]);
                    }
                }
                Console.WriteLine("Total No. of Rentals: " + count);
                Console.WriteLine("Average Rental Amount: " + totalRentalAmount/count);
                Console.WriteLine();

            }


        }
        public static int getTotalLines()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            while ((line = file.ReadLine()) != null)
            {
                counter++;
            }
            file.Close();
            return counter;
        }
        public static int GetNthIndex(string s, char t, int n)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public static void getRevenueByMonth()
        {
            Console.WriteLine("Enter Month(In 2 digits format #00)");
            string _month = Console.ReadLine();
            float _revenue = 0;
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            int index = 0;
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                string[] date= temp[4].Split('-');
                if (date[1] == _month)
                {
                  
                    _revenue += float.Parse(temp[5]);
                }
                index++;
            }
            Console.WriteLine("TOTAL REVENUE BY MONTH: "+_revenue);
        }
        public static void getRevenueByYear()
        {
            Console.WriteLine("Enter Yeat(In 4 digits format #0000)");
            string _year = Console.ReadLine();
            float _revenue = 0;
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            int index = 0;
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                string[] date = temp[4].Split('-');
                if (date[2] == _year)
                {

                    _revenue += float.Parse(temp[5]);
                }
                index++;
            }
            Console.WriteLine("TOTAL REVENUE BY YEAR: " + _revenue);
        }
    }
}
