using AdPackSimLib;
using AdPackSimLib.Calculators;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AdPackSimBlazor.Client.Pages
{
    public class Serverless : ComponentBase
    {
        protected ICalculator Calculator;

        protected Data Data = Data.Default;

        protected Task OptimalTotalDays()
        {
            Data = Calculator.OptimalTotalDays(Data);
            return Task.CompletedTask;
        }

        protected Task OptimalReinvestingDays()
        {
            Data = Calculator.OptimalReinvestingDays(Data);
            return Task.CompletedTask;
        }

        protected Task OptimalInitialPacks()
        {
            Data = Calculator.OptimalInitialPacks(Data);
            return Task.CompletedTask;
        }

        protected Task Calculate()
        {
            Data = Calculator.Calculate(Data);
            return Task.CompletedTask;
        }

        protected Task ToROL()
        {
            Data = Calculator.ToROL(Data);
            return Task.CompletedTask;
        }
    }
}
