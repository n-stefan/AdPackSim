
//#define DECIMAL //decimal 128-bit
#define DOUBLE //double 64-bit
//#define FLOAT //float 32-bit

namespace AdPackSimCs
{
    struct AdPackStruct
    {
#if DECIMAL
        public decimal Value { get; set; }
#elif DOUBLE
        public double Value { get; set; }
#elif FLOAT
        public float Value { get; set; }
#endif
        public bool IsActive { get; set; }

        //public static AdPackStruct New
        //{
        //    get
        //    {
        //        return new AdPackStruct { /*Value = 0d,*/ IsActive = true };
        //    }
        //}
    }
}
