﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentmyPlace
{
    static class Report
    {
        /// <summary>
        /// Displays the record of the pervious rentals of the customer when the user enters a e-mail
        /// </summary>
        public static void individualCustomerRentals()
        {
            string text = ""; 
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
                    text += data[index] + System.Environment.NewLine;
                }
                index++;
            }
            string choice;
            while (true)
            {
                Console.WriteLine("\nDo you want to save this report? (Y/N): ");
                choice = Console.ReadLine();
                if (choice == "Y" || choice == "y" || choice == "N" || choice == "n")
                {
                    break;
                }
                else Console.WriteLine("Please Enter Valid Choice");
            }
            if (choice == "Y" || choice == "y")
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Report.txt", true);
                try
                {
                    sw.WriteLine("Individual Customer Rentals (Date: " + DateTime.Now.ToString() + "): ");
                    sw.WriteLine();
                    sw.Write(text);
                    sw.WriteLine();
                    sw.WriteLine("===========================================================================");
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
        /// <summary>
        /// A list of all rentals sorted by customer then by date
        /// For each customer provide total number of rentals and average rental amount.
        /// </summary>
        public static void historicalCustomerRentals()
        {
            string text = "";
            Console.WriteLine("Historical Customer Rentals: ");
            Console.WriteLine();
            string[] names = new string[getTotalLines()];
            int index = 0;
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                names[index] = temp[2];
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
                        DateTime date2 = DateTime.Parse(data[j + 1].Substring(GetNthIndex(data[j + 1], '\t', 4) + 1, 10));
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
                text += name + System.Environment.NewLine;
            }
            Console.WriteLine();
            text += System.Environment.NewLine;
            string[] uniqueNames = names.Distinct().ToArray();
            for (int i = 0; i < uniqueNames.Length; i++)
            {
                Console.WriteLine(uniqueNames[i]);
                text += uniqueNames[i] + System.Environment.NewLine;
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
                text += "Total No. of Rentals: " + count + System.Environment.NewLine;
                Console.WriteLine("Average Rental Amount: " + totalRentalAmount / count);
                text += "Average Rental Amount: " + totalRentalAmount / count + System.Environment.NewLine;
                Console.WriteLine();
                text += System.Environment.NewLine;
            }
            string choice;
            while (true)
            {
                Console.WriteLine("\nDo you want to save this report? (Y/N): ");
                choice = Console.ReadLine();
                if (choice == "Y" || choice == "y" || choice == "N" || choice == "n")
                {
                    break;
                }
                else Console.WriteLine("Please Enter Valid Choice");
            }
            if (choice == "Y" || choice == "y")
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Report.txt", true);
                try
                {
                    sw.WriteLine("Historical Customer Rentals (Date: " + DateTime.Now.ToString() + "): ");
                    sw.WriteLine();
                    sw.Write(text);
                    sw.WriteLine();
                    sw.WriteLine("===========================================================================");
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
        /// <summary>
        /// gets the total numeber of lines in the transaction text file
        /// this function help in initializing the array 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Take a input of 2 digit and dispay the total revenu genertated in that month
        /// </summary>
        public static void getRevenueByMonth()
        {
            string text = "";
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
            text += "TOTAL REVENUE BY MONTH: " + _revenue + System.Environment.NewLine;
            string choice;
            while (true)
            {
                Console.WriteLine("\nDo you want to save this report? (Y/N): ");
                choice = Console.ReadLine();
                if (choice == "Y" || choice == "y" || choice == "N" || choice == "n")
                {
                    break;
                }
                else Console.WriteLine("Please Enter Valid Choice");
            }
            if (choice == "Y" || choice == "y")
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Report.txt", true);
                try
                {
                    sw.WriteLine("Revenue By Month (Date: " + DateTime.Now.ToString() + "): ");
                    sw.WriteLine();
                    sw.Write(text);
                    sw.WriteLine();
                    sw.WriteLine("===========================================================================");
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
        /// <summary>
        /// Take a input of 4 digit and dispay the total revenu genertated in that year
        /// </summary>
        public static void getRevenueByYear()
        {
            string text = "";
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
            text += "TOTAL REVENUE BY MONTH: " + _revenue + System.Environment.NewLine;
            string choice;
            while (true)
            {
                Console.WriteLine("\nDo you want to save this report? (Y/N): ");
                choice = Console.ReadLine();
                if (choice == "Y" || choice == "y" || choice == "N" || choice == "n")
                {
                    break;
                }
                else Console.WriteLine("Please Enter Valid Choice");
            }
            if (choice == "Y" || choice == "y")
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Report.txt", true);
                try
                {
                    sw.WriteLine("Revenue By Year (Date: " + DateTime.Now.ToString() + "): ");
                    sw.WriteLine();
                    sw.Write(text);
                    sw.WriteLine();
                    sw.WriteLine("===========================================================================");
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
