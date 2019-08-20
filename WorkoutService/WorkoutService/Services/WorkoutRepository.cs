using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        public WorkoutHistory Read(Guid routineId)
        {
            return LoadAsync<WorkoutHistory>(getKeyFromRoutine(routineId), _config).Result;
        }
        public async Task Write(Workout workout)
        {
            var history = Read(workout.RoutineId);
            WorkoutRoutine routine = null;
            if (history == null) history = new WorkoutHistory(getKeyFromRoutine(workout.RoutineId));
            else routine = getMostRecentNotCompletedWorkout(history);
            if (routine == null) history.WorkoutRoutines.Add(new WorkoutRoutine());

            routine.Workouts.Add(workout);
            history.key = getKeyFromRoutine(workout.RoutineId);
            history.WorkoutRoutinesJSON = JsonConvert.SerializeObject(history.WorkoutRoutines);
            await SaveAsync(history, _config);
        }
        public void CompleteWorkout(Guid id)
        {
            var history = Read(id);
            var routine = getMostRecentNotCompletedWorkout(history);

            routine.DateCompleted = DateTime.Now;
            history.WorkoutRoutinesJSON = JsonConvert.SerializeObject(history.WorkoutRoutines);
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
