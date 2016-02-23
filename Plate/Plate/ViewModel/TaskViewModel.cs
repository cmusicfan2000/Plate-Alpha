using System;
using System.Linq;
using SQLite.Net;
using Plate.Model;

namespace Plate.ViewModel
{
    class TaskViewModel : GenericViewModel
    {
        private string _description;
        /// <summary>
        /// Gets or sets a description of the task
        /// </summary>
        public string description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged("description");
                }
            }
        }

        private int _timeToComplete;
        /// <summary>
        /// Gets or sets the time (in minutes) to complete the task
        /// </summary>
        public int timeToComplete
        {
            get { return _timeToComplete; }
            set
            {
                if (_timeToComplete != value)
                {
                    _timeToComplete = value;
                    RaisePropertyChanged("timeToComplete");
                }
            }
        }

        private int _timeRemaining;
        /// <summary>
        /// Gets or sets the remaining time (in minutes) it will take to complete the task
        /// </summary>
        public int timeRemaining
        {
            get { return _timeRemaining; }
            set
            {
                if (_timeRemaining != value)
                {
                    _timeRemaining = value;
                    RaisePropertyChanged("timeRemaining");
                }
            }
        }

        private int _progress;
        /// <summary>
        /// Gets or sets the progress of the task (1-4)
        /// </summary>
        public int progress
        {
            get { return _progress; }
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    RaisePropertyChanged("progress");
                }
            }
        }

        private string _status;
        /// <summary>
        /// Gets or sets a summary of the current status of the task
        /// </summary>
        public string status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged("status");
                }
            }
        }

        private int _quadrant;
        /// <summary>
        /// Gets or sets the quadrant to which this task belongs (1-4)
        /// </summary>
        public int quadrant
        {
            get { return _quadrant; }
            set
            {
                if (_quadrant != value)
                {
                    _quadrant = value;
                    RaisePropertyChanged("quadrant");
                }
            }
        }

        private int _plateID;
        /// <summary>
        /// Gets or sets the ID that represents the plate to which the task belongs
        /// </summary>
        public int plateID
        {
            get { return _plateID; }
            set
            {
                if (_plateID != value)
                {
                    _plateID = value;
                    RaisePropertyChanged("plateID");
                }
            }
        }

        private bool _reminder;
        /// <summary>
        /// Gets or sets the reminder flag indicating a reminder attached to the task
        /// </summary>
        public bool reminder
        {
            get { return _reminder; }
            set
            {
                if (_reminder != value)
                {
                    _reminder = value;
                    RaisePropertyChanged("reminder");
                }
            }
        }

        private DateTime _reminderDateTime;
        /// <summary>
        /// Gets or sets the date and time of the reminder (remembers previous setting)
        /// </summary>
        public DateTime reminderDateTime
        {
            get { return _reminderDateTime; }
            set
            {
                if (_reminderDateTime != value)
                {
                    _reminderDateTime = value;
                    RaisePropertyChanged("reminderDateTime");
                }
            }
        }

        /// <summary>
        /// Retrieves a task with the given ID from the local database as a TaskViewModel
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public TaskViewModel Get(int taskID)
        {
            // Declare locals
            var task = new TaskViewModel();

            // Perform operations inside the database
            using (var db = new SQLiteConnection(App.SQLITE_PLATFORM, App.DB_PATH))
            {
                // Find the task in the database
                var _task = (db.Table<TaskModel>().Where(c => c.ID == taskID)).Single();

                // Copy values from the database to the instance of the view model
                task.ID = _task.ID;
                task.name = _task.name;
                task.description = _task.description;
                task.timeToComplete = _task.timeToComplete;
                task.timeRemaining = _task.timeRemaining;
                task.progress = _task.progress;
                task.status = _task.status;
                task.quadrant = _task.quadrant;
                task.plateID = _task.plateID;
                task.reminder = _task.reminder;
                task.reminderDateTime = _task.reminderDateTime;
            }

            // Return the found item
            return task;
        }

        /// <summary>
        /// Saves the given task to the local database
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public string Save(TaskViewModel task)
        {
            // Declare locals
            string result = string.Empty;

            // Perform operations inside the local database
            using (var db = new SQLiteConnection(App.SQLITE_PLATFORM, App.DB_PATH))
            {
                try
                {
                    // Retrieve the item from the databaser
                    var existingTask = (db.Table<TaskModel>().Where(c => c.ID == task.ID)).SingleOrDefault();

                    // IF an existing item was found
                    // - Update the information in the database
                    // ELSE
                    // - Add the task to the database
                    // ENDIF
                    if (existingTask != null)
                    {
                        existingTask.name = task.name;
                        existingTask.description = task.description;
                        existingTask.timeToComplete = task.timeToComplete;
                        existingTask.timeRemaining = task.timeRemaining;
                        existingTask.progress = task.progress;
                        existingTask.status = task.status;
                        existingTask.quadrant = task.quadrant;
                        existingTask.plateID = task.plateID;
                        existingTask.reminder = task.reminder;
                        existingTask.reminderDateTime = task.reminderDateTime;

                        // Update the task in the local database
                        int success = db.Update(existingTask);
                    }
                    else
                    {
                        int success = db.Insert(new TaskModel()
                        {
                            name = task.name,
                            description = task.description,
                            timeToComplete = task.timeToComplete,
                            timeRemaining = task.timeRemaining,
                            progress = task.progress,
                            status = task.status,
                            quadrant = task.quadrant,
                            plateID = task.plateID,
                            reminder = task.reminder,
                            reminderDateTime = task.reminderDateTime
                        });
                    }

                    // Set the result to success
                    result = "Success";
                }
                catch
                {
                    // Set the result to Failed
                    result = "Failed";
                }
            }

            // Return the result
            return result;
        }

        /// <summary>
        /// Removes the task from the local database
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public string Delete(int taskID)
        {
            // Declare locals
            string result = string.Empty;

            // Perform operations inside the database
            using (var dbConn = new SQLiteConnection(App.SQLITE_PLATFORM, App.DB_PATH))
            {
                // Retrieve a direct connection to the item in the database
                var existingTask = dbConn.Query<TaskModel>("select * from TaskModel where ID =" + taskID).FirstOrDefault();

                // IF the task was found
                // - Attempt to delete it
                // ENDIF
                if (existingTask != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        // Delete the task
                        dbConn.Delete(existingTask);

                        // IF the task was deleted
                        // - Set the result to "Success"
                        // ELSE
                        // - Set the result to "Failed"
                        // ENDIF
                        if (dbConn.Delete(existingTask) > 0)
                        {
                            result = "Success";
                        }
                        else
                        {
                            result = "Failed";
                        }
                    });
                }

                // Return the result
                return result;
            }
        }
    }
}
