using CsvHelper;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace Xcom2CheatFileCreator
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public List<string> classList = new List<string>();
        private string Xcom2Location = string.Empty;

        public Settings(string InitialLocation)
        {
            InitializeComponent();
            Xcom2Location = InitialLocation;
            Xcom2LocationTextBox.Text = Xcom2Location;
        }

        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            //string folder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            classList = SoldierClass.LoadSettingsCsv("soldierClass.csv");
            ClassListBox.ItemsSource = (classList);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            classList.Remove(ClassListBox.SelectedValue.ToString());
            ClassListBox.Items.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            classList.Add(ClassTextBox.Text.TrimEnd());
            ClassListBox.Items.Refresh();
            ClassTextBox.Text = string.Empty;
        }

        private void CSVSaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SoldierClass.WriteCSVFile(classList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
            classList = SoldierClass.LoadSettingsCsv("soldierClass.csv");
            ClassListBox.ItemsSource = classList;
            ClassListBox.Items.Refresh();
        }

        private void ResetClassesButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really wish to reset the class list?", "Confirm reset", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                classList.Clear();
                foreach (object item in Enum.GetValues(typeof(LWClass)))
                {
                    classList.Add(item.ToString());
                }
                ClassListBox.ItemsSource = classList;
                ClassListBox.Items.Refresh();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog fbd = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = fbd.ShowDialog();
            if (result == WinForms.DialogResult.OK)
            {
                Xcom2LocationTextBox.Text = fbd.SelectedPath;
            }
        }

        private void SettingsWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Resources["ClassList"] = classList;
            Application.Current.Resources["Xcom2Location"] = Xcom2LocationTextBox.Text;
        }
    }
}