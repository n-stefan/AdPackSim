using AdPackSimLib;
using AdPackSimLib.Calculators;
using Microsoft.AspNetCore.Mvc;

namespace AdPackSimBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnsafeStructArrayController : ControllerBase
    {
        private readonly IUnsafeStructArray _unsafeStructArray;

        public UnsafeStructArrayController(IUnsafeStructArray unsafeStructArray) =>
            _unsafeStructArray = unsafeStructArray;

        [HttpPost("[action]")]
        public Data Calculate(Data input) =>
            _unsafeStructArray.Calculate(input);

        [HttpPost("[action]")]
        public Data OptimalTotalDays(Data input) =>
            _unsafeStructArray.OptimalTotalDays(input);

        [HttpPost("[action]")]
        public Data OptimalReinvestingDays(Data input) =>
            _unsafeStructArray.OptimalReinvestingDays(input);

        [HttpPost("[action]")]
        public Data OptimalInitialPacks(Data input) =>
            _unsafeStructArray.OptimalInitialPacks(input);

        [HttpPost("[action]")]
        public Data ToROL(Data input) =>
            _unsafeStructArray.ToROL(input);
    }
}
