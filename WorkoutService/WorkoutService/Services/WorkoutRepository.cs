using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkoutService.Models;
namespace WorkoutService.Services
{
    public class WorkoutRepository : DynamoDBContext, IWorkoutRepository
    {
        private DynamoDBOperationConfig _config;
        private IHttpContextAccessor _httpContextAccessor;
        public WorkoutRepository(IAmazonDynamoDB db, IOptions<ApplicationSettings> appSettings, IHttpContextAccessor httpContextAccessor) : base(db)
        {
            _config = new DynamoDBOperationConfig()
            {
                OverrideTableName = appSettings.Value.DynamoDb
            };
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<WorkoutHistory> Read(Guid routineId)
        {
            return await LoadAsync<WorkoutHistory>(getKeyFromRoutine(routineId), _config);
        }
        public async Task Write(Workout workout)
        {
            var history = Read(workout.RoutineId).Result;
            if (history == null) history = new WorkoutHistory(getKeyFromRoutine(workout.RoutineId));

            var routine = getMostRecentNotCompletedWorkout(history);
            if (routine == null) history.WorkoutRoutines.Add(new WorkoutRoutine());

            routine.Workouts.Add(workout);
            await SaveAsync(history, _config);
        }
        public void CompleteWorkout(Guid id)
        {
            var history = Read(id).Result;
            var routine = getMostRecentNotCompletedWorkout(history);

            routine.DateCompleted = DateTime.Now;
            SaveAsync(history, _config);
        }
        private WorkoutRoutine getMostRecentNotCompletedWorkout(WorkoutHistory wh)
        {
            return wh.WorkoutRoutines.OrderByDescending(x => x.DateStarted).FirstOrDefault(x => x.DateCompleted == null);
        }
        private string getKeyFromRoutine(Guid routineId)
        {
            var userId = (_httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier);
            return $"{userId}-{routineId}";
        }
    }
}
