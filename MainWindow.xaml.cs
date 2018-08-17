/**
 * Name: Matthew Jett
 * Class: CSCD 371-01 .Net Programming with C# | Tom Capaul | Spring 2018
 * Description: This class is both the code for the GUI events and the backend code for the FileSystemWatcher object.
 * Notes: This is my MainWindow. Serving as the home base or hub for the other menu item pages to overlay on.
 *        FOR GRADER!: Tom gave me permission to turn this assignment in late, please refer to him.
 *        DESIGN JUSTIFICATION: I know the assignment requires us to implement a menu with sub-menu items,
 *                              but since the functionality of this app is simple enough I used my own
 *                              design judgement to go with a simple 3 tab design. Each tab contains all the info
 *                              needed to represent one of the menu items asked for in the assignment requirments.
 *                              Same goes with toolbar buttons, adding these will create more redundencies and
 *                              will only create more clutter and confuse the user. This app was built to be ready
 *                              for updates and new features down the road as I continue to work on this for my portfolio.
 * References: Custom Stylesheets: https://stackoverflow.com/questions/6745663/how-to-create-make-rounded-corner-buttons-in-wpf
 *             Folder Browser: https://stackoverflow.com/questions/4547320/using-folderbrowserdialog-in-wpf-application
 */

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace EyeInTheSky
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml + backend code for FileSystemWatcher project
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private FileSystemWatcher _watcher = new FileSystemWatcher();
        private ObservableCollection<string[]> _eventList = new ObservableCollection<string[]>();
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            Main.Source = new Uri("pack://EyeInTheSky:,,,/MonitorPage.xaml", UriKind.Absolute);
            GreyOutCurrPageButton();
        }

        private void GreyOutCurrPageButton()
        {
            if (Main.Source.Equals("pack://EyeInTheSky:,,,/MonitorPage.xaml"))
            {
                Main.Content = new MonitorPage(ref _watcher, ref _eventList);

                Menu_MonitorButton.IsEnabled = false; // DISABLE
                Menu_DataBaseButton.IsEnabled = true;
                Menu_HelpButton.IsEnabled = true;
            }
            else if (Main.Source.Equals("pack://EyeInTheSky:,,,/DataBasePage.xaml"))
            {
                Main.Content = new DataBasePage(ref _eventList);

                Menu_MonitorButton.IsEnabled = true;
                Menu_DataBaseButton.IsEnabled = false; // DISABLE
                Menu_HelpButton.IsEnabled = true;
            }
            else if (Main.Source.Equals("pack://EyeInTheSky:,,,/HelpPage.xaml"))
            {
                Main.Content = new HelpPage();

                Menu_MonitorButton.IsEnabled = true;
                Menu_DataBaseButton.IsEnabled = true;
                Menu_HelpButton.IsEnabled = false; // DISABLE
            }
        }

        #region GUI Events
        private void Menu_MonitorButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Source = new Uri("pack://EyeInTheSky:,,,/MonitorPage.xaml", UriKind.Absolute);
            GreyOutCurrPageButton();
        }

        private void MenuStrip_DataBaseButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: BUG(HIGH) - if clicked on before database created will crash
            Main.Source = new Uri("pack://EyeInTheSky:,,,/DataBasePage.xaml", UriKind.Absolute);
            GreyOutCurrPageButton();
        }

        private void MenuStrip_HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Source = new Uri("pack://EyeInTheSky:,,,/HelpPage.xaml", UriKind.Absolute);
            GreyOutCurrPageButton();
        }
        #endregion
    }
}
