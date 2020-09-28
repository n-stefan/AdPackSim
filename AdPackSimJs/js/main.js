
var profit = 0;
var packPrice = 25;
var packReturn = 30;
var packsLimit = 3000;
var packsSize = 6000; //10000;
var packs = [];

var currencyEurFormat = new Intl.NumberFormat('de-DE', {
    currency: 'EUR',
    style: 'currency',
    minimumFractionDigits: 2
});

var currencyRolFormat = new Intl.NumberFormat('ro-RO', {
    currency: 'ROL',
    style: 'currency',
    minimumFractionDigits: 2
});

var percentFormat = new Intl.NumberFormat('en-US', {
    style: 'percent',
    minimumFractionDigits: 2
});

var integerFormat = new Intl.NumberFormat('en-US', {
    style: 'decimal',
    minimumFractionDigits: 0
});

document.addEventListener('DOMContentLoaded', function() {
    document.getElementById('OptimalTotalDays').addEventListener('click', OptimalTotalDays_Click, false);
    document.getElementById('OptimalReinvestingDays').addEventListener('click', OptimalReinvestingDays_Click, false);
    document.getElementById('OptimalInitialPacks').addEventListener('click', OptimalInitialPacks_Click, false);
    document.getElementById('Calculate').addEventListener('click', Calculate_Click, false);
    document.getElementById('ToROL').addEventListener('click', ToROL_Click, false);
}, false);

function Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, updateControls)
{
    var initialInvestment = initialPacks * packPrice;
    var activePacks = initialPacks;
    var maxActivePacks = initialPacks;
    var allPacks = initialPacks;
    var total, profitPercent, totalPercent, peakReturnPerDay, peakReturnPerMonth;
    total = 0;

    //for (var i = 0, packs = new Array(packsSize); i < packsSize;) packs[i++] = 0;
    for (var i = 0; i < packsSize; i++) packs[i] = 0;

    for (var i = 0; i < totalDays; i++)
    {
        for (var j = 0; j < allPacks; j++)
        {
            if (packs[j] == -1) continue;

            total += returnPerDayPerPack;
            if (total >= packPrice && i < reinvestingDays && activePacks < packsLimit)
            {
                total -= packPrice;
                allPacks++;
                activePacks++;
            }

            packs[j] += returnPerDayPerPack;
            if (packs[j] >= packReturn)
            {
                packs[j] = -1;
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

        document.getElementById('Total').value = currencyEurFormat.format(total); //ToString(total, 'C');
        document.getElementById('InitialInvestment').value = currencyEurFormat.format(initialInvestment); //ToString(initialInvestment, 'C');
        document.getElementById('Profit').value = currencyEurFormat.format(profit); //ToString(profit, 'C');
        document.getElementById('ProfitPercent').value = percentFormat.format(profitPercent); //ToString(profitPercent, 'P');
        document.getElementById('TotalPercent').value = percentFormat.format(totalPercent); //ToString(totalPercent, 'P');
        document.getElementById('ActivePacks').value = integerFormat.format(activePacks); //ToString(activePacks, 'D');
        document.getElementById('MaxActivePacks').value = integerFormat.format(maxActivePacks); //ToString(maxActivePacks, 'D');
        document.getElementById('PeakReturnPerDay').value = currencyEurFormat.format(peakReturnPerDay); //ToString(peakReturnPerDay, 'C');
        document.getElementById('PeakReturnPerMonth').value = currencyEurFormat.format(peakReturnPerMonth); //ToString(peakReturnPerMonth, 'C');
    }
}

function OptimalTotalDays_Click(event) {
    var returnPerDayPerPack = parseFloat(document.getElementById('ReturnPerDayPerPack').value);
    var reinvestingDays = parseInt(document.getElementById('ReinvestingDays').value, 10);
    var initialPacks = parseInt(document.getElementById('InitialPacks').value, 10);
    var maxProfit = 0;
    var optimalTotalDays = 1;

    var date = new Date();

    for (var totalDays = 1; totalDays <= 365; totalDays++)
    {
        Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, false);

        if (profit > maxProfit)
        {
            maxProfit = profit;
            optimalTotalDays = totalDays;
        }
    }

    document.getElementById('TotalDays').value = optimalTotalDays;

    Calc(returnPerDayPerPack, optimalTotalDays, reinvestingDays, initialPacks, true);

    var elapsed = new Date() - date;

    document.getElementById('Timer1').value = elapsed;
}

function OptimalReinvestingDays_Click(event) {
    var returnPerDayPerPack = parseFloat(document.getElementById('ReturnPerDayPerPack').value);
    var totalDays = parseInt(document.getElementById('TotalDays').value, 10);
    var initialPacks = parseInt(document.getElementById('InitialPacks').value, 10);
    var maxProfit = 0;
    var optimalReinvestingDays = 1;

    var date = new Date();

    for (var reinvestingDays = 1; reinvestingDays <= totalDays; reinvestingDays++)
    {
        Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, false);

        if (profit > maxProfit)
        {
            maxProfit = profit;
            optimalReinvestingDays = reinvestingDays;
        }
    }

    document.getElementById('ReinvestingDays').value = optimalReinvestingDays;

    Calc(returnPerDayPerPack, totalDays, optimalReinvestingDays, initialPacks, true);

    var elapsed = new Date() - date;

    document.getElementById('Timer2').value = elapsed;
}

function OptimalInitialPacks_Click(event) {
    var returnPerDayPerPack = parseFloat(document.getElementById('ReturnPerDayPerPack').value);
    var totalDays = parseInt(document.getElementById('TotalDays').value, 10);
    var reinvestingDays = parseInt(document.getElementById('ReinvestingDays').value, 10);
    var maxProfit = 0;
    var optimalInitialPacks = 1;

    var date = new Date();

    for (var initialPacks = 1; initialPacks <= 500; initialPacks++)
    {
        Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, false);
            
        if (profit > maxProfit)
        {
            maxProfit = profit;
            optimalInitialPacks = initialPacks;
        }
    }

    document.getElementById('InitialPacks').value = optimalInitialPacks;

    Calc(returnPerDayPerPack, totalDays, reinvestingDays, optimalInitialPacks, true);

    var elapsed = new Date() - date;

    document.getElementById('Timer3').value = elapsed;
}

function Calculate_Click(event) {
    var returnPerDayPerPack = parseFloat(document.getElementById('ReturnPerDayPerPack').value);
    var totalDays = parseInt(document.getElementById('TotalDays').value, 10);
    var reinvestingDays = parseInt(document.getElementById('ReinvestingDays').value, 10);
    var initialPacks = parseInt(document.getElementById('InitialPacks').value, 10);

    Calc(returnPerDayPerPack, totalDays, reinvestingDays, initialPacks, true);
}

function ToROL_Click(event) {
    var amountInEUR;
    var exchangeRate = parseFloat(document.getElementById('ExchangeRate').value) * 10000;

    var total = document.getElementById('Total').value;
    if (total != '') {
        amountInEUR = parseFloat(Sanitize(total));
        document.getElementById('TotalROL').value = currencyRolFormat.format(amountInEUR * exchangeRate); //ToString(amountInEUR * exchangeRate, 'N');
    }

    var initialInvestment = document.getElementById('InitialInvestment').value;
    if (initialInvestment != '') {
        amountInEUR = parseFloat(Sanitize(initialInvestment));
        document.getElementById('InitialInvestmentROL').value = currencyRolFormat.format(amountInEUR * exchangeRate); //ToString(amountInEUR * exchangeRate, 'N');
    }

    var profit = document.getElementById('Profit').value;
    if (profit != '') {
        amountInEUR = parseFloat(Sanitize(profit));
        document.getElementById('ProfitROL').value = currencyRolFormat.format(amountInEUR * exchangeRate); //ToString(amountInEUR * exchangeRate, 'N');
    }

    var peakReturnPerDay = document.getElementById('PeakReturnPerDay').value;
    if (peakReturnPerDay != '') {
        amountInEUR = parseFloat(Sanitize(peakReturnPerDay));
        document.getElementById('PeakReturnPerDayROL').value = currencyRolFormat.format(amountInEUR * exchangeRate); //ToString(amountInEUR * exchangeRate, 'N');
    }

    var peakReturnPerMonth = document.getElementById('PeakReturnPerMonth').value;
    if (peakReturnPerMonth != '') {
        amountInEUR = parseFloat(Sanitize(peakReturnPerMonth));
        document.getElementById('PeakReturnPerMonthROL').value = currencyRolFormat.format(amountInEUR * exchangeRate); //ToString(amountInEUR * exchangeRate, 'N');
    }
}

function Sanitize(value) {
    value = value.slice(0, -2);
    value = value.replace(/\./g, '');
    value = value.replace(',', '.');
    return value;
}

//function ToString(value, format)
//{
//    if (format == 'D') return value.toFixed(0);
//    else if (format == 'N') return value.toFixed(2);
//    else if (format == 'C') return value.toFixed(2) + ' €';
//    else if (format == 'P') return value.toFixed(2) + ' %';
//}
