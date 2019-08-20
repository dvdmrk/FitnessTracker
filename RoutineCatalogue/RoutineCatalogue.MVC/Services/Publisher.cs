using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.Services
{
    public class Publisher : IPublisherAdapter
    {
        IPublisher _publisher;
        public Publisher(IPublisher publisher)
        {
            _publisher = publisher;
        }
        public Task<bool> Publish()
        {
            return _publisher.Publish();
        }
    }
}
