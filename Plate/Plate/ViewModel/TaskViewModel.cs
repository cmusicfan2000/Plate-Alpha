using System;
using System.Linq;
using System.ComponentModel;
using SQLite.Net;
using Plate.Model;

namespace Plate.ViewModel
{
    class TaskViewModel : GenericViewModel
    {
        // *********** //
        // Description //
        // *********** //
        private string _description;
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

        // **************** //
        // Time To Complete //
        // **************** //
        private int _timeToComplete;
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

        // ************** //
        // Time Remaining //
        // ************** //
        private int _timeRemaining;
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

        // ******** //
        // Progress //
        // ******** //
        private int _progress;
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

        // ****** //
        // Status //
        // ****** //
        private string _status;
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

        // ******** //
        // Quadrant //
        // ******** //
        private int _quadrant;
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

        // ******** //
        // Plate ID //
        // ******** //
        private int _plateID;
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

        // ******** //
        // Reminder //
        // ******** //
        private bool _reminder;
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

        // ****************** //
        // Reminder Date Time //
        // ****************** //
        private DateTime _reminderDateTime;
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

        // --- //
        // Get //
        // --- //
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

        // ---- //
        // Save //
        // ---- //
        public string Save(TaskViewModel task)
        {
            // Declare locals
            string result = string.Empty;

            // Perform operations inside the database
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

        // ------ //
        // Delete //
        // ------ //
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
