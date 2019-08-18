using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutineCatalogue.Models.ApiModels;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
using RoutineCatalogue.MVC.Repositories;
using System;
using System.Threading.Tasks;

namespace RoutineCatalogue.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExerciseController : ControllerBase
    {
        IRepository<Exercise, ExerciseViewModel, ExerciseIndexViewModel> _repo;
        public ExerciseController(IRepository<Exercise, ExerciseViewModel, ExerciseIndexViewModel> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new { message = await _repo.GetAll(), hypermedia = new HyperMediaResponse<Exercise>(Guid.Empty) });
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(new { message = _repo.Get(id), hypermedia = new HyperMediaResponse<Exercise>(id) });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Exercise body)
        {
            try
            {
                return Ok(new { message = _repo.Add(body), hypermedia = new HyperMediaResponse<Exercise>(body.Id) });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] ExerciseViewModel body)
        {
            try
            {
                _repo.Update(body);
                return Ok(new { message = "Exercise Updated!", hypermedia = new HyperMediaResponse<Exercise>(body.Id) });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _repo.Delete(id);
                return Ok(new { message = "Exercise Deleted!", hypermedia = new HyperMediaResponse<Exercise>() });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}
