using System;
using System.Globalization;
using System.Collections.Generic;
using TradeConsoleApp.Models;

namespace TradeConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (CultureInfo.CurrentCulture.Name != "en-US")
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            }

            Console.WriteLine("input reference date");
            string referenceDate = Console.ReadLine();

            Console.WriteLine("input number of trades");
            int numberTrades = Convert.ToInt32(Console.ReadLine());

            List<Trade> trades = new List<Trade>();

            for (int i = 0; i < numberTrades; i++)
            {
                Console.WriteLine("input trade {0} of {1}", (i + 1), numberTrades);
                string register = Console.ReadLine();
                string[] fields = register.Split(" ");

                Trade trade = new Trade();
                trade.Value = double.Parse(fields[0]);
                trade.ClientSector = trade.ClientSectorStringToEnum(fields[1]);
                trade.NextPaymentDate = DateTime.Parse(fields[2]);

                if (fields.Length > 3)
                {
                    trade.IsPoliticallyExposed = (fields[3] == "1");
                }

                trades.Add(trade);
            }

            Console.WriteLine("");
            Console.WriteLine("----------------------");
            foreach (Trade trade in trades)
            {
                Console.WriteLine(trade.GetCategory(DateTime.Parse(referenceDate)));
            }

            Console.ReadLine();
        }
    }
}
