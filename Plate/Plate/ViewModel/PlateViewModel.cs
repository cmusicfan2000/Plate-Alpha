using Plate.Model;
using SQLite.Net;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI;

namespace Plate.ViewModel
{
    class PlateViewModel : GenericViewModel
    {
        private Color _color;
        /// <summary>
        /// Gets or sets the color of the plate
        /// </summary>
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

        private ObservableCollection<TaskViewModel> _tasks;
        /// <summary>
        /// Gets or sets the collection of tasks on a plate
        /// </summary>
        public ObservableCollection<TaskViewModel> tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
                RaisePropertyChanged("tasks");
            }
        }

        /// <summary>
        /// Retrieves the plate with the given ID from the local database
        /// </summary>
        /// <param name="plateID"></param>
        /// <returns></returns>
        public PlateViewModel Get(int plateID)
        {
            // Declare locals
            var plate = new PlateViewModel();

            // Perform operations inside the database
            using (var db = new SQLiteConnection(App.SQLITE_PLATFORM, App.DB_PATH))
            {
                // Find the plate in the database
                var _plate = (db.Table<PlateModel>().Where(p => p.ID == plateID)).Single();

                // Copy values from the database to the instance of the view model
                plate.ID = _plate.ID;
                plate.name = _plate.name;
                plate.color = Color.FromArgb(_plate.a, _plate.r, _plate.g, _plate.b);

                plate.tasks = new ObservableCollection<TaskViewModel>()

                List<TaskModel> taskList = (db.Table<TaskModel>().Where(t => t.plateID == plateID)).ToList();

                
                // the above queary will return a list of Task Models. This list needs to be converted to an
                //  observable collection of taskViewModels
                
            }

            // Return the found plate
            return plate;
        }

        /// <summary>
        /// Saves changes to the plate with the given ID to the local database
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        public string Save(PlateViewModel plate)
        {
            // Declare locals
            string result = string.Empty;

            // Perform operations inside the database
            using (var db = new SQLiteConnection(App.SQLITE_PLATFORM, App.DB_PATH))
            {
                try
                {
                    // Retrieve the plate from the database
                    var existingPlate = (db.Table<PlateModel>().Where(p => p.ID == plate.ID)).SingleOrDefault();

                    // IF an existing plate was found
                    // - Update the information in the database
                    // ELSE
                    // - Add the plate to the database
                    // ENDIF
                    if (existingPlate != null)
                    {
                        existingPlate.name = plate.name;
                        existingPlate.a = plate.color.A;
                        existingPlate.r = plate.color.R;
                        existingPlate.g = plate.color.G;
                        existingPlate.b = plate.color.B;

                        int success = db.Update(existingPlate);
                    }
                    else
                    {
                        int success = db.Insert(new PlateModel()
                        {
                            name = plate.name,
                            a = plate.color.A,
                            r = plate.color.R,
                            g = plate.color.G,
                            b = plate.color.B
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
        /// Removes the plate with the given ID from the local database
        /// </summary>
        /// <param name="plateID"></param>
        /// <returns></returns>
        public string Delete(int plateID)
        {
            // Declare locals
            string result = string.Empty;

            // Perform operations inside the database
            using (var dbConn = new SQLiteConnection(App.SQLITE_PLATFORM, App.DB_PATH))
            {
                // Retrieve a direct connection to the item in the database
                var existingPlate = dbConn.Query<TaskModel>("select * from PlateModel where ID =" + plateID).FirstOrDefault();

                // IF the task was found
                // - Attempt to delete it
                // ENDIF
                if (existingPlate != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        // Delete the task
                        dbConn.Delete(existingPlate);

                        // IF the task was deleted
                        // - Set the result to "Success"
                        // ELSE
                        // - Set the result to "Failed"
                        // ENDIF
                        if (dbConn.Delete(existingPlate) > 0)
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
