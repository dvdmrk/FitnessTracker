using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
using RoutineCatalogue.MVC.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.ViewComponents
{
    public class SetListViewComponent : ViewComponent
    {
        ApplicationDbContext _context;
        IMapper _mapper;
        public SetListViewComponent(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            return View(await _context.Set<Set>().Include(e => e.Exercise).Where(e => e.Routine.Id == id).Select(e => _mapper.Map<SetIndexViewModel>(e)).ToListAsync());
        }
    }
}