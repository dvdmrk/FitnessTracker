using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkoutService.Models;
using WorkoutService.Services;
namespace WorkoutService.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SetController : ControllerBase
    {
        IMemoryCache _cache;
        HypermediaService _hypermediaService;
        IWorkoutRepository _workoutRepository;
        public SetController(IMemoryCache cache, HypermediaService hypermediaService, IWorkoutRepository workoutRepository)
        {
            _cache = cache;
            _hypermediaService = hypermediaService;
            _workoutRepository = workoutRepository;
        }
        // GET api/values
        [HttpGet("{routineId}")]
        public IActionResult StartRoutine(Guid routineId)
        {
            if (routineId == null || routineId == Guid.Empty) return BadRequest();
            var routine = new Routine();
            _cache.TryGetValue(routineId, out routine);
            var set = routine.Sets.First();
            return Ok(new { set, hypermedia = _hypermediaService.GetHypermediaForNextSet() });
        }

        [HttpPost]
        public async Task<IActionResult> PostWorkout([FromBody] Workout workout)
        {
            if (workout.Order < 0) return BadRequest();
            await _workoutRepository.Write(workout);
            var routine = new Routine();
            _cache.TryGetValue(workout.RoutineId, out routine);
            var i = 0;
            routine.Sets.ToList().ForEach(c => { c.Order = i; i++; });
            var set = routine.Sets.Skip(workout.Order + 1).FirstOrDefault();
            if (set == null)
            {
                _workoutRepository.CompleteWorkout(workout.RoutineId);
                return Ok(new { message = "Congrats! You completed a workout." });
            }
            return Ok(new { set, hypermedia = _hypermediaService.GetHypermediaForNextSet() });
        }
    }
}
