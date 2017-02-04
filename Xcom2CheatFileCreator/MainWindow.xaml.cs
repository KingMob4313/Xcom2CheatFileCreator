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

        private string inputFileName = "cheatfiletemplate.csv";
        private int levelup = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            soldierList = SoldierCSVReader.ProcessSoldierFile(soldierList, inputFileName, StatGrid);
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
            string outputFile = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            int.TryParse(LevelSet.Text, out levelup);
            sfd.InitialDirectory = @"G:\Program Files (x86)\Steam\SteamApps\common\XCOM 2\Binaries";
            // Show file dialog box
            Nullable<bool> result = sfd.ShowDialog();
            if (result == true)
            {
                char driveLetter = Convert.ToChar(DriveLetterSet.Text);
                inputFileName = sfd.FileName;
                soldierList = SoldierTextFileGenerator.ProcessSoldierFile(soldierList, inputFileName, levelup, driveLetter, LongWarCheckBox.IsChecked);
                MessageBox.Show("File Created Successfully.");
            }
        }

        private void AddSoldierButton_Click(object sender, RoutedEventArgs e)
        {
            soldierList.Add(new Soldier());
            this.StatGrid.Items.Refresh();
        }

        private void ExportCSVButton_Click(object sender, RoutedEventArgs e)
        {
            string outputFile = string.Empty;
            SaveFileDialog csvsfd = new SaveFileDialog();
            csvsfd.Filter = "CSV documents (.csv)|*.csv"; //filter files by extension
            int.TryParse(LevelSet.Text, out levelup);
            csvsfd.InitialDirectory = @"G:\Program Files (x86)\Steam\SteamApps\common\XCOM 2\Binaries";
            // Show file dialog box
            Nullable<bool> result = csvsfd.ShowDialog();
            if (result == true)
            {
                TextWriter tr = File.CreateText(csvsfd.FileName);
                CsvWriter csvW = new CsvWriter(tr);
                csvW.WriteHeader(typeof(Soldier));
                foreach (Soldier item in soldierList)
                {
                    csvW.WriteRecord(item);
                }
                csvW.Dispose();
            }
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }
    }
}