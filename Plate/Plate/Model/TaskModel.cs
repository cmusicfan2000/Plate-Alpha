using System;
using SQLite.Net.Attributes;

namespace Model
{
    public class TaskModel
    {
        /// <summary>
        ///  Gets or sets the identifier
        /// </summary>
        [PrimaryKey, AutoIncrement]
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
}
