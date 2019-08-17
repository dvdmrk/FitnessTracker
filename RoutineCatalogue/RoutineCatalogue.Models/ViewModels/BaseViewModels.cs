using System;
namespace RoutineCatalogue.Models.ViewModels
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
    }
    public class NamedViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
