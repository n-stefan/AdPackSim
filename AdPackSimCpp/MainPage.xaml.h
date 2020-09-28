//
// MainPage.xaml.h
// Declaration of the MainPage class.
//

#pragma once

#include "MainPage.g.h"

using namespace std;
using namespace Platform;
using namespace Windows::UI::Xaml;

namespace AdPackSimCpp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public ref class MainPage sealed
	{
	public:
		MainPage();

	private:
		double profit = 0;
		const double packPrice = 25;
		const double packReturn = 30;
		const int maxPacks = 3000;

		void Calc(double returnPerDayPerPack, int totalDays, int reinvestingDays, int initialPacks, bool updateControls);

		void Calculate_Click(Object^ sender, RoutedEventArgs^ e);
		void OptimalTotalDays_Click(Object^ sender, RoutedEventArgs^ e);
		void OptimalReinvestingDays_Click(Object^ sender, RoutedEventArgs^ e);
		void OptimalInitialPacks_Click(Object^ sender, RoutedEventArgs^ e);
		void ToROL_Click(Object^ sender, RoutedEventArgs^ e);

		String^ ToString(double value, char format);
	};
}
