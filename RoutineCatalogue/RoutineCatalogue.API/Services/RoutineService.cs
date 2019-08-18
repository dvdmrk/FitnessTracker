using Microsoft.EntityFrameworkCore;
using RoutineCatalogue.Models.ApiModels;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.MVC.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace RoutineCatalogue.API.Services
{
    public class RoutineService
    {
        ApplicationDbContext _context;
        public RoutineService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetAll()
        {
            return await _context.Set<Routine>().Include(x => x.Sets).ThenInclude(x => x.Exercise).Select(r => new 
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                CreatedBy = r.CreateBy.Email,
                Sets = r.Sets.Select(s => new SetBase
                {
                    Id = s.Id,
                    ExerciseId = s.Exercise.Id,
                    Exercise = s.Exercise.Name,
                    Directions = s.Exercise.Description,
                    Repetitions = s.Repetitions,
                    Weight = s.Weight,
                    Order = s.Order
                })
            }).ToListAsync();
        }
    }
}
