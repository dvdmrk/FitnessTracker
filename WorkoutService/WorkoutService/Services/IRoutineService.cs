using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutService.Models;
namespace WorkoutService.Services
{
    public interface IRoutineService
    {
        [Get("api/Routine/GetRoutinesWithSets")]
        Task<IEnumerable<Routine>> GetRoutines();
    }
}
