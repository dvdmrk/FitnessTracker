using Microsoft.AspNetCore.Identity;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.Settings;
using RoutineCatalogue.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineCatalogue.MVC.Factories
{
    public class RoutineFactory
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            var user = context.Set<User>().FirstOrDefault();

            var pushups = new Exercise
            {
                Name = "Push Ups",
                Description = "Lay stomach down, palms shoulder width apart against the ground, and raise your upper body by pushing against the ground while keeping your core tight.",
                CreateBy = user,
                CreateDate = DateTime.Now
            };
            var pullups = new Exercise
            {
                Name = "Pull Ups",
                Description = "Grap bar tightly, hands shoulder width apart. Pull your body from the ground until your chin is above the bar.",
                CreateBy = user,
                CreateDate = DateTime.Now
            };
            var dips = new Exercise
            {
                Name = "Dips",
                Description = "Place hands palm down on a plateau raised higher than feet. Your body should be in a sitting position with your legs straight. Lower yourself down by your arms and push yourself back up.",
                CreateBy = user,
                CreateDate = DateTime.Now
            };
            var squats = new Exercise
            {
                Name = "Squats",
                Description = "Stand straight up, feet shoulder width apart, engage your core. Lower your body bending only at the knees for activation, and core for balance. Push out with your knees to raise your body again.",
                CreateBy = user,
                CreateDate = DateTime.Now
            };
            var benchpress = new Exercise
            {
                Name = "Bench Press",
                Description = "Lay flat on your back on a plane with weights in each hand. Hands remain gripping weights and fist remains perpendicular to your body. Lower weights and press through chest and arms to raise weights.",
                CreateBy = user,
                CreateDate = DateTime.Now
            };
            var deadlift = new Exercise
            {
                Name = "Deadlifts",
                Description = "Bend at the knees keeping back straight until palms can grasp weights on ground. Push with legs to continue to lift weights and straighten back. Keep core engaged.",
                CreateBy = user,
                CreateDate = DateTime.Now
            };
            context.Set<Exercise>().AddRange(new List<Exercise> { pushups, pullups, dips, squats, benchpress, deadlift });
            context.SaveChanges();
            //Create Routines
            var thor = new Routine
            {
                Name = "Thors Workout Routine",
                Description = "While Thor might not be the strongest avenger he, he is the god of thunder. In this 12 exercise routine you'll complete all the exercises that make Thor strong enough to lift MJollnir.",
                CreateBy = user,
                CreateDate = DateTime.Now,
                Sets = new List<Set>
                {
                    new Set
                    {
                        Exercise = squats,
                        Repetitions = 20,
                        Weight = 60,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = squats,
                        Repetitions = 15,
                        Weight = 70,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = squats,
                        Repetitions = 10,
                        Weight = 80,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = pushups,
                        Repetitions = 20,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = benchpress,
                        Repetitions = 12,
                        Weight = 60,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = benchpress,
                        Repetitions = 10,
                        Weight = 70,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = benchpress,
                        Repetitions = 8,
                        Weight = 80,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = pushups,
                        Repetitions = 20,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = deadlift,
                        Repetitions = 12,
                        Weight = 60,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = deadlift,
                        Repetitions = 10,
                        Weight = 70,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = deadlift,
                        Repetitions = 8,
                        Weight = 80,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    }, 
                    new Set
                    {
                        Exercise = pushups,
                        Repetitions = 20,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    }
                }
            };
            var spiderman = new Routine
            {
                Name = "Spider Man's Workout Routine",
                Description = "Want to train like the super lean, web slinging super hero. Here's how in this 12 set Routine.",
                CreateBy = user,
                CreateDate = DateTime.Now,
                Sets = new List<Set>
                {
                    new Set
                    {
                        Exercise = squats,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = squats,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = squats,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = squats,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = dips,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = dips,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = dips,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = dips,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = pushups,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = pushups,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = pushups,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    },
                    new Set
                    {
                        Exercise = pushups,
                        Repetitions = 50,
                        CreateBy = user,
                        CreateDate = DateTime.Now
                    }
                }
            };
            context.Set<Routine>().AddRange(new List<Routine> { thor, spiderman });
            context.SaveChanges();
        }
    }
}
