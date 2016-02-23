using System.ComponentModel;

namespace Plate.ViewModel
{
    class GenericViewModel
    {
        private int _ID;
        /// <summary>
        /// Gets or sets the ID of the item (Generic)
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    RaisePropertyChanged("ID");
                }
            }
        }

        private string _name;
        /// <summary>
        /// Gets or sets the name of the item (Generic)
        /// </summary>
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifies the system of a change in the given property
        /// </summary>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
