using RestEase;
using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.Services
{
    public interface IPublisher
    {
        [Post("api/Routine/")]
        Task<bool> Publish();
    }
}
