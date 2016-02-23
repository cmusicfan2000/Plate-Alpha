using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plate.Model;

namespace Plate.Data
{
    /// <summary>
    /// Fake service to simulate data layer to cloud
    /// </summary>
    public class FakeService
    {
        /// <summary>
        /// The name of the service
        /// </summary>
        public static string Name = "Fake Data Service."; 
 
        /// <summary>
        /// Returns a list of tasks for the given plate
        /// </summary>
        /// <param name="plateID"></param>
        /// <returns></returns>
        public static List<TaskModel> GetTasks(int plateID)
        {
            // Output debig code
            Debug.WriteLine("GET tasks for plate");

            // Return default values
            if (plateID == 0)
            {
                return new List<TaskModel>()
                {
                    new TaskModel()
                    {
                        name = "Get Gas",
                        description = "I need to get gas for the car",
                        timeToComplete =30,
                        timeRemaining = 15,
                        progress = 2,
                        status = "On my way to the gas station",
                        quadrant = 1,
                        plateID = 0,
                        reminder = false
                    },
                    new TaskModel()
                    {
                        name = "Do Laundry",
                        description = "I'm running out of clean clothes",
                        timeToComplete = 60,
                        timeRemaining = 60,
                        progress = 1,
                        status = "",
                        quadrant = 1,
                        plateID = 0,
                        reminder = false
                    },
                    new TaskModel()
                    {
                        name = "Learn to play Piano",
                        description = "I want to be at intermediate level by the age of 27",
                        timeToComplete = 1800,
                        timeRemaining = 1700,
                        progress = 3,
                        status = "Waiting on piano to arrive",
                        quadrant = 2,
                        plateID = 0,
                        reminder = false
                    }
                };
            }
            else if (plateID == 1)
            {
                return new List<TaskModel>()
                {
                    new TaskModel()
                    {
                        name = "Contact Client",
                        description = "I need more information from Client",
                        timeToComplete = 15,
                        timeRemaining = 15,
                        progress = 1,
                        status = "",
                        quadrant = 1,
                        plateID = 1,
                        reminder = true,
                        reminderDateTime = new DateTime(2016, 2, 19, 8, 0, 0)
                    },
                    new TaskModel()
                    {
                        name = "Rewrite Proposal",
                        description = "The boss wants it done by the end of the day",
                        timeToComplete = 90,
                        timeRemaining = 60,
                        progress = 2,
                        status = "Reformatting",
                        quadrant = 1,
                        plateID = 1,
                        reminder = false
                    },
                    new TaskModel()
                    {
                        name = "Work on career plan",
                        description = "Gotta climb the ladder",
                        timeToComplete = 43200,
                        timeRemaining = 32000,
                        progress = 3,
                        status = "Waiting on a job opening",
                        quadrant = 2,
                        plateID = 1,
                        reminder = false
                    }
                };
            }
            else
            {
                return new List<TaskModel>()
                {
                    new TaskModel()
                    {
                        name = "Beat Kingdom Hearts 2",
                        description = "Need to level up to 50 to win",
                        timeToComplete = 4320,
                        timeRemaining = 3220,
                        progress = 2,
                        status = "Working on reaching level 20",
                        quadrant = 3,
                        plateID = 2,
                        reminder = false
                    }
                };
            }
        }

        /// <summary>
        /// Inserts a task into the cloud database
        /// </summary>
        /// <param name="toInsert"></param>
        public static void Write(TaskModel toInsert)
        { 
            Debug.WriteLine("INSERT task with name " + toInsert.name); 
        }

        /// <summary>
        /// Deletes a task from the cloud database
        /// </summary>
        /// <param name="toDelete"></param>
        public static void Delete(TaskModel toDelete)
        {
            Debug.WriteLine("DELETE person with name " + toDelete.name);
        }
    }
}
