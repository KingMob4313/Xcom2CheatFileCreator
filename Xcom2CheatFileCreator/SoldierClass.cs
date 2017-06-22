using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Xcom2CheatFileCreator
{
    public static class SoldierClass
    {
        public static void WriteCSVFile(List<string> currentClassList)
        {
            TextWriter tr = File.CreateText("soldierClass.csv");
            CsvWriter csvW = new CsvWriter(tr);
            //Manually Writing Header
            csvW.WriteField("Class");
            csvW.NextRecord();
            foreach (var item in currentClassList)
            {
                csvW.WriteField(item);
                csvW.NextRecord();
            }
            tr.Close();
            csvW.Dispose();
            tr.Dispose();
        }

        public static List<string> LoadSettingsCsv(string inputFileName)
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
    }
}