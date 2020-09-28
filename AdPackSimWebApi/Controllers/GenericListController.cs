using AdPackSimLib;
using AdPackSimLib.Calculators;
using Microsoft.AspNetCore.Mvc;

namespace AdPackSimWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericListController : ControllerBase
    {
        private readonly IGenericList _genericList;

        public GenericListController(IGenericList genericList) =>
            _genericList = genericList;

        [HttpPost("[action]")]
        public Data Calculate([FromBody] Data input) =>
            _genericList.Calculate(input);

        [HttpPost("[action]")]
        public Data OptimalTotalDays([FromBody] Data input) =>
            _genericList.OptimalTotalDays(input);

        [HttpPost("[action]")]
        public Data OptimalReinvestingDays([FromBody] Data input) =>
            _genericList.OptimalReinvestingDays(input);

        [HttpPost("[action]")]
        public Data OptimalInitialPacks([FromBody] Data input) =>
            _genericList.OptimalInitialPacks(input);

        [HttpPost("[action]")]
        public Data ToROL([FromBody] Data input) =>
            _genericList.ToROL(input);
    }
}
