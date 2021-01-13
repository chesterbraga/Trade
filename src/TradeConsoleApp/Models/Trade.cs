using System;

namespace TradeConsoleApp.Models
{
    public class Trade : ITrade
    {
        public double Value { get; set; }

        public ClientSectorEnum ClientSector { get; set; }

        public DateTime NextPaymentDate { get; set; }

        public bool IsPoliticallyExposed { get; set; }

        public ClientSectorEnum ClientSectorStringToEnum(string clientSectorStr)
        {
            if (clientSectorStr == "Private")
            {
                return ClientSectorEnum.Private;
            }
            else
            {
                return ClientSectorEnum.Public;
            }
        }

        public CategoryEnum GetCategory(DateTime referenceDate)
        {
            if (IsPoliticallyExposed)
            {
                return CategoryEnum.PEP;
            }
            else
            {
                TimeSpan diff = referenceDate - NextPaymentDate;

                if (diff.Days > 30)
                {
                    return CategoryEnum.DEFAULTED;
                }
                else if (Value > 1000000)
                {
                    if (ClientSector == ClientSectorEnum.Private)
                    {
                        return CategoryEnum.HIGHRISK;
                    }
                    else
                    {
                        return CategoryEnum.MEDIUMRISK;
                    }
                }
                else
                {
                    return CategoryEnum.NONE;
                }
            }
        }
    }
}