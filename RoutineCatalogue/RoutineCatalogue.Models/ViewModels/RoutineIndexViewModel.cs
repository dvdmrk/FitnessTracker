using System;
using System.ComponentModel.DataAnnotations;
namespace RoutineCatalogue.Models.ViewModels
{
    public class RoutineIndexViewModel : NamedViewModel
    {
        [Display(Name = "Updated By")]
        public string UpdateBy { get; set; }
        [Display(Name = "Updated On")]
        public DateTime UpdateDate { get; set; }
    }
}