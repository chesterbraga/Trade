using System;
using Xunit;
using TradeConsoleApp.Models;
using System.Globalization;
using System.Collections.Generic;

namespace TradeTests
{
    public class UnitTest
    {
        public static readonly string[] InputTrades = { "2000000 Private 12/29/2025", "400000 Public 07/01/2020", "5000000 Public 01/02/2024", "3000000 Public 10/26/2023", "3000000 Public 10/26/2023 1" };

        [Fact]
        public void TradeTests()
        {
            if (CultureInfo.CurrentCulture.Name != "en-US")
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            }

            List<Trade> trades = new List<Trade>();

            for (int i = 0; i < InputTrades.Length; i++)
            {
                string register = InputTrades[i];
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

            DateTime referenceDate = DateTime.Parse("12/11/2020");

            Assert.Equal("HIGHRISK", trades[0].GetCategory(referenceDate).ToString());
            Assert.Equal("DEFAULTED", trades[1].GetCategory(referenceDate).ToString());
            Assert.Equal("MEDIUMRISK", trades[2].GetCategory(referenceDate).ToString());
            Assert.Equal("MEDIUMRISK", trades[3].GetCategory(referenceDate).ToString());
            Assert.Equal("PEP", trades[4].GetCategory(referenceDate).ToString());
        }
    }
}