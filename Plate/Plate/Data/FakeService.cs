using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Data
{
    // ================== //
    // Task Service Model //
    // ================== //
    public class Task
    {
        /// <summary>
        ///  Gets or sets the identifier
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the task
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets a description of the task
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Gets or sets the time (in minutes) to complete the task
        /// </summary>
        public int timeToComplete { get; set; }

        /// <summary>
        /// Gets or sets the remaining time (in minutes) it will take to complete the task
        /// </summary>
        public int timeRemaining { get; set; }

        /// <summary>
        /// Gets or sets the progress of the task (1-4)
        /// </summary>
        public int progress { get; set; }

        /// <summary>
        /// Gets or sets a summary of the current status of the task
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Gets or sets the quadrant to which this task belongs (1-4)
        /// </summary>
        public int quadrant { get; set; }

        /// <summary>
        /// Gets or sets the ID that represents the plate to which the task belongs
        /// </summary>
        public int plateID { get; set; }

        /// <summary>
        /// Gets or sets the reminder flag indicating a reminder attached to the task
        /// </summary>
        public bool reminder { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the reminder (remembers previous setting)
        /// </summary>
        public DateTime reminderDateTime { get; set; }
    }

    // =========================================== //
    // Fake Servie to simulate data layer to cloud //
    // =========================================== //
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
        public static List<Task> GetTasks(int plateID)
        {
            // Output debig code
            Debug.WriteLine("GET tasks for plate");

            // Return default values
            if (plateID == 0)
            {
                return new List<Task>()
                {
                    new Task()
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
                    new Task()
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
                    new Task()
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
                return new List<Task>()
                {
                    new Task()
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
                    new Task()
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
                    new Task()
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
                return new List<Task>()
                {
                    new Task()
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
        public static void Write(Task toInsert)
        { 
            Debug.WriteLine("INSERT task with name " + toInsert.name); 
        }

        /// <summary>
        /// Deletes a task from the cloud database
        /// </summary>
        /// <param name="toDelete"></param>
        public static void Delete(Task toDelete)
        {
            Debug.WriteLine("DELETE person with name " + toDelete.name);
        }
    }
}
