using Windows.UI;
using SQLite.Net;
using Plate.Model;
using System.Linq;

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
                var _plate = (db.Table<Model.PlateModel>().Where(p => p.ID == plateID)).Single();

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

        // ------ //
        // Delete //
        // ------ //
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
