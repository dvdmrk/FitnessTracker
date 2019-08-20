using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using WorkoutService.Models;
using WorkoutService.Services;
namespace WorkoutService.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoutineController : ControllerBase
    {
        IMemoryCache _cache;
        HypermediaService _hypermediaService;
        RoutineFactory _routineFactory;
        public RoutineController(IMemoryCache cache, HypermediaService hypermediaService, RoutineFactory routineFactory)
        {
            _cache = cache;
            _hypermediaService = hypermediaService;
            _routineFactory = routineFactory;
        }
        [HttpGet]
        public IActionResult GetRoutines()
        {
            var routines = new List<Routine>();
            _cache.TryGetValue("Routines", out routines);
            return Ok(routines);
        }
        [HttpGet("{id}")]
        public IActionResult GetRoutine(Guid id)
        {
            var routine = new Routine();
            _cache.TryGetValue(id, out routine);
            return Ok(new { routine, hypermedia = _hypermediaService.GetHypermediaFromRoutineId(routine.Id) });
        }
        [HttpPost]
        [EnableCors("RoutineService")]
        public void HydrateCache()
        {
            _routineFactory.HyrdrateCache();
        }
    }
}