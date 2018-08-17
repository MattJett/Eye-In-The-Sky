/**
 * Name: Matthew Jett
 * Class: CSCD 371-01 .Net Programming with C# | Tom Capaul | Spring 2018
 * Description: This class is my Database menu. Here the database is loaded up, filtered, and cleared from here.
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;

namespace EyeInTheSky
{
    /// <summary>
    /// Interaction logic for DataBasePage.xaml
    /// </summary>
    public partial class DataBasePage : Page
    {
        #region Fields
        SQLiteConnection connect;
        SQLiteDataAdapter adapterForTable;
        DataTable tableOfEvents;
        private ObservableCollection<string[]> _eventList;
        #endregion

        public DataBasePage(ref ObservableCollection<string[]> eventList)
        {
            InitializeComponent();
            _eventList = eventList;
            DataBase_Load("database.db", "");

            var contentsInGrid = DataGrid_dbPortal.ItemsSource as ObservableCollection<string[]>;
            if (contentsInGrid == null) return;
            contentsInGrid.CollectionChanged += new NotifyCollectionChangedEventHandler(DataGrid_DataChanged);

            DataBaseButtons_GreyOut();
        }

        #region Database Methods
        private void DataBase_Load(string file, string ext)
        {
            try
            {
                connect = new SQLiteConnection("Data Source=" + file);
                
                connect.Open();

                SQLiteCommand build = connect.CreateCommand();
                build.CommandText = "CREATE TABLE if not exists database(entry VARCHAR, change VARCHAR, file VARCHAR, ext VARCHAR, path VARCHAR, modified VARCHAR);";
                build.ExecuteNonQuery();

                adapterForTable = new SQLiteDataAdapter("SELECT * FROM database" + ext, connect);
                tableOfEvents = new DataTable();
                adapterForTable.Fill(tableOfEvents);
                DataGrid_dbPortal.ItemsSource = tableOfEvents.AsDataView();

                connect.Close();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            // TODO: FEATURE - maybe remove if I want to open a new database anytime, overriding current info in grid...
            Button_LoadDataBase.IsEnabled = false;
        }

        private void DataBase_Clear()
        {
            connect.Open();
            SQLiteCommand clear = new SQLiteCommand("DELETE FROM database", connect);
            clear.ExecuteNonQuery();
            connect.Close();

            MessageBox.Show("Database has been wiped.");
            Clear();

            Button_LoadDataBase.IsEnabled = false; // TODO: FEATURE - keep false for now, feature not ready, should be set to true when working...
        }
        #endregion

        #region Helper Methods
        private void Clear()
        {
            DataGrid_dbPortal.ItemsSource = null;
            DataGrid_dbPortal.Items.Clear();
        }
        #endregion

        #region GUI Formatting - greying out elements
        private void DataGrid_DataChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DataGrid_dbPortal.HasItems)
                Button_ClearDataBase.IsEnabled = true;
            else
                Button_ClearDataBase.IsEnabled = false;
        }
        private void DataBaseButtons_GreyOut()
        {
            Button_ClearDataBase.IsEnabled = false;
            Button_LoadDataBase.IsEnabled = false;
        }
        #endregion

        #region Event Handlers
        private void Button_ClearDataBase_Click(object sender, RoutedEventArgs e)
        {
            DataBase_Clear();
            Button_ClearDataBase.IsEnabled = false;
            Button_LoadDataBase.IsEnabled = false; // TODO: FEATURE - should be set to true when feature is enabled...
        }
        
        private void Button_Run_Click(object sender, RoutedEventArgs e)
        {
            string ext = "";

            Clear();

            if (ComboBox_Filter.Text == null || ComboBox_Filter.Text == "")
            {
                DataBase_Load("database.db", ext);
            }
            else if (!(ComboBox_Filter.Text.Substring(0).Contains(".")))
            {
                ext = ComboBox_Filter.Text.Insert(0, ".");
                DataBase_Load("database.db", " WHERE ext = '" + ext + "' ");
            }
            else
            {
                ext = ComboBox_Filter.Text;
                DataBase_Load("database.db", " WHERE ext = '" + ext + "' ");
            }
            
        }
        #endregion
    }
}
