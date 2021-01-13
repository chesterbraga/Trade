using System;

namespace TradeConsoleApp.Models
{
    public interface ITrade
    {
        double Value { get; }
        ClientSectorEnum ClientSector { get; }
        DateTime NextPaymentDate { get; }
        bool IsPoliticallyExposed { get; }
        CategoryEnum GetCategory(DateTime referenceDate);
    }
}