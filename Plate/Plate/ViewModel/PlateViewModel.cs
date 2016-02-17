using Windows.UI;
using SQLite.Net;
using Plate.Model;

namespace Plate.ViewModel
{
    class PlateViewModel : GenericViewModel
    {
        // ***** //
        // Color //
        // ***** //
        private Color _color;
        public Color color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    RaisePropertyChanged("color");
                }
            }
        }

        // --- //
        // Get //
        // --- //
        public PlateViewModel Get(int plateID)
        {
            // Declare locals
            var plate = new PlateViewModel();

            // Perform operations inside the database
            using (var db = new SQLiteConnection(App.SQLITE_PLATFORM, App.DB_PATH))
            {
                // Find the plate in the database
                var _plate = (db.Table<Model.Plate>().Where(p => p.ID == plateID)).Single();

                // Copy values from the database to the instance of the view model
                plate.ID = _plate.ID;
                plate.name = _plate.name;
                plate.color = Color.FromArgb(_plate.a, _plate.r, _plate.g, _plate.b);
            }

            // Return the found plate
            return plate;
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
                    var existingTask = (db.Table<Task>().Where(c => c.ID == task.ID)).SingleOrDefault();

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
                        int success = db.Insert(new Task()
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
                var existingTask = dbConn.Query<Task>("select * from Task where ID =" + taskID).FirstOrDefault();

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
