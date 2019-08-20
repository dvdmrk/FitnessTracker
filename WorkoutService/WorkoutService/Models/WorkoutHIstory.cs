using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace WorkoutService.Models
{
    public class WorkoutHistory
    {
        public WorkoutHistory() {}
        public WorkoutHistory(string _key)
        {
            key = _key;
        }
        [DynamoDBHashKey]
        public string key { get; set; }
        public string WorkoutRoutinesJSON { get; set; }
        public ICollection<WorkoutRoutine> WorkoutRoutines => JsonConvert.DeserializeObject <ICollection<WorkoutRoutine>>(WorkoutRoutinesJSON);
    }
}
