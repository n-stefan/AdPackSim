
namespace AdPackSimLib.Calculators
{
    public interface ICalculator
    {
        Data Calculate(Data input);
        Data OptimalTotalDays(Data input);
        Data OptimalReinvestingDays(Data input);
        Data OptimalInitialPacks(Data input);
        Data ToROL(Data input);
    }
}
