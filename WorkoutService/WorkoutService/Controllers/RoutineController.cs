using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public RoutineController(IMemoryCache cache, HypermediaService hypermediaService)
        {
            _cache = cache;
            _hypermediaService = hypermediaService;
        }
        [HttpGet]
        public IActionResult GetRoutines()
        {
            var routines = new List<Routine>();

            return Ok(_cache.TryGetValue("Routines", out routines));
        }
        [HttpGet("{id}")]
        public IActionResult GetRoutine(Guid id)
        {
            var routine = new Routine();
            _cache.TryGetValue(id, out routine);
            return Ok(new { routine, hypermedia = _hypermediaService.GetHypermediaFromRoutineId(routine.Id) });
        }
    }
}