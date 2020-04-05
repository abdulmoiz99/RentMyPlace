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


        }
        public static void historicalCustomerRentals()
        {
            string[] names = new string[getTotalLines()];
            int index = 0;
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\transactions.txt");
            foreach (string log in data)
            {
                string[] temp = log.Split('\t');
                names[index] =temp[2];
                index++;
            }
            Array.Sort(names);
            names = names.Distinct().ToArray();
            for (int i = 0; i < names.Length; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    if (data[j].Contains(names[i]))
                    {
                        Console.WriteLine(data[j]);
                    }
                }
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
    }
}
