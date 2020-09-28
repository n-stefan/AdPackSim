
//#define DECIMAL //decimal 128-bit
//#define DOUBLE //double 64-bit
//#define FLOAT //float 32-bit

using System.Collections.Generic;

namespace AdPackSimLib.Calculators
{
    public class GenericList : Calculator, IGenericList
    {
        private List<AdPack> packs = new List<AdPack>();
        private const int maxPacks = 3000;
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
        protected override Data Calc(decimal returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, decimal exchangeRate, bool updateControls)
#elif DOUBLE
        protected override Data Calc(double returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, double exchangeRate, bool updateControls)
#elif FLOAT
        protected override Data Calc(float returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, float exchangeRate, bool updateControls)
#endif
        {
            var initialInvestment = initialPacks * packPrice;
            var activePacks = initialPacks;
            var maxActivePacks = initialPacks;
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

            packs.Clear();

            for (var i = 0; i < initialPacks; i++) packs.Add(new AdPack());

            for (var i = 0; i < totalDays; i++)
            {
                for (var j = 0; j < packs.Count; j++)
                {
                    if (!packs[j].IsActive) continue;

                    total += returnPerDayPerPack;
                    if (total >= packPrice && i < reinvestingDays && activePacks < maxPacks)
                    {
                        packs.Add(new AdPack());
                        total -= packPrice;
                        activePacks++;
                    }

                    packs[j].Value += returnPerDayPerPack;
                    if (packs[j].Value >= packReturn)
                    {
                        packs[j].IsActive = false;
                        activePacks--;
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
