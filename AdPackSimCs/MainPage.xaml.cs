﻿
//#define DECIMAL //decimal 128-bit
#define DOUBLE //double 64-bit
//#define FLOAT //float 32-bit

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdPackSimCs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<AdPack> packs = new List<AdPack>();
        //private List<ChartData> chartData = new List<ChartData>();
#if DECIMAL
        private decimal profit;
        private const decimal packPrice = 25m;
        private const decimal packReturn = 30m;
#elif DOUBLE
        private double profit;
        private const double packPrice = 25d;
        private const double packReturn = 30d;
#elif FLOAT
        private float profit;
        private const float packPrice = 25f;
        private const float packReturn = 30f;
#endif
        private const int maxPacks = 3000;

        public MainPage()
        {
            InitializeComponent();
        }

        //TODO: use Task?
#if DECIMAL
        private void Calc(decimal returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, bool updateControls,
            bool drawChart)
#elif DOUBLE
        private void Calc(double returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, bool updateControls,
            bool drawChart)
#elif FLOAT
        private void Calc(float returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, bool updateControls,
            bool drawChart)
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
            //chartData.Clear();

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

                //if (drawChart) chartData.Add(new ChartData { Day = i + 1, Total = total, Profit = Math.Max(total - initialInvestment, 0) });
            }

            //if (drawChart)
            //{
            //    (ChartTotal.Series[0] as AreaSeries).ItemsSource = chartData;
            //    (ChartTotal.Series[1] as AreaSeries).ItemsSource = chartData;
            //}

            profit = total - initialInvestment;

            if (updateControls)
            {
                profitPercent = profit / initialInvestment;
                totalPercent = total / initialInvestment;
                peakReturnPerDay = returnPerDayPerPack * maxActivePacks;
                peakReturnPerMonth = peakReturnPerDay * 31;

                Total.Text = total.ToString("C");
                InitialInvestment.Text = initialInvestment.ToString("C");
                Profit.Text = profit.ToString("C");
                ProfitPercent.Text = profitPercent.ToString("P");
                TotalPercent.Text = totalPercent.ToString("P");
                ActivePacks.Text = activePacks.ToString("D");
                MaxActivePacks.Text = maxActivePacks.ToString("D");
                PeakReturnPerDay.Text = peakReturnPerDay.ToString("C");
                PeakReturnPerMonth.Text = peakReturnPerMonth.ToString("C");
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
#if DECIMAL
            var returnPerDayPerPack = decimal.Parse(ReturnPerDayPerPack.Text);
#elif DOUBLE
            var returnPerDayPerPack = double.Parse(ReturnPerDayPerPack.Text);
#elif FLOAT
            var returnPerDayPerPack = float.Parse(ReturnPerDayPerPack.Text);
#endif
            var totalDays = int.Parse(TotalDays.Text);
            var reinvestingDays = int.Parse(ReinvestingDays.Text);
            var initialPacks = int.Parse(InitialPacks.Text);
            var drawChart = DrawChart.IsChecked.Value;

            Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, true, drawChart);
        }

        private void OptimalTotalDays_Click(object sender, RoutedEventArgs e)
        {
#if DECIMAL
            var maxProfit = 0m;
            var returnPerDayPerPack = decimal.Parse(ReturnPerDayPerPack.Text);
#elif DOUBLE
            var maxProfit = 0d;
            var returnPerDayPerPack = double.Parse(ReturnPerDayPerPack.Text);
#elif FLOAT
            var maxProfit = 0f;
            var returnPerDayPerPack = float.Parse(ReturnPerDayPerPack.Text);
#endif
            var reinvestingDays = int.Parse(ReinvestingDays.Text);
            var initialPacks = int.Parse(InitialPacks.Text);
            var drawChart = DrawChart.IsChecked.Value;
            var optimalTotalDays = 1;

            var sw = new Stopwatch();
            sw.Start();

            for (var totalDays = 1; totalDays <= 365; totalDays++)
            {
                Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, false, false);

                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    optimalTotalDays = totalDays;
                }
            }

            TotalDays.Text = optimalTotalDays.ToString();

            Calc(returnPerDayPerPack, optimalTotalDays, reinvestingDays, initialPacks, true, drawChart);

            sw.Stop();
            Timer1.Text = sw.ElapsedMilliseconds.ToString("N0");
        }

        private void OptimalReinvestingDays_Click(object sender, RoutedEventArgs e)
        {
#if DECIMAL
            var maxProfit = 0m;
            var returnPerDayPerPack = decimal.Parse(ReturnPerDayPerPack.Text);
#elif DOUBLE
            var maxProfit = 0d;
            var returnPerDayPerPack = double.Parse(ReturnPerDayPerPack.Text);
#elif FLOAT
            var maxProfit = 0f;
            var returnPerDayPerPack = float.Parse(ReturnPerDayPerPack.Text);
#endif
            var totalDays = int.Parse(TotalDays.Text);
            var initialPacks = int.Parse(InitialPacks.Text);
            var drawChart = DrawChart.IsChecked.Value;
            var optimalReinvestingDays = 1;

            var sw = new Stopwatch();
            sw.Start();

            for (var reinvestingDays = 1; reinvestingDays <= totalDays; reinvestingDays++)
            {
                Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, false, false);

                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    optimalReinvestingDays = reinvestingDays;
                }
            }

            ReinvestingDays.Text = optimalReinvestingDays.ToString();

            Calc(returnPerDayPerPack, totalDays, optimalReinvestingDays, initialPacks, true, drawChart);

            sw.Stop();
            Timer2.Text = sw.ElapsedMilliseconds.ToString("N0");
        }

        private void OptimalInitialPacks_Click(object sender, RoutedEventArgs e)
        {
#if DECIMAL
            var maxProfit = 0m;
            var returnPerDayPerPack = decimal.Parse(ReturnPerDayPerPack.Text);
#elif DOUBLE
            var maxProfit = 0d;
            var returnPerDayPerPack = double.Parse(ReturnPerDayPerPack.Text);
#elif FLOAT
            var maxProfit = 0f;
            var returnPerDayPerPack = float.Parse(ReturnPerDayPerPack.Text);
#endif
            var totalDays = int.Parse(TotalDays.Text);
            var reinvestingDays = int.Parse(ReinvestingDays.Text);
            var drawChart = DrawChart.IsChecked.Value;
            var optimalInitialPacks = 1;

            var sw = new Stopwatch();
            sw.Start();

            for (var initialPacks = 1; initialPacks <= 500; initialPacks++)
            {
                Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, false, false);

                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    optimalInitialPacks = initialPacks;
                }
            }

            InitialPacks.Text = optimalInitialPacks.ToString();

            Calc(returnPerDayPerPack, totalDays, reinvestingDays, optimalInitialPacks, true, drawChart);

            sw.Stop();
            Timer3.Text = sw.ElapsedMilliseconds.ToString("N0");
        }

        private void ToROL_Click(object sender, RoutedEventArgs e)
        {
#if DECIMAL
            var amountInEUR = 0m;
            var exchangeRate = decimal.Parse(ExchangeRate.Text) * 10000;
#elif DOUBLE
            var amountInEUR = 0d;
            var exchangeRate = double.Parse(ExchangeRate.Text) * 10000;
#elif FLOAT
            var amountInEUR = 0f;
            var exchangeRate = float.Parse(ExchangeRate.Text) * 10000;
#endif
            if (!string.IsNullOrWhiteSpace(Total.Text))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(Total.Text, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(Total.Text, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(Total.Text, NumberStyles.Currency);
#endif
                TotalROL.Text = (amountInEUR * exchangeRate).ToString("N");
            }

            if (!string.IsNullOrWhiteSpace(InitialInvestment.Text))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(InitialInvestment.Text, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(InitialInvestment.Text, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(InitialInvestment.Text, NumberStyles.Currency);
#endif
                InitialInvestmentROL.Text = (amountInEUR * exchangeRate).ToString("N");
            }

            if (!string.IsNullOrWhiteSpace(Profit.Text))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(Profit.Text, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(Profit.Text, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(Profit.Text, NumberStyles.Currency);
#endif
                ProfitROL.Text = (amountInEUR * exchangeRate).ToString("N");
            }

            if (!string.IsNullOrWhiteSpace(PeakReturnPerDay.Text))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(PeakReturnPerDay.Text, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(PeakReturnPerDay.Text, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(PeakReturnPerDay.Text, NumberStyles.Currency);
#endif
                PeakReturnPerDayROL.Text = (amountInEUR * exchangeRate).ToString("N");
            }

            if (!string.IsNullOrWhiteSpace(PeakReturnPerMonth.Text))
            {
#if DECIMAL
                amountInEUR = decimal.Parse(PeakReturnPerMonth.Text, NumberStyles.Currency);
#elif DOUBLE
                amountInEUR = double.Parse(PeakReturnPerMonth.Text, NumberStyles.Currency);
#elif FLOAT
                amountInEUR = float.Parse(PeakReturnPerMonth.Text, NumberStyles.Currency);
#endif
                PeakReturnPerMonthROL.Text = (amountInEUR * exchangeRate).ToString("N");
            }
        }

        private void DrawChart_Click(object sender, RoutedEventArgs e)
        {
            ChartTotal.Visibility = DrawChart.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
