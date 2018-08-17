/**
 * Name: Matthew Jett
 * Class: CSCD 371-01 .Net Programming with C# | Tom Capaul | Spring 2018
 * Description: This class is my Monitor menu. Here is where you start and stop the FileSystemWatcher, choose a directory or file to watch, specify a filter for directories, and export contents out.
 */

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SQLite;
using WinForms = System.Windows.Forms;
using System.Collections.Specialized;

namespace EyeInTheSky
{
    /// <summary>
    /// Interaction logic for MonitorPage.xaml
    /// </summary>
    public partial class MonitorPage : Page
    {
        #region Fields
        private FileSystemWatcher _watcher;
        private string[] _event;
        private ObservableCollection<string[]> _eventList;
        private bool _isOn;
        #endregion

        #region class Constructor
        public MonitorPage(ref FileSystemWatcher watcher, ref ObservableCollection<string[]> eventList)
        {
            InitializeComponent();

            _watcher = watcher;
            _eventList = eventList;

            _isOn = false;
            DataGrid_DisplayOutput.ItemsSource = _eventList;

            var contentsInGrid = DataGrid_DisplayOutput.ItemsSource as ObservableCollection<string[]>;
            if (contentsInGrid == null) return;
            contentsInGrid.CollectionChanged += new NotifyCollectionChangedEventHandler(DataGrid_DataChanged);

            ExportButtons_GreyOut();
        }
        #endregion

        #region FileSystemWatcher Methods
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (e != null)
            {
                _event = new string[5];
                _event[0] = e.ChangeType.ToString();
                _event[1] = e.Name;
                _event[2] = e.FullPath;
                _event[3] = DateTime.Now.ToString();
                if (e.Name.Contains("."))
                    _event[4] = e.Name.Substring(e.Name.LastIndexOf("."));

                SetThreadSafe();
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            if (e != null)
            {
                _event = new string[5];
                _event[0] = e.ChangeType.ToString();
                _event[1] = e.Name;
                _event[2] = e.FullPath;
                _event[3] = DateTime.Now.ToString();
                if (e.Name.Contains("."))
                    _event[4] = e.Name.Substring(e.Name.LastIndexOf("."));

                SetThreadSafe();
            }
        }

        private delegate void SetCallback();
        private void SetThreadSafe()
        {
            if (!CheckAccess())
            {
                SetCallback d = new SetCallback(SetThreadSafe);
                Dispatcher.Invoke(d);
            }
            else
            {
                _eventList.Add(_event);
            }
        }
        #endregion

        #region Start/Stop Methods
        private void Start()
        {
            // NEW FILESYSTEMWATCHER everytime START is pressed - new instance of watcher monitoring
            _watcher = new FileSystemWatcher();

            // PATH ASSIGNMENT
            if (TextBox_PathInputField.Text == "")
            {
                // Default START press without any fields in Path or Filter
                // TODO: grey out start button until TextBox_PathInputField.Text contians content...
                _watcher.Path = "C:";
                CheckBox_SubFolders.IsChecked = true;
                _watcher.IncludeSubdirectories = true;
            }

            // FILTER ASSIGNMENT
            if (ComboBox_Filter.SelectedIndex <= 0 && ComboBox_Filter.Text == "")   // IF: selected nothing or nothing is in field
                _watcher.Filter = ComboBox_Filter.Text = "*.*";
            else if (!(ComboBox_Filter.Text.Substring(0).Contains(".")))            // IF: user enters extension without typing a '.', add it for them
                _watcher.Filter = ComboBox_Filter.Text.Insert(0, "*.");
            else if (!ComboBox_Filter.Text.Substring(0).Contains("*"))              // IF: user enters file extension without a '*', add it for them
                _watcher.Filter = ComboBox_Filter.Text.Insert(0, "*");
            else                                                                    // THEN: user must have correctly formatted a file type extension
                _watcher.Filter = ComboBox_Filter.Text;

            // PATH ASSIGNMENT
            _watcher.Path = TextBox_PathInputField.Text + "\\";

            // SUBFOLDER SELECTION
            if (CheckBox_SubFolders.IsChecked.GetValueOrDefault())
                _watcher.IncludeSubdirectories = true;

            _watcher.Changed += new FileSystemEventHandler(OnChanged);
            _watcher.Created += new FileSystemEventHandler(OnChanged);
            _watcher.Deleted += new FileSystemEventHandler(OnChanged);
            _watcher.Renamed += new RenamedEventHandler(OnRenamed);
            _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            Button_Start.Content = "STOP";
            _isOn = true;
            _watcher.EnableRaisingEvents = true;
        }

        private void Stop()
        {
            Button_Start.Content = "START";
            _isOn = false;
            _watcher.EnableRaisingEvents = false;

            _watcher.Changed -= new FileSystemEventHandler(OnChanged);
            _watcher.Created -= new FileSystemEventHandler(OnChanged);
            _watcher.Deleted -= new FileSystemEventHandler(OnChanged);
            _watcher.Renamed -= new RenamedEventHandler(OnRenamed);
            //_watcher.Dispose(); // this is causing a crash, is it nessessary?
            TextBox_PathInputField.Text = ""; // TODO: IF this chnaged after export button pressed making watcher stop, if user chnages this, then starting watching new path and add to existing _eventList
            ComboBox_Filter.Text = "";  // TODO: IF this chnaged also, then filter new type, maybe use OnChnaged event handlers for these elements...
            CheckBox_SubFolders.IsChecked = false;            
        }
        #endregion

        #region Helper Methods
        private void AskToExport()
        {
            MessageBoxResult result = MessageBox.Show("THE DATA TABLE IS ABOUT TO BE WIPED!\nHitting OK will continue and clear the data from the table. If you wish to keep this data, select CANCEL and then select either export option located above the START button.", "WARNING", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                Clear();
            }
        }

        private void AskToClear()
        {
            MessageBoxResult result = MessageBox.Show("Select YES if you want to clear the data table?\nOtherwise NO if you want to append new results to current data.", "CLEAR DATA?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Clear();
            }
        }

        private void Clear()
        {
            DataGrid_DisplayOutput.ItemsSource = null;
            DataGrid_DisplayOutput.Items.Clear();
            _eventList.Clear();
        }
        #endregion

        #region GUI Formatting - greying out elements
        private void DataGrid_DataChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DataGrid_DisplayOutput.HasItems)
                ExportButtons_Enable();
            else
                ExportButtons_GreyOut();
        }
        private void ExportButtons_GreyOut()
        {
            Button_ExportToDataBase.IsEnabled = false;
            Button_DumpToTextFile.IsEnabled = false;
        }
        private void ExportButtons_Enable()
        {
            Button_ExportToDataBase.IsEnabled = true;
            Button_DumpToTextFile.IsEnabled = false; // TODO: feature not completed yet
        }
        #endregion

        #region Events Handlers
        private void Button_Browse_Click(object sender, RoutedEventArgs e)
        {
            // FEATURE NOTE: this is for folder selecting only
            WinForms.FolderBrowserDialog folderBrowser = new WinForms.FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop
            };
            WinForms.DialogResult folderBrowser_Status = folderBrowser.ShowDialog();
            if (folderBrowser_Status.ToString() == "OK")
                TextBox_PathInputField.Text = folderBrowser.SelectedPath;

            // TODO: FEATURE NOTE: this is for file selecting only
            //WinForms.OpenFileDialog openFileBrowser = new WinForms.OpenFileDialog();
            //WinForms.DialogResult openFileBrowser_Status = openFileBrowser.ShowDialog();
            //TextBox_PathInputField.Text = openFileBrowser.FileName;
            //if (openFileBrowser_Status.ToString() == "OK")
            //    TextBox_PathInputField.Text = openFileBrowser.FileName;
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            // TURN OFF
            if (_isOn)
            {
                Stop();
                AskToExport();
            }
            // TURN ON
            else
            {
                if (_eventList.Count > 0)
                {
                    AskToClear();
                }
                DataGrid_DisplayOutput.ItemsSource = _eventList;
                Start();
            }
        }

        private void Button_ExportToDataBase_Click(object sender, RoutedEventArgs e)
        {
            if (_watcher.EnableRaisingEvents == true)
                Stop();
            
            SQLiteConnection connect = new SQLiteConnection("Data Source=database.db;");
            SQLiteCommand insert = connect.CreateCommand();
            SQLiteCommand build = connect.CreateCommand();

            connect.Open();
            build.CommandText = "CREATE TABLE if not exists database(entry VARCHAR, change VARCHAR, file VARCHAR, ext VARCHAR, path VARCHAR, modified VARCHAR);";
            build.ExecuteNonQuery();

            for (int i = 0; i < _eventList.Count; i++)
            {
                insert.CommandText = "INSERT INTO database(entry, change, file, ext, path, modified) VALUES(?, ?, ?, ?, ?, ?);";
                insert.Parameters.Add("@entry", DbType.String).Value = i;
                insert.Parameters.Add("@change", DbType.String).Value = _eventList[i][0];
                insert.Parameters.Add("@file", DbType.String).Value = _eventList[i][1];
                insert.Parameters.Add("@ext", DbType.String).Value = _eventList[i][4];
                insert.Parameters.Add("@path", DbType.String).Value = _eventList[i][2];
                insert.Parameters.Add("@modified", DbType.String).Value = _eventList[i][3];
                insert.ExecuteNonQuery();
            }

            // TODO: FEATURE - add ability to specify database name and location to save...
            MessageBox.Show("Contents of table have been successfully exported to a file named, \"database.db\". This file is located in the same directory as the EyeInTheSky.exe.\n" +
                "To Open this database, select the \"DataBase\" tab up top. There you can view all contents of this database, filter by extension, or clear the database contents completely.", "SUCCESSFUL");

            connect.Close();
            Clear();
        }
        #endregion
    }
}
