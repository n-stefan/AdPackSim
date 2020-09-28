
//#define DECIMAL //decimal 128-bit
//#define DOUBLE //double 64-bit
//#define FLOAT //float 32-bit

using System;

namespace AdPackSimLib.Calculators
{
    public class UnsafeStructArray : Calculator, IUnsafeStructArray
    {
        private AdPackStruct[] packs = new AdPackStruct[10000];
        private const int packsLimit = 3000;
#if DECIMAL
        private const decimal packPrice = 25m;
        private const decimal packReturn = 30m;
#elif DOUBLE
        private const double packPrice = 25d;
        private const double packReturn = 30d;
#elif FLOAT
        private const float packPrice = 25f;
        private const float packReturn = 30f;
#endif

#if DECIMAL
        unsafe protected override Data Calc(decimal returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, decimal exchangeRate, bool updateControls)
#elif DOUBLE
        unsafe protected override Data Calc(double returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, double exchangeRate, bool updateControls)
#elif FLOAT
        unsafe protected override Data Calc(float returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, float exchangeRate, bool updateControls)
#endif
        {
            var initialInvestment = initialPacks * packPrice;
            var activePacks = initialPacks;
            var maxActivePacks = initialPacks;
            var allPacks = initialPacks;
#if DECIMAL
            decimal total, profitPercent, totalPercent, peakReturnPerDay, peakReturnPerMonth;
            total = 0m;
#elif DOUBLE
            double total, profitPercent, totalPercent, peakReturnPerDay, peakReturnPerMonth;
            total = 0d;
#elif FLOAT
            float total, profitPercent, totalPercent, peakReturnPerDay, peakReturnPerMonth;
            total = 0f;
#endif

            Array.Clear(packs, 0, packs.Length);

            for (var i = 0; i < initialPacks; i++) packs[i].IsActive = true; //= AdPackStruct.New

            for (var i = 0; i < totalDays; i++)
            {
                fixed (AdPackStruct* first = packs) //&packs[0]
                {
                    var current = first;
                    for (var j = 0; j < allPacks; j++, current++)
                    {
                        if (!current->IsActive) continue;

                        total += returnPerDayPerPack;
                        if (total >= packPrice && i < reinvestingDays && activePacks < packsLimit)
                        {
                            total -= packPrice;
                            activePacks++;
                            first[allPacks++].IsActive = true; //= AdPackStruct.New
                        }

                        current->Value += returnPerDayPerPack;
                        if (current->Value >= packReturn)
                        {
                            current->IsActive = false;
                            activePacks--;
                        }
                    }
                }

                if (activePacks > maxActivePacks) maxActivePacks = activePacks;
            }

            profit = total - initialInvestment;

            if (updateControls)
            {
                profitPercent = profit / initialInvestment;
                totalPercent = total / initialInvestment;
                peakReturnPerDay = returnPerDayPerPack * maxActivePacks;
                peakReturnPerMonth = peakReturnPerDay * 31;

                return new Data
                {
                    Total = total.ToString("N"), //total.ToString("C"),
                    InitialInvestment = initialInvestment.ToString("N"), //initialInvestment.ToString("C"),
                    Profit = profit.ToString("N"), //profit.ToString("C"),
                    ProfitPercent = profitPercent.ToString("P"),
                    TotalPercent = totalPercent.ToString("P"),
                    ActivePacks = activePacks.ToString("D"),
                    MaxActivePacks = maxActivePacks.ToString("D"),
                    PeakReturnPerDay = peakReturnPerDay.ToString("N"), //peakReturnPerDay.ToString("C"),
                    PeakReturnPerMonth = peakReturnPerMonth.ToString("N"), //peakReturnPerMonth.ToString("C"),

                    ReturnPerDayPerPack = returnPerDayPerPack.ToString(),
                    TotalDays = totalDays.ToString(),
                    ReinvestingDays = reinvestingDays.ToString(),
                    InitialPacks = initialPacks.ToString(),
                    ExchangeRate = exchangeRate.ToString()
                };
            }
            else
            {
                return null;
            }
        }
    }
}
