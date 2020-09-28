using AdPackSimLib;
using AdPackSimLib.Calculators;
using Microsoft.AspNetCore.Mvc;

namespace AdPackSimWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnsafeStructArrayController : ControllerBase
    {
        private readonly IUnsafeStructArray _unsafeStructArray;

        public UnsafeStructArrayController(IUnsafeStructArray unsafeStructArray) =>
            _unsafeStructArray = unsafeStructArray;

        [HttpPost("[action]")]
        public Data Calculate([FromBody] Data input) =>
            _unsafeStructArray.Calculate(input);

        [HttpPost("[action]")]
        public Data OptimalTotalDays([FromBody] Data input) =>
            _unsafeStructArray.OptimalTotalDays(input);

        [HttpPost("[action]")]
        public Data OptimalReinvestingDays([FromBody] Data input) =>
            _unsafeStructArray.OptimalReinvestingDays(input);

        [HttpPost("[action]")]
        public Data OptimalInitialPacks([FromBody] Data input) =>
            _unsafeStructArray.OptimalInitialPacks(input);

        [HttpPost("[action]")]
        public Data ToROL([FromBody] Data input) =>
            _unsafeStructArray.ToROL(input);
    }
}
