using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.Services
{
    public interface IPublisherAdapter
    {
        Task<bool> Publish();
    }
}
