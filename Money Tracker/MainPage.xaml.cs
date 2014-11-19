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
using SQLitePCL;
using Money_Tracker.DataClasses;
using System.Collections.ObjectModel;


namespace Money_Tracker
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            lstTransactions.DataContext = transactions;
        }

        public ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();

        private void updateListView()
        {
            transactions.Clear();
            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statement = connection.Prepare("SELECT rowid, * FROM transactions ORDER BY datetime(time) DESC"))
                {
                    while (statement.Step() == SQLiteResult.ROW)
                    {
                        transactions.Add(new Transaction(statement[0].ToString(), statement[1].ToString(), statement[2].ToString(), statement[3].ToString()));
                    }
                }

            }
        }
        private void updateCounter()
        {
            
            string query;
            switch (cmbTimeSpan.SelectedIndex)
            {
                // Spent today.
                case 0:
                    query = "SELECT SUM(value) FROM transactions WHERE time>datetime('now', 'start of day');";
                    break;
                // Yesterday
                case 1:
                    query = "SELECT SUM(value) FROM transactions WHERE time>datetime('now', 'start of day', '-1 day') AND time<datetime('now', 'start of day');";
                    break;
                //Last Week
                case 2:
                    query = "SELECT SUM(value) FROM transactions WHERE time>datetime('now', 'start of day', '-7 day');";
                    break;
                //Last Month
                case 3:
                    query = "SELECT SUM(value) FROM transactions WHERE time>datetime('now', 'start of day', '-1 month');";
                    break;
                default:
                    query = "SELECT date('now');";
                    break;
            }
            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statement = connection.Prepare(query))
                {
                    statement.Step();
                    if (statement[0] != null)
                    {
                        //lblCounter.Text = "£" + float.Parse(statement[0].ToString()).ToString("n2");
                        lblCounter.Text = GeorgeMethods.convertToCurrencyString(statement[0].ToString());
                    }
                    else
                    {
                        lblCounter.Text = "£0.00";
                    }
                }

            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            updateListView();
            updateCounter();
        }

        private void btnAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddTransactionPage));
        }

        private void cmbTimeSpan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbTimeSpan != null)
            {
                updateCounter();
            }
            
        }

        private void ListViewItem_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            Transaction t = (Transaction)(sender as MenuFlyoutItem).Tag;
            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statement = connection.Prepare("DELETE FROM transactions WHERE rowid='" + transactions[transactions.IndexOf(t)].id + "';"))
                {
                    
                    statement.Step();
                }

            }
            transactions.Remove(t);
            updateCounter();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            updateListView();
        }

        private void btnTag_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageLabelsPage));
        }


    }
}
