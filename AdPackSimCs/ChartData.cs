
//#define DECIMAL //decimal 128-bit
#define DOUBLE //double 64-bit
//#define FLOAT //float 32-bit

namespace AdPackSimCs
{
    public struct ChartData
    {
        public int Day { get; set; }
#if DECIMAL
        public decimal Total { get; set; }
        public decimal Profit { get; set; }
#elif DOUBLE
        public double Total { get; set; }
        public double Profit { get; set; }
#elif FLOAT
        public float Total { get; set; }
        public float Profit { get; set; }
#endif
        public int ActivePacks { get; set; }
    }
}
