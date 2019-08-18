using System;
using System.Collections.Generic;
namespace WorkoutService.Models
{
    public class Routine : BaseResponse
    {
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public IEnumerable<Set> Sets { get; set; }
    }
    public class Set : BaseResponse
    {
        public string Exercise { get; set; }
        public string Directions { get; set; }
        public Guid ExerciseId { get; set; }
        public int? Repetitions { get; set; }
        public double? Weight { get; set; }
        public int Order { get; set; }
    }
    public class BaseResponse
    {
        public Guid Id { get; set; }
    }
}
