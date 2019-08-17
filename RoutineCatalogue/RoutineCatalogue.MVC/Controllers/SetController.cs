using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
using RoutineCatalogue.MVC.Repositories;
using System;
namespace RoutineCatalogue.MVC.Controllers
{
    public class SetController : Controller
    {
        IRepository<Set, SetViewModel, SetIndexViewModel> _repo;
        SetService _service;
        public SetController(IRepository<Set, SetViewModel, SetIndexViewModel> repo, SetService service)
        {
            _repo = repo;
            _service = service;
        }
        public IActionResult Index(Guid id)
        {
            return ViewComponent("SetList", id);
        }
        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();
            var set = _repo.Get((Guid)id);
            if (set == null) return NotFound();
            return PartialView(set);
        }
        [HttpPost]
        public IActionResult Create(SetViewModel vm)
        {
            _repo.Add(_service.CreateSet(vm));
            return ViewComponent("SetList", vm.RoutineId);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var set = _repo.Get((Guid)id);
            if (set == null) return NotFound();
            return PartialView(set);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SetViewModel set)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(set);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.EntityExists(set.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Edit), "Routine", new { Id = set.RoutineId });
            }
            return View(set);
        }
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var set = _repo.Get((Guid)id);
            if (set == null) return NotFound();
            return PartialView(set);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var entity = _repo.Get(id);
            _repo.Delete(id);
            return RedirectToAction(nameof(Edit), "Routine", entity.RoutineId);
        }
    }
}
