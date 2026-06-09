using Microsoft.AspNetCore.Mvc;
using solution.Services.Interfaces;
using System.Diagnostics;

namespace solution.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;

        public HomeController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _memberService.GetAllForUIAsync();

            return View(model);
        }



    }
}
