using Microsoft.AspNetCore.Mvc;
using solution.Services.Interfaces;
using solution.ViewModels.Team;

namespace solution.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly IMemberService _memberService;

        public TeamController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {
            var datas = await _memberService.GetAllAsync();

            return View(datas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MemberCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _memberService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var data = await _memberService.GetByIdAsync(id);

            return View(data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _memberService.GetByIdAsync(id);

            MemberUpdateVM model = new()
            {
                Id = data.Id,
                Name = data.Name,
                Job = data.Job,
                Description = data.Description,
                Image = data.Image
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MemberUpdateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _memberService.UpdateAsync(id, model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _memberService.GetByIdAsync(id);

            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _memberService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}