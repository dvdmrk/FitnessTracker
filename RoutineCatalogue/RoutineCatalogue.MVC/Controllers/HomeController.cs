using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
using RoutineCatalogue.MVC.Data;
using RoutineCatalogue.MVC.Models;
using RoutineCatalogue.MVC.Repositories;

namespace RoutineCatalogue.MVC.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Set, SetViewModel, SetIndexViewModel> _repo;
        public HomeController(IRepository<Set,SetViewModel,SetIndexViewModel> repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var test = _repo.GetAll();
            var t1 = test.Result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
