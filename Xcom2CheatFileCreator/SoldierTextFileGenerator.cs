using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using CsvHelper;
using MahApps.Metro.Controls;

namespace Xcom2CheatFileCreator
{
    internal static class SoldierTextFileGenerator
    {
        public static string PsiClassName = "PsiOperative";

        public static List<Soldier> ProcessSoldierFile(List<Soldier> soldierListInstance, string outputFileName, int level, char driveLetter, bool? isLongWar2)
        {
            StringBuilder textFor = new StringBuilder();
            textFor.AppendLine("; ---Date Generated : " + DateTime.Now.ToShortDateString() + "---");
            foreach (Soldier s in soldierListInstance)
            {
                string composedClassName = string.Empty;
                string classPrefix = string.Empty;
                if (isLongWar2 == true)
                {
                    classPrefix = "LWS_";
                }

                if (s.SoldierClass.ToString() == LWClass.Psionic.ToString())
                {
                    composedClassName = PsiClassName;
                }
                else
                {
                    composedClassName = classPrefix + s.SoldierClass.ToString();
                }
                textFor.AppendLine("; ---Set Class---");
                textFor.AppendLine("MakeSoldierAClass \"" + s.FirstName + " " + s.LastName + "\" " + composedClassName);
                textFor.AppendLine("; ---General Stats---");
                textFor.AppendLine("SetSoldierStat eStat_HP " + s.Health + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_Mobility " + s.Mobility + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_SightRadius " + s.SightRadius + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("; ---Offense Stats---");
                textFor.AppendLine("SetSoldierStat eStat_Offense " + s.Aim + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_CritChance " + s.CritChance + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_ArmorPiercing " + s.ArmorPierce + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_FlankingCritChance " + s.FlankingCrit + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_FlankingAimBonus " + s.FlankingAim + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_PsiOffense " + s.PsiOffensive + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("; ---Defense Stats---");
                textFor.AppendLine("SetSoldierStat eStat_Will " + s.Will + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_Dodge " + (Math.Ceiling(s.Dodge * 1.5)).ToString() + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_Defense " + s.Dodge + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_ArmorMitigation " + s.Armor + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("; ---Other Stats---");
                string modifiedHackSkill = (s.SoldierClass == "Specialist" ? (s.Hack * 2) : s.Hack).ToString();
                textFor.AppendLine("SetSoldierStat eStat_Hacking " + modifiedHackSkill + " " + s.FirstName + " " + s.LastName);
                //textFor.AppendLine("SetSoldierStat eStat_UtilityItems " + s.UtilitySlots + " " + s.FirstName + " " + s.LastName);
                //textFor.AppendLine("SetSoldierStat eStat_BackpackSize 4" + " " + s.FirstName + " " + s.LastName);
                //textFor.AppendLine("SetSoldierStat eStat_CombatSims 2" + " " + s.FirstName + " " + s.LastName);
                textFor.AppendLine("SetSoldierStat eStat_Strength " + s.Strength + " " + s.FirstName + " " + s.LastName);

                if (composedClassName == PsiClassName)
                {
                    textFor.AppendLine("MakeSoldierAPsiOp " + s.FirstName + " " + s.LastName);
                    //textFor.AppendLine("RankUpPsiOp " + s.FirstName + " " + s.LastName);
                    int ii = 1;
                    while (ii < level)
                    {
                        textFor.AppendLine("RankUpPsiOp " + s.FirstName + " " + s.LastName);
                        ii++;
                    }
                }
                textFor.AppendLine("; ---NEXT SOLDIER PLEASE---");
                textFor.AppendLine(";");
            }
            if (level > 0)
            {
                textFor.AppendLine("LevelUpBarracks " + level.ToString());
            }
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();

                String initialDirectory = driveLetter + @":\Program Files (x86)\Steam\SteamApps\common\XCOM 2\Binaries";
                if (!Directory.Exists(initialDirectory))
                {
                    initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                sfd.InitialDirectory = initialDirectory;
                sfd.FileName = outputFileName;
                sfd.Filter = "Text documents (.txt)|*.txt"; //filter files by extension
                Nullable<bool> result = sfd.ShowDialog();

                if (result == true)
                {
                    StreamWriter nsw = new StreamWriter(sfd.FileName);
                    nsw.Write(textFor.ToString());
                    nsw.Close();
                    MessageBox.Show("File Created Successfully.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Generating cheat file");
            }

            return soldierListInstance;
        }

        public static void WriteCSVFile(SaveFileDialog csvsfd, List<Soldier> soldierList)
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
}