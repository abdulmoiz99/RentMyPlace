﻿using System;
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
            Console.WriteLine("==RENT MY PLACE==");
            //   Transactions.AddTrasnaction();
            //  Transactions.CheckOut("1");
            Console.WriteLine(Transactions.checkAvaiableity(1));


            Console.ReadLine();
        }
    }
}
