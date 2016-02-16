using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Plate.ViewModel
{
    class TaskViewModel
    {
        // ** //
        // ID //
        // ** //
        private int _ID;
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

        // **** //
        // Name //
        // **** //
        private string _name;
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

        // ---------------------- //
        // Raise Property Changed //
        // ---------------------- //
        public event PropertyChangedEventHandler PropertyChanged;
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
