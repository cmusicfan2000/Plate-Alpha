using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.ViewModel
{
    class PlateItemsViewModel : GenericViewModel
    {
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


    }
}
