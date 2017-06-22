using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Xcom2CheatFileCreator
{
    internal static class SoldierCSVReader
    {
        public static List<Soldier> ProcessSoldierFile(List<Soldier> soldierListInstance, string inputFileName, DataGrid currentDataGrid)
        {
            List<Soldier> currentSoldierListInstance;
            currentSoldierListInstance = (soldierListInstance == null) ? new List<Soldier>() : soldierListInstance;

            try
            {
                var streamReader = new StreamReader(inputFileName);
                var csv = new CsvReader(streamReader);

                while (csv.Read())
                {
                    if (csv.GetField<string>(3) != "I" && csv.GetField<string>(4) != string.Empty)
                    {
                        Soldier currentSoldier = new Soldier();
                        currentSoldier.FirstName = csv.GetField<string>(0).TrimEnd();
                        currentSoldier.LastName = csv.GetField<string>(1).TrimEnd();
                        Debug.WriteLine(currentSoldier.FirstName + " " + currentSoldier.LastName);
                        //string grabbedClass = csv.GetField<string>(2).TrimEnd();
                        //LWClass colorValue = (LWClass)Enum.Parse(typeof(LWClass), grabbedClass);
                        //currentSoldier.SoldierClass = colorValue;
                        currentSoldier.SoldierClass = csv.GetField<string>(2).TrimEnd();
                        int tempStrength;
                        int.TryParse(csv.GetField<string>(3).TrimEnd(), out tempStrength);
                        currentSoldier.Strength = tempStrength > 50 ? tempStrength : 50;
                        currentSoldier.Health = csv.GetField<int>(4);
                        currentSoldier.Mobility = csv.GetField<int>(5);
                        currentSoldier.SightRadius = csv.GetField<int>(6);
                        currentSoldier.Aim = csv.GetField<int>(7);
                        currentSoldier.CritChance = csv.GetField<int>(8);
                        currentSoldier.ArmorPierce = csv.GetField<int>(9);
                        currentSoldier.FlankingAim = csv.GetField<int>(10);
                        currentSoldier.FlankingCrit = csv.GetField<int>(11);
                        currentSoldier.PsiOffensive = csv.GetField<int>(12);
                        currentSoldier.Will = csv.GetField<int>(13);
                        currentSoldier.Dodge = csv.GetField<int>(14);
                        currentSoldier.Armor = csv.GetField<int>(15);
                        currentSoldier.Hack = csv.GetField<int>(16);
                        //currentSoldier.UtilitySlots = csv.GetField<int>(17);

                        currentSoldierListInstance.Add(currentSoldier);
                    }
                    else if (csv.GetField<string>(4) == string.Empty)
                    {
                        break;
                    }
                }
                currentDataGrid.ItemsSource = currentSoldierListInstance;
                streamReader.Close();
                streamReader.Dispose();
                csv.Dispose();
                return currentSoldierListInstance;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error");
            }

            return null;
        }
    }
}