using System.Text;

namespace AdPackSimLib
{
    public class Data
    {
        public string TotalDays { get; set; }
        public string ReinvestingDays { get; set; }
        public string InitialPacks { get; set; }
        public string ReturnPerDayPerPack { get; set; }
        public string ExchangeRate { get; set; }
        public string MaxActivePacks { get; set; }
        public string ActivePacks { get; set; }
        public string Total { get; set; }
        public string InitialInvestment { get; set; }
        public string Profit { get; set; }
        public string PeakReturnPerDay { get; set; }
        public string PeakReturnPerMonth { get; set; }
        public string ProfitPercent { get; set; }
        public string TotalROL { get; set; }
        public string InitialInvestmentROL { get; set; }
        public string ProfitROL { get; set; }
        public string PeakReturnPerDayROL { get; set; }
        public string PeakReturnPerMonthROL { get; set; }
        public string TotalPercent { get; set; }
        public string Timer1 { get; set; }
        public string Timer2 { get; set; }
        public string Timer3 { get; set; }

        public static Data Default
        {
            get
            {
                return new Data
                {
                    TotalDays = "365",
                    ReinvestingDays = "200",
                    InitialPacks = "100",
                    ReturnPerDayPerPack = "0.185",
                    ExchangeRate = "4.5"
                };
            }
        }

        public string ToQueryString()
        {
            var queryString = new StringBuilder("?");
            queryString.Append($"{nameof(TotalDays)}={TotalDays}&");
            queryString.Append($"{nameof(ReinvestingDays)}={ReinvestingDays}&");
            queryString.Append($"{nameof(InitialPacks)}={InitialPacks}&");
            queryString.Append($"{nameof(ReturnPerDayPerPack)}={ReturnPerDayPerPack}&");
            queryString.Append($"{nameof(ExchangeRate)}={ExchangeRate}&");
            queryString.Append($"{nameof(MaxActivePacks)}={MaxActivePacks}&");
            queryString.Append($"{nameof(ActivePacks)}={ActivePacks}&");
            queryString.Append($"{nameof(Total)}={Total}&");
            queryString.Append($"{nameof(InitialInvestment)}={InitialInvestment}&");
            queryString.Append($"{nameof(Profit)}={Profit}&");
            queryString.Append($"{nameof(PeakReturnPerDay)}={PeakReturnPerDay}&");
            queryString.Append($"{nameof(PeakReturnPerMonth)}={PeakReturnPerMonth}&");
            queryString.Append($"{nameof(ProfitPercent)}={ProfitPercent}&");
            queryString.Append($"{nameof(TotalROL)}={TotalROL}&");
            queryString.Append($"{nameof(InitialInvestmentROL)}={InitialInvestmentROL}&");
            queryString.Append($"{nameof(ProfitROL)}={ProfitROL}&");
            queryString.Append($"{nameof(PeakReturnPerDayROL)}={PeakReturnPerDayROL}&");
            queryString.Append($"{nameof(PeakReturnPerMonthROL)}={PeakReturnPerMonthROL}&");
            queryString.Append($"{nameof(TotalPercent)}={TotalPercent}&");
            queryString.Append($"{nameof(Timer1)}={Timer1}&");
            queryString.Append($"{nameof(Timer2)}={Timer2}&");
            queryString.Append($"{nameof(Timer3)}={Timer3}");
            return queryString.ToString();
        }
    }
}
