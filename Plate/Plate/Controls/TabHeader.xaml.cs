using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Plate.Controls
{
    // ---------------- //
    // Tab Header Class //
    // ---------------- //
    public sealed partial class TabHeader : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TabHeader), null);
        public static readonly DependencyProperty QuadrantProperty = DependencyProperty.Register("Quadrant", typeof(string), typeof(TabHeader), null);
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(Visibility), typeof(TabHeader), null);

        // ***** //
        // Labal //
        // ***** //
        public string Label
        {
            get { return GetValue(LabelProperty) as string; }
            set { SetValue(LabelProperty, value); }
        }

        // ******** //
        // Quadrant //
        // ******** //
        public string Quadrant
        {
            get { return GetValue(QuadrantProperty) as string; }
            set { SetValue(QuadrantProperty, value); }
        }

        // *********** //
        // Is Selected //
        // *********** //
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                // IF the value is changing
                // - Set the private property
                // - Set the dependancy property
                // ENDIF
                if (value != _IsSelected)
                {
                    _IsSelected = value;

                    if (value == true)
                    {
                        SetValue(IsSelectedProperty, Visibility.Visible);
                    }
                    else
                    {
                        SetValue(IsSelectedProperty, Visibility.Collapsed);
                    }
                }
            }
        }

        // ----------- //
        // Constructor //
        // ----------- //
        public TabHeader()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }

    // ------------------- //
    // Quadrant Converters //
    // ------------------- //
    public class QuadrantVisibilityConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        // ------- //
        // Convert //
        // ------- //
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Get the quadrant that the path is displaying
            string PathQuadrant = parameter as string;
            string ControlQuadrant = value as string;

            // IF the quadrant set by the user is 5 OR
            // IF the quadrant set by the user matches the predefined quadrant for the path
            // - Display the path
            // ELSE
            // - Do not display the path
            // ENDIF
            if ((ControlQuadrant == "5") || (PathQuadrant == ControlQuadrant))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        // ------------ //
        // Convert Back //
        // ------------ //
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}