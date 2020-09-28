
//#define DECIMAL //decimal 128-bit
//#define DOUBLE //double 64-bit
//#define FLOAT //float 32-bit

namespace AdPackSimLib
{
    class AdPack
    {
#if DECIMAL
        public decimal Value { get; set; }
#elif DOUBLE
        public double Value { get; set; }
#elif FLOAT
        public float Value { get; set; }
#endif
        public bool IsActive { get; set; }

        public AdPack()
        {
#if DECIMAL
            Value = 0m;
#elif DOUBLE
            Value = 0d;
#elif FLOAT
            Value = 0f;
#endif
            IsActive = true;
        }
    }
}
