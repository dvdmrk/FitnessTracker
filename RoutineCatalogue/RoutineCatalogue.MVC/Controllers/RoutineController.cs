using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
using RoutineCatalogue.MVC.Repositories;
using System;
using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.Controllers
{
    [Authorize]
    public class RoutineController : Controller
    {
        IRepository<Routine, RoutineViewModel, RoutineIndexViewModel> _repo;
        public RoutineController(IRepository<Routine, RoutineViewModel, RoutineIndexViewModel> repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAll());
        }
        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();
            var exercise = _repo.Get((Guid)id);
            if (exercise == null) return NotFound();
            return View(exercise);
        }
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Routine routine)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Edit", new { id = _repo.Add(routine) });
            return View(routine);
        }
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var exercise = _repo.Get((Guid)id);
            if (exercise == null) return NotFound();
            return View(exercise);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoutineViewModel routine)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(routine);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.EntityExists(routine.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(routine);
        }
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var routine = _repo.Get((Guid)id);
            if (routine == null) return NotFound();
            return PartialView(routine);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
