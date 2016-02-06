using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Plate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        // ----------------- //
        // Tab Header Tapped //
        // ----------------- //
        private void TabHeader_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Get the quadrant of the selected tab
            string quadrant = ((Controls.TabHeader)sender).Quadrant;

            // Deselect all other tab headers
            if (quadrant != ShortTermGoals_Tab.Quadrant)
            {
                ShortTermGoals_Tab.IsSelected = false;
            }
            if (quadrant != LongTermGoals_Tab.Quadrant)
            {
                LongTermGoals_Tab.IsSelected = false;
            }
            if (quadrant != Distractions_Tab.Quadrant)
            {
                Distractions_Tab.IsSelected = false;
            }
            if (quadrant != TimeWasters_Tab.Quadrant)
            {
                TimeWasters_Tab.IsSelected = false;
            }
            if (quadrant != Summary_Tab.Quadrant)
            {
                Summary_Tab.IsSelected = false;
            }

            // Select the current tab header
            ((Controls.TabHeader)sender).IsSelected = true;
        }
    }
}
