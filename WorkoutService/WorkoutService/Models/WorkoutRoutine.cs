using System;
using System.Collections.Generic;
namespace WorkoutService.Models
{
    public class WorkoutRoutine
    {
        public WorkoutRoutine()
        {
            DateStarted = DateTime.Now;
        }
        public ICollection<Workout> Workouts { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}