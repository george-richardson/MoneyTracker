using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLitePCL;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Money_Tracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddTransactionPage : Page
    {
        public AddTransactionPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statement = connection.Prepare("INSERT INTO transactions VALUES(" + txtValue.Text + ", '" + txtDesc.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', NULL);"))
                {
                    statement.Step();
                }

            }
            
            this.Frame.GoBack();
        }



    }
}
