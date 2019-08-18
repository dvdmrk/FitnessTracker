using Microsoft.AspNetCore.Mvc;
using RoutineCatalogue.Models.ViewModels;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.MVC.Repositories;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace RoutineCatalogue.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SetController : ControllerBase
    {
        IRepository<Set, SetViewModel, SetIndexViewModel> _repo;
        public SetController(IRepository<Set, SetViewModel, SetIndexViewModel> repo)
        {
            _repo = repo;
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(_repo.Get(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Set body)
        {
            try
            {
                return Ok(_repo.Add(body));
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] SetViewModel body)
        {
            try
            {
                _repo.Update(body);
                return Ok();
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
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}