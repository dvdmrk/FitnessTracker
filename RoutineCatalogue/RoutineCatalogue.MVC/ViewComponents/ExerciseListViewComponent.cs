using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.MVC.Data;
using System.Linq;
using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.ViewComponents
{
    public class ExerciseListViewComponent : ViewComponent
    {
        ApplicationDbContext _context;
        IMapper _mapper;
        public ExerciseListViewComponent(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Set<Exercise>().Select(e => _mapper.Map<SelectListItem>(e)).ToListAsync());
        }
    }
}