using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
using RoutineCatalogue.MVC.Data;
namespace RoutineCatalogue.MVC
{
    public class SetService
    {
        ApplicationDbContext _context;
        public SetService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Set CreateSet(SetViewModel vm)
        {
            return new Set
            {
                Exercise = _context.Set<Exercise>().Find(vm.ExerciseId),
                Routine = _context.Set<Routine>().Find(vm.RoutineId)
            };
        }
    }
}