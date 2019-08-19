using System;
using System.Threading.Tasks;
using WorkoutService.Models;
namespace WorkoutService.Services
{
    public interface IWorkoutRepository
    {
        Task<WorkoutHistory> Read(Guid routineId);
        Task Write(Workout workout);
        void CompleteWorkout(Guid id);
    }
}
