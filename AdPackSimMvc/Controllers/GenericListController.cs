using AdPackSimLib;
using AdPackSimLib.Calculators;
using AdPackSimMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdPackSimMvc.Controllers
{
    public class GenericListController : Controller
    {
        private readonly IGenericList _genericList;

        public GenericListController(IGenericList genericList) =>
            _genericList = genericList;

        public IActionResult Index() =>
            View(Data.Default);

        [HttpPost]
        public IActionResult Calculate([FromForm] Data input) =>
            View("Index", _genericList.Calculate(input));

        [HttpPost]
        public IActionResult OptimalTotalDays([FromForm] Data input) =>
            View("Index", _genericList.OptimalTotalDays(input));

        [HttpPost]
        public IActionResult OptimalReinvestingDays([FromForm] Data input) =>
            View("Index", _genericList.OptimalReinvestingDays(input));

        [HttpPost]
        public IActionResult OptimalInitialPacks([FromForm] Data input) =>
            View("Index", _genericList.OptimalInitialPacks(input));

        [HttpPost]
        public IActionResult ToROL([FromForm] Data input) =>
            View("Index", _genericList.ToROL(input));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
