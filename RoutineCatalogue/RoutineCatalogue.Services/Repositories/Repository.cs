using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineCatalogue.Services.Repositories
{
    public class Repository<Model, ViewModel> : IRepository<Model, ViewModel> where Model : BaseEntity
    {
        DbSet<Model> _context;
        IQueryable<Model> _reader;
        IMapper _mapper;
        Func<Task<int>> _saveChanges;
        
        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context.Set<Model>();
            switch (typeof(Model).ToString())
            {
                case "Set":
                    _reader = (context.Set<Set>().Include("User").Include("Exerise").Include("Routine")) as IQueryable<Model>;
                    break;
                case "Exercise":
                    _reader = (context.Set<Exercise>().Include("User")) as IQueryable<Model>;
                    break;
                case "Routine":
                    _reader = (context.Set<Routine>().Include("User").Include(x => x.Sets).ThenInclude(x => x.Exercise)) as IQueryable<Model>;
                    break;
            }
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

        public Task<List<ViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
