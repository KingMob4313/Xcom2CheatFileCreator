using CsvHelper;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Xcom2CheatFileCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private List<Soldier> soldierList = new List<Soldier>();
        public List<string> classList = new List<string>();

        private string inputFileName = "cheatfiletemplate.csv";
        private string classFileName = "soldierClass.csv";
        private int levelup = 0;
        private string XcomDirectoryOverride = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            soldierList = SoldierCSVReader.ProcessSoldierFile(soldierList, inputFileName, StatGrid);
            classList = LoadSettingsCsv(classFileName);
            ClassComboBox.ItemsSource = classList;
            //Readonly the soldier column
        }

        private void LoadCsvButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "csv file"; //default file name
            dlg.DefaultExt = ".csv"; //default file extension
            dlg.Filter = "CSV documents (.csv)|*.csv"; //filter files by extension
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Show file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                inputFileName = dlg.FileName;
                soldierList.Clear();
                soldierList = SoldierCSVReader.ProcessSoldierFile(soldierList, inputFileName, StatGrid);
            }
            this.StatGrid.Items.Refresh();
        }

        private void GenerateFileButton_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(LevelSet.Text, out levelup);

            inputFileName = "cheat.txt";
            char driveLetter = Convert.ToChar(DriveLetterSetTextBox.Text);
            if (LocationOverride.IsChecked == true)
            {
                soldierList = SoldierTextFileGenerator.GenerateCheatFile(soldierList, inputFileName, levelup, XcomDirectoryOverride, LongWarCheckBox.IsChecked, RichardsClassesCheckBox.IsChecked);
            }
            else
            {
                soldierList = SoldierTextFileGenerator.GenerateCheatFile(soldierList, inputFileName, levelup, driveLetter, LongWarCheckBox.IsChecked, RichardsClassesCheckBox.IsChecked);
            }
        }

        private void AddSoldierButton_Click(object sender, RoutedEventArgs e)
        {
            soldierList.Add(Soldier.NewSoldier());
            this.StatGrid.Items.Refresh();
        }

        private void ExportCSVButton_Click(object sender, RoutedEventArgs e)
        {
            string outputFile = string.Empty;
            SaveFileDialog csvsfd = new SaveFileDialog();
            csvsfd.Filter = "CSV documents (.csv)|*.csv"; //filter files by extension
            int.TryParse(LevelSet.Text, out levelup);
            csvsfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // Show file dialog box
            Nullable<bool> result = csvsfd.ShowDialog();
            if (result == true)
            {
                SoldierTextFileGenerator.WriteCSVFile(csvsfd, soldierList);
            }
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MessageBox.Show("Coming soon.");
            Settings sw = new Settings(DriveLetterSetTextBox.Text.TrimEnd() + @":\Program Files (x86)\Steam\SteamApps\common\XCOM 2\Binaries");
            sw.classList = classList;
            sw.ShowDialog();
            if (Application.Current.Resources["ClassList"] != null)
            {
                classList = (List<string>)Application.Current.Resources["ClassList"];
            }
            if (Application.Current.Resources["Xcom2Location"] != null)
            {
                XcomDirectoryOverride = Application.Current.Resources["Xcom2Location"].ToString();
            }
            else
            {
                LocationOverride.IsChecked = false;
            }
            ClassComboBox.ItemsSource = classList;
            ClassComboBox.Items.Refresh();
        }

        private void StatGrid_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
        }

        private void DeleteSoldierButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatGrid.SelectedIndex > -1)
            {
                soldierList.RemoveAt(StatGrid.SelectedIndex);
                StatGrid.Items.Refresh();
            }
            else
            {
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        private void StatGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (StatGrid.SelectedIndex > -1)
            {
                DeleteButton.IsEnabled = true;
            }
            else
            {
                DeleteButton.IsEnabled = false;
            }
        }

        private List<string> LoadSettingsCsv(string inputFileName)
        {
            List<string> currentClassList = new List<string>();

            try
            {
                var streamReader = new StreamReader(inputFileName);
                var csv = new CsvReader(streamReader);

                while (csv.Read())
                {
                    if (!string.IsNullOrWhiteSpace(csv.GetField<string>(0)))
                    {
                        string currentClass = string.Empty;
                        currentClass = csv.GetField<string>(0).TrimEnd();

                        currentClassList.Add(currentClass);
                    }
                    else
                    {
                        return null;
                    }
                }
                streamReader.Close();
                streamReader.Dispose();
                csv.Dispose();
                return currentClassList;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error");
            }

            return null;
        }

        private void SetSoldierClass_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ClassComboBox.SelectedIndex > -1)
            {
                Soldier soldierToChange = new Soldier();
                soldierToChange = (Soldier)StatGrid.SelectedItems[0];
                soldierToChange.SoldierClass = ClassComboBox.SelectedValue.ToString();
                StatGrid.SelectedItems[0] = soldierToChange;
                StatGrid.Items.Refresh();
            }
        }

        private void LevelSet_LostFocus(object sender, RoutedEventArgs e)
        {
            int currentLevel = 2;

            int.TryParse(LevelSet.Text, out currentLevel);
            if (currentLevel < 2)
            {
                MessageBox.Show("Level cannot be set to below 2.", "Error");
                currentLevel = 2;
                LevelSet.Text = currentLevel.ToString();
            }
        }

        private void LocationOverride_Checked(object sender, RoutedEventArgs e)
        {
            if (LocationOverride.IsChecked == true)
            {
                DriveLetterSetTextBox.IsEnabled = false;
            }
            else
            {
                DriveLetterSetTextBox.IsEnabled = true;
            }
        }

        private void LW2_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MessageBox.Show("Adds 'LWS_' prefix to all base class names except Psionics. ", "Notice");
        }

        private void Question_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MessageBox.Show("Change the selected soldier's class to the selected class.", "Notice");
        }

        private void RichardsClassesCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            classList.Clear();
            if (RichardsClassesCheckBox.IsChecked == true)
            {
                foreach (object item in Enum.GetValues(typeof(WotCClass)))
                {
                    classList.Add(item.ToString());
                }
            }
            else
            {
                //{
                //    classList.Clear();
                //    foreach (object item in Enum.GetValues(typeof(LWClass)))
                //    {
                //        classList.Add(item.ToString());
                //    }
                //    ClassListBox.ItemsSource = classList;
                //    ClassListBox.Items.Refresh();
                //}
            }
        }
    }
}