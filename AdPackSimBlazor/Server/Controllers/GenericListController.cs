using AdPackSimLib;
using AdPackSimLib.Calculators;
using Microsoft.AspNetCore.Mvc;

namespace AdPackSimBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericListController : ControllerBase
    {
        private readonly IGenericList _genericList;

        public GenericListController(IGenericList genericList) =>
            _genericList = genericList;

        [HttpPost("[action]")]
        public Data Calculate(Data input) =>
            _genericList.Calculate(input);

        [HttpPost("[action]")]
        public Data OptimalTotalDays(Data input) =>
            _genericList.OptimalTotalDays(input);

        [HttpPost("[action]")]
        public Data OptimalReinvestingDays(Data input) =>
            _genericList.OptimalReinvestingDays(input);

        [HttpPost("[action]")]
        public Data OptimalInitialPacks(Data input) =>
            _genericList.OptimalInitialPacks(input);

        [HttpPost("[action]")]
        public Data ToROL(Data input) =>
            _genericList.ToROL(input);
    }
}
