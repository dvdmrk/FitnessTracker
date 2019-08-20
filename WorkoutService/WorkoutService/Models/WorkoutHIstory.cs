﻿using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
namespace WorkoutService.Models
{
    public class WorkoutHistory
    {
        public WorkoutHistory() {}
        public WorkoutHistory(string key)
        {
            Key = key;
        }
        [DynamoDBHashKey]
        public string Key { get; set; }
        public ICollection<WorkoutRoutine> WorkoutRoutines { get; set; }
    }
}
