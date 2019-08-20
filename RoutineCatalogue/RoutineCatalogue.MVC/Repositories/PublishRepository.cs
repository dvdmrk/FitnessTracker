using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
using RoutineCatalogue.MVC.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.Repositories
{
    public class PublishRepository<Model, ViewModel, IndexVieModel> : IRepository<Model, ViewModel, IndexVieModel> where Model : BaseEntity where ViewModel : BaseViewModel
    {
        Repository<Model, ViewModel, IndexVieModel> _inner;
        IPublisherAdapter _publisher;
        public PublishRepository(Repository<Model, ViewModel, IndexVieModel> inner, IPublisherAdapter publisher)
        {
            _inner = inner;
            _publisher = publisher;
        }
        public Guid Add(Model entity)
        {
            _publisher.Publish();
            return _inner.Add(entity);
        }
        public void Delete(Guid id)
        {
            _publisher.Publish();
            _inner.Delete(id);
        }
        public bool EntityExists(Guid id)
        {
            return _inner.EntityExists(id);
        }
        public ViewModel Get(Guid id)
        {
            return _inner.Get(id);
        }
        public Task<List<IndexVieModel>> GetAll()
        {
            return _inner.GetAll();
        }
        public void Update(ViewModel viewModel)
        {
            _publisher.Publish();
            _inner.Update(viewModel);
        }
    }
}