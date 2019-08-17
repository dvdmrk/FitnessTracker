using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
using RoutineCatalogue.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.Repositories
{
    public class Repository<Model, ViewModel, IndexVieModel> : IRepository<Model, ViewModel, IndexVieModel> where Model : BaseEntity where ViewModel : BaseViewModel
    {
        DbSet<Model> _context;
        IQueryable<Model> _reader;
        IMapper _mapper;
        Func<Task<int>> _saveChanges;
        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context.Set<Model>();
            switch (typeof(Model).Name)
            {
                case "Set":
                    _reader = (context.Set<Set>().Include(x => x.Exercise).Include(x => x.Routine)) as IQueryable<Model>;
                    break;
                case "Exercise":
                    _reader = (context.Set<Exercise>().Include(x => x.CreateBy).Include(x => x.UpdateBy)) as IQueryable<Model>;
                    break;
                case "Routine":
                    _reader = (context.Set<Routine>().Include(x => x.CreateBy).Include(x => x.UpdateBy).Include(x => x.Sets).ThenInclude(x => x.Exercise)) as IQueryable<Model>;
                    break;
            }
            _mapper = mapper;
            _saveChanges = new Func<Task<int>>(() => context.SaveChangesAsync());
        }
        public Guid Add(Model entity)
        {
            _context.Add(entity);
            _saveChanges();
            return entity.Id;
        }
        public void Delete(Guid id)
        {
            _context.Remove(_context.Find(id));
            _saveChanges();
        }
        public bool EntityExists(Guid id)
        {
            return _context.Any(e => e.Id == id);
        }
        public ViewModel Get(Guid id)
        {
            var entity = _reader.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<ViewModel>(entity);
        }
        public Task<List<IndexVieModel>> GetAll()
        {
            return _reader.Select(e => _mapper.Map<IndexVieModel>(e)).ToListAsync();
        }
        public void Update(ViewModel viewModel)
        {
            var entity = _reader.FirstOrDefault(x => x.Id == viewModel.Id);
            _mapper.Map(viewModel, entity);
        }
    }
}