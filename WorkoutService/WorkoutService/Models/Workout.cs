using System;
namespace WorkoutService.Models
{
    public class Workout
    {
        public int Order { get; set; }
        public double Weight { get; set; }
        public Guid RoutineId { get; set; }
        public Guid SetId { get; set; }
        public int Repetitions { get; set; }
    }
}