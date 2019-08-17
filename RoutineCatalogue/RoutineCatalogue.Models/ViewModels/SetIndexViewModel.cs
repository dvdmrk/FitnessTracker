using System.ComponentModel.DataAnnotations;
namespace RoutineCatalogue.Models.ViewModels
{
    public class SetIndexViewModel : BaseViewModel
    {
        [Display(Name="Exercise")]
        public string Exercise { get; set; }
        [Display(Name = "Recommended # of Repetitions")]
        public int? Repetitions { get; set; }
        [Display(Name = "Recommended % of Max Weight")]
        public double? Weight { get; set; }
    }
}