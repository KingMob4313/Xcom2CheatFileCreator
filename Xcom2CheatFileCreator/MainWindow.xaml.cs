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

        private string outputFileName = string.Empty;
        private int levelup = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadCsvButton_Click(object sender, RoutedEventArgs e)
        {
            soldierList = SoldierCSVReader.ProcessSoldierFile(soldierList, outputFileName, StatGrid);
        }

        private void GenerateFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            int.TryParse(LevelSet.Text, out levelup);
            sfd.InitialDirectory = @"G:\Program Files (x86)\Steam\SteamApps\common\XCOM 2\Binaries";
            // Show file dialog box
            Nullable<bool> result = sfd.ShowDialog();
            if (result == true)
            {
                char driveLetter = Convert.ToChar(DriveLetterSet.Text);
                outputFileName = sfd.FileName;
                soldierList = SoldierTextFileGenerator.ProcessSoldierFile(soldierList, outputFileName, levelup, driveLetter, LongWarCheckBox.IsChecked);
                MessageBox.Show("File Created Successfully.");
            }
        }
    }
}