using System.Diagnostics;
using System.Globalization;

namespace AdPackSimLib.Calculators
{
    public abstract class Calculator
    {
#if DECIMAL
        protected decimal profit;
#elif DOUBLE
        protected double profit;
#elif FLOAT
        protected float profit;
#endif

#if DECIMAL
        protected abstract Data Calc(decimal returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, decimal exchangeRate, bool updateControls);
#elif DOUBLE
        //TODO: Try (ref struct) Data input
        //TODO: Try generics
        protected abstract Data Calc(double returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, double exchangeRate, bool updateControls);
#elif FLOAT
        protected abstract Data Calc(float returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, float exchangeRate, bool updateControls);
#endif

        public Data Calculate(Data input)
        {
#if DECIMAL
            var returnPerDayPerPack = decimal.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = decimal.Parse(input.ExchangeRate);
#elif DOUBLE
            var returnPerDayPerPack = double.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = double.Parse(input.ExchangeRate);
#elif FLOAT
            var returnPerDayPerPack = float.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = float.Parse(input.ExchangeRate);
#endif
            var totalDays = int.Parse(input.TotalDays);
            var reinvestingDays = int.Parse(input.ReinvestingDays);
            var initialPacks = int.Parse(input.InitialPacks);

            return Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, exchangeRate, true);
        }

        public Data OptimalTotalDays(Data input)
        {
#if DECIMAL
            var returnPerDayPerPack = decimal.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = decimal.Parse(input.ExchangeRate);
            var maxProfit = 0m;
#elif DOUBLE
            var returnPerDayPerPack = double.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = double.Parse(input.ExchangeRate);
            var maxProfit = 0d;
#elif FLOAT
            var returnPerDayPerPack = float.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = float.Parse(input.ExchangeRate);
            var maxProfit = 0f;
#endif
            var reinvestingDays = int.Parse(input.ReinvestingDays);
            var initialPacks = int.Parse(input.InitialPacks);
            var optimalTotalDays = 1;

            var sw = new Stopwatch();
            sw.Start();

            for (var totalDays = 1; totalDays <= 365; totalDays++)
            {
                Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, exchangeRate, false);

                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    optimalTotalDays = totalDays;
                }
            }

            var output = Calc(returnPerDayPerPack, optimalTotalDays, reinvestingDays, initialPacks, exchangeRate, true);

            sw.Stop();

            output.Timer1 = sw.ElapsedMilliseconds.ToString("N0");

            return output;
        }

        public Data OptimalReinvestingDays(Data input)
        {
#if DECIMAL
            var returnPerDayPerPack = decimal.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = decimal.Parse(input.ExchangeRate);
            var maxProfit = 0m;
#elif DOUBLE
            var returnPerDayPerPack = double.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = double.Parse(input.ExchangeRate);
            var maxProfit = 0d;
#elif FLOAT
            var returnPerDayPerPack = float.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = float.Parse(input.ExchangeRate);
            var maxProfit = 0f;
#endif
            var totalDays = int.Parse(input.TotalDays);
            var initialPacks = int.Parse(input.InitialPacks);
            var optimalReinvestingDays = 1;

            var sw = new Stopwatch();
            sw.Start();

            for (var reinvestingDays = 1; reinvestingDays <= totalDays; reinvestingDays++)
            {
                Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, exchangeRate, false);

                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    optimalReinvestingDays = reinvestingDays;
                }
            }

            var output = Calc(returnPerDayPerPack, totalDays, optimalReinvestingDays, initialPacks, exchangeRate, true);

            sw.Stop();

            output.Timer2 = sw.ElapsedMilliseconds.ToString("N0");

            return output;
        }

        public Data OptimalInitialPacks(Data input)
        {
#if DECIMAL
            var returnPerDayPerPack = decimal.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = decimal.Parse(input.ExchangeRate);
            var maxProfit = 0m;
#elif DOUBLE
            var returnPerDayPerPack = double.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = double.Parse(input.ExchangeRate);
            var maxProfit = 0d;
#elif FLOAT
            var returnPerDayPerPack = float.Parse(input.ReturnPerDayPerPack);
            var exchangeRate = float.Parse(input.ExchangeRate);
            var maxProfit = 0f;
#endif
            var reinvestingDays = int.Parse(input.ReinvestingDays);
            var totalDays = int.Parse(input.TotalDays);
            var optimalInitialPacks = 1;

            var sw = new Stopwatch();
            sw.Start();

            for (var initialPacks = 1; initialPacks <= 500; initialPacks++)
            {
                Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, exchangeRate, false);

                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    optimalInitialPacks = initialPacks;
                }
            }

            var output = Calc(returnPerDayPerPack, totalDays, reinvestingDays, optimalInitialPacks, exchangeRate, true);

            sw.Stop();

            output.Timer3 = sw.ElapsedMilliseconds.ToString("N0");

            return output;
        }

        public Data ToROL(Data input)
        {
#if DECIMAL
            var exchangeRate = decimal.Parse(input.ExchangeRate) * 10000;
            var amountInEUR = 0m;
#elif DOUBLE
            var exchangeRate = double.Parse(input.ExchangeRate) * 10000;
            var amountInEUR = 0d;
#elif FLOAT
            var exchangeRate = float.Parse(input.ExchangeRate) * 10000;
            var amountInEUR = 0f;
#endif

            if (!string.IsNullOrWhiteSpace(input.Total))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(input.Total, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(input.Total, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(input.Total, NumberStyles.Currency);
#endif
                input.TotalROL = (amountInEUR * exchangeRate).ToString("N");
            }

            if (!string.IsNullOrWhiteSpace(input.InitialInvestment))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(input.InitialInvestment, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(input.InitialInvestment, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(input.InitialInvestment, NumberStyles.Currency);
#endif
                input.InitialInvestmentROL = (amountInEUR * exchangeRate).ToString("N");
            }

            if (!string.IsNullOrWhiteSpace(input.Profit))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(input.Profit, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(input.Profit, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(input.Profit, NumberStyles.Currency);
#endif
                input.ProfitROL = (amountInEUR * exchangeRate).ToString("N");
            }

            if (!string.IsNullOrWhiteSpace(input.PeakReturnPerDay))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(input.PeakReturnPerDay, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(input.PeakReturnPerDay, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(input.PeakReturnPerDay, NumberStyles.Currency);
#endif
                input.PeakReturnPerDayROL = (amountInEUR * exchangeRate).ToString("N");
            }

            if (!string.IsNullOrWhiteSpace(input.PeakReturnPerMonth))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(input.PeakReturnPerMonth, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(input.PeakReturnPerMonth, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(input.PeakReturnPerMonth, NumberStyles.Currency);
#endif
                input.PeakReturnPerMonthROL = (amountInEUR * exchangeRate).ToString("N");
            }

            return input;
        }
    }
}
