//
// MainPage.xaml.cpp
// Implementation of the MainPage class.
//

#include "pch.h"
#include "MainPage.xaml.h"
#include "AdPack.h"

using namespace AdPackSimCpp;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

MainPage::MainPage()
{
	InitializeComponent();
}

void MainPage::Calc(double returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, bool updateControls)
{
	double initialInvestment = initialPacks * packPrice;
	int activePacks = initialPacks;
	int maxActivePacks = initialPacks;
	double total, profitPercent, totalPercent, peakReturnPerDay, peakReturnPerMonth;
	total = profitPercent = peakReturnPerDay = peakReturnPerMonth = 0;

	vector<AdPack> packs;

	for (int i = 0; i < initialPacks; i++) packs.push_back(AdPack());

	for (int i = 0; i < totalDays; i++)
	{
		for (vector<AdPack>::size_type j = 0; j < packs.size(); ++j)
		{
			if (!packs[j].IsActive) continue;

			total += returnPerDayPerPack;
			if (total >= packPrice && i < reinvestingDays && activePacks < maxPacks)
			{
				packs.push_back(AdPack());
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
		profitPercent = (profit * 100) / initialInvestment;
		totalPercent = (total * 100) / initialInvestment;
		peakReturnPerDay = returnPerDayPerPack * maxActivePacks;
		peakReturnPerMonth = peakReturnPerDay * 31;

		Total->Text = ToString(total, 'C');
		InitialInvestment->Text = ToString(initialInvestment, 'C');
		Profit->Text = ToString(profit, 'C');
		ProfitPercent->Text = ToString(profitPercent, 'P');
		TotalPercent->Text = ToString(totalPercent, 'P');
		ActivePacks->Text = activePacks.ToString();
		MaxActivePacks->Text = maxActivePacks.ToString();
		PeakReturnPerDay->Text = ToString(peakReturnPerDay, 'C');
		PeakReturnPerMonth->Text = ToString(peakReturnPerMonth, 'C');
	}
}

void MainPage::Calculate_Click(Object^ sender, RoutedEventArgs^ e)
{
	double returnPerDayPerPack = _wtof(ReturnPerDayPerPack->Text->Data());
	int totalDays = _wtoi(TotalDays->Text->Data());
	int reinvestingDays = _wtoi(ReinvestingDays->Text->Data());
	int initialPacks = _wtoi(InitialPacks->Text->Data());

	Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, true);
}

void MainPage::OptimalTotalDays_Click(Object^ sender, RoutedEventArgs^ e)
{
	double returnPerDayPerPack = _wtof(ReturnPerDayPerPack->Text->Data());
	int reinvestingDays = _wtoi(ReinvestingDays->Text->Data());
	int initialPacks = _wtoi(InitialPacks->Text->Data());
	double maxProfit = 0;
	int optimalTotalDays = 1;

	unsigned int start = clock();

	for (int totalDays = 1; totalDays <= 365; totalDays++)
	{
		Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, false);

		if (profit > maxProfit)
		{
			maxProfit = profit;
			optimalTotalDays = totalDays;
		}
	}

	TotalDays->Text = optimalTotalDays.ToString();

	Calc(returnPerDayPerPack, optimalTotalDays, reinvestingDays, initialPacks, true);

	unsigned int stop = clock() - start;

	Timer1->Text = stop.ToString();
}

void MainPage::OptimalReinvestingDays_Click(Object^ sender, RoutedEventArgs^ e)
{
	double returnPerDayPerPack = _wtof(ReturnPerDayPerPack->Text->Data());
	int totalDays = _wtoi(TotalDays->Text->Data());
	int initialPacks = _wtoi(InitialPacks->Text->Data());
	double maxProfit = 0;
	int optimalReinvestingDays = 1;

	unsigned int start = clock();

	for (int reinvestingDays = 1; reinvestingDays <= totalDays; reinvestingDays++)
	{
		Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, false);

		if (profit > maxProfit)
		{
			maxProfit = profit;
			optimalReinvestingDays = reinvestingDays;
		}
	}

	ReinvestingDays->Text = optimalReinvestingDays.ToString();

	Calc(returnPerDayPerPack, totalDays, optimalReinvestingDays, initialPacks, true);

	unsigned int stop = clock() - start;

	Timer2->Text = stop.ToString();
}

void MainPage::OptimalInitialPacks_Click(Object^ sender, RoutedEventArgs^ e)
{
	double returnPerDayPerPack = _wtof(ReturnPerDayPerPack->Text->Data());
	int totalDays = _wtoi(TotalDays->Text->Data());
	int reinvestingDays = _wtoi(ReinvestingDays->Text->Data());
	double maxProfit = 0;
	int optimalInitialPacks = 1;

	unsigned int start = clock();

	for (int initialPacks = 1; initialPacks <= 500; initialPacks++)
	{
		Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, false);

		if (profit > maxProfit)
		{
			maxProfit = profit;
			optimalInitialPacks = initialPacks;
		}
	}

	InitialPacks->Text = optimalInitialPacks.ToString();

	Calc(returnPerDayPerPack, totalDays, reinvestingDays, optimalInitialPacks, true);

	unsigned int stop = clock() - start;

	Timer3->Text = stop.ToString();
}

void MainPage::ToROL_Click(Object^ sender, RoutedEventArgs^ e)
{
	double amountInEUR;
	double exchangeRate = _wtof(ExchangeRate->Text->Data()) * 10000;

	if (!Total->Text->IsEmpty())
	{
		amountInEUR = _wtof(Total->Text->Data());
		TotalROL->Text = ToString(amountInEUR * exchangeRate, 'N');
	}

	if (!InitialInvestment->Text->IsEmpty())
	{
		amountInEUR = _wtof(InitialInvestment->Text->Data());
		InitialInvestmentROL->Text = ToString(amountInEUR * exchangeRate, 'N');
	}

	if (!Profit->Text->IsEmpty())
	{
		amountInEUR = _wtof(Profit->Text->Data());
		ProfitROL->Text = ToString(amountInEUR * exchangeRate, 'N');
	}

	if (!PeakReturnPerDay->Text->IsEmpty())
	{
		amountInEUR = _wtof(PeakReturnPerDay->Text->Data());
		PeakReturnPerDayROL->Text = ToString(amountInEUR * exchangeRate, 'N');
	}

	if (!PeakReturnPerMonth->Text->IsEmpty())
	{
		amountInEUR = _wtof(PeakReturnPerMonth->Text->Data());
		PeakReturnPerMonthROL->Text = ToString(amountInEUR * exchangeRate, 'N');
	}
}

//TODO: Thousands separator
String^ MainPage::ToString(double value, char format)
{
	wchar_t buffer[50];

	if (format == 'N')
		swprintf(buffer, 50, L"%.2f", value);
	else if (format == 'C')
		swprintf(buffer, 50, L"%.2f €", value);
	else if (format == 'P')
		swprintf(buffer, 50, L"%.2f %%", value);

	return ref new String(buffer);
}
