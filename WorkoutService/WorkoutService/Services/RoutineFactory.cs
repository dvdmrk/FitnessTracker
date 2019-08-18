using Microsoft.Extensions.Caching.Memory;
namespace WorkoutService.Services
{
    public class RoutineFactory
    {
        IRoutineService _routineService;
        IMemoryCache _cache;
        public RoutineFactory(IRoutineService routineService, IMemoryCache cache)
        {
            _routineService = routineService;
            _cache = cache;
            var routines = _routineService.GetRoutines().Result;
            _cache.Set("Routines", routines);
            foreach (var routine in routines)
                _cache.Set($"{routine.Id}", routine);
        }
    }
}
