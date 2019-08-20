using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
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
            HyrdrateCache();
        }
        public Task<bool> HyrdrateCache()
        {
            try
            {
                var routines = _routineService.GetRoutines().Result;
                _cache.Set("Routines", routines);
                foreach (var routine in routines)
                    _cache.Set(routine.Id, routine);
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                return Task.FromResult(false);
            }
        }
    }
}
