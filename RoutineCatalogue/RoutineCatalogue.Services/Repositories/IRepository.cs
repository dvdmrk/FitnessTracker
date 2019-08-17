using RoutineCatalogue.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoutineCatalogue.Services.Repositories
{
    public interface IRepository<Model, ViewModel> where Model : BaseEntity
    {
        ViewModel Get(Guid id);
        Task<List<ViewModel>> GetAll();
        Guid Add(Model entity);
        void Update(ViewModel viewModel);
        void Delete(Guid id);
        bool EntityExists(Guid id);
    }
}
