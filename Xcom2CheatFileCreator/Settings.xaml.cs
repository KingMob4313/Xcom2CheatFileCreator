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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public List<string> classList = new List<string>();

        public Settings()
        {
            InitializeComponent();
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

        private void SettingsWindow_Closed(object sender, EventArgs e)
        {

            Application.Current.Resources["ClassList"] = classList;
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Enter new class name as what it's referenced in the mod.", "Notice");
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
    }
}
