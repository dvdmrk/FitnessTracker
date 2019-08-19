using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
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
        public SetController(IMemoryCache cache, HypermediaService hypermediaService)
        {
            _cache = cache;
            _hypermediaService = hypermediaService;
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

        // GET api/values/5
        [HttpPost]
        public IActionResult PostWorkout(Workout workout)
        {
            if (workout.Order < 0) return BadRequest();
            //Update DynamoDB  with values
            var routine = new Routine();
            _cache.TryGetValue(workout.RoutineId, out routine);
            var set = routine.Sets.Skip(workout.Order + 1).FirstOrDefault();
            return set == null ? Ok(new { message = "Congrats! You completed a workout." }) : Ok(new { set, hypermedia = _hypermediaService.GetHypermediaForNextSet() });
        }
    }
}
