﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class RoutineController : ControllerBase
    {
        IRepository<Routine, RoutineViewModel, RoutineIndexViewModel> _repo;
        public RoutineController(IRepository<Routine, RoutineViewModel, RoutineIndexViewModel> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(new { message = await _repo.GetAll(), hypermedia = new HyperMediaResponse<Routine>(Guid.Empty) });
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(new { message = _repo.Get(id), hypermedia = new HyperMediaResponse<Routine>(id) });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Routine body)
        {
            try
            {
                return Ok(new { message = _repo.Add(body), hypermedia = new HyperMediaResponse<Routine>(body.Id) });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] RoutineViewModel body)
        {
            try
            {
                _repo.Update(body);
                return Ok(new { message = "Routine Updated!", hypermedia = new HyperMediaResponse<Routine>(body.Id) });
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
                return Ok(new { message = "Routine Deleted!", hypermedia = new HyperMediaResponse<Routine>() });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}
