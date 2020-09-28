using AdPackSimLib;
using AdPackSimLib.Calculators;
using AdPackSimMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdPackSimMvc.Controllers
{
    public class UnsafeStructArrayController : Controller
    {
        private readonly IUnsafeStructArray _unsafeStructArray;

        public UnsafeStructArrayController(IUnsafeStructArray unsafeStructArray) =>
            _unsafeStructArray = unsafeStructArray;

        public IActionResult Index() =>
            View(Data.Default);

        [HttpPost]
        public IActionResult Calculate([FromForm] Data input) =>
            View("Index", _unsafeStructArray.Calculate(input));

        [HttpPost]
        public IActionResult OptimalTotalDays([FromForm] Data input) =>
            View("Index", _unsafeStructArray.OptimalTotalDays(input));

        [HttpPost]
        public IActionResult OptimalReinvestingDays([FromForm] Data input) =>
            View("Index", _unsafeStructArray.OptimalReinvestingDays(input));

        [HttpPost]
        public IActionResult OptimalInitialPacks([FromForm] Data input) =>
            View("Index", _unsafeStructArray.OptimalInitialPacks(input));

        [HttpPost]
        public IActionResult ToROL([FromForm] Data input) =>
            View("Index", _unsafeStructArray.ToROL(input));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
