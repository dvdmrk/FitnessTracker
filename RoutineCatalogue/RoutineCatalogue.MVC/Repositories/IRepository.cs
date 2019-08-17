using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.Repositories
{
    public interface IRepository<Model, ViewModel, IndexVieModel> where Model : BaseEntity where ViewModel : BaseViewModel
    {
        ViewModel Get(Guid id);
        Task<List<IndexVieModel>> GetAll();
        Guid Add(Model entity);
        void Update(ViewModel viewModel);
        void Delete(Guid id);
        bool EntityExists(Guid id);
    }
}