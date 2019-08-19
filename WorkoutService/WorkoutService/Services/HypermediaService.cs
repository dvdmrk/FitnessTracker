using System;
using WorkoutService.Models;
namespace WorkoutService.Services
{
    public class HypermediaService
    {
        public Hypermedia GetHypermediaFromRoutineId(Guid id)
        {
            return new Hypermedia
            {
                Rel = "FirstSet",
                Href = $"/api/Set?RoutineId={id}",
                Action = "GET"
            };
        }
        public Hypermedia GetHypermediaForNextSet()
        {
            return new Hypermedia
            {
                Rel = "NextSet",
                Href = "/api/Set",
                Action = "POST",
            };
        }
    }
}
