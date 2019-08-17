using System;
using System.ComponentModel.DataAnnotations;
namespace RoutineCatalogue.Models.ViewModels
{
    public class SetViewModel : BaseViewModel
    {
        [Display(Name = "Exercise Name")]
        public string Exercise { get; set; }
        [Display(Name = "Recommended # of Repetitions")]
        public int? Repetitions { get; set; }
        [Display(Name = "Recommended % of Max Weight")]
        public double? Weight { get; set; }
        public Guid RoutineId { get; set; }
        public Guid ExerciseId { get; set; }
    }
}
