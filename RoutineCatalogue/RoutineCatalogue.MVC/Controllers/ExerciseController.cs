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
    public class ExerciseController : Controller
    {
        IRepository<Exercise, ExerciseViewModel, ExerciseIndexViewModel> _repo;
        public ExerciseController(IRepository<Exercise, ExerciseViewModel, ExerciseIndexViewModel> repo)
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
            return PartialView(exercise);
        }
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(exercise);
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var exercise = _repo.Get((Guid)id);
            if (exercise == null) return NotFound();
            return PartialView(exercise);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExerciseViewModel exercise)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(exercise);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.EntityExists(exercise.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var exercise = _repo.Get((Guid)id);
            if (exercise == null) return NotFound();
            return PartialView(exercise);
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
