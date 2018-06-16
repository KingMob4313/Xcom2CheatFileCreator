using CsvHelper;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Xcom2CheatFileCreator
{
    internal static class SoldierTextFileGenerator
    {
        public static string PsiClassName = "PsiOperative";

        public static List<Soldier> GenerateCheatFile(List<Soldier> soldierListInstance, string outputFileName, int level, char driveLetter, bool? isLongWar2, bool? isRichards)
        {
            StringBuilder textFor = ComposeCheatText(soldierListInstance, level, isLongWar2, isRichards);
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();

                String initialDirectory = driveLetter + @":\Program Files (x86)\Steam\SteamApps\common\XCOM 2\Binaries";
                if (!Directory.Exists(initialDirectory))
                {
                    initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    MessageBox.Show("Overridden Directory is incorrect.\r\nUsing current My Documents Folder.", "Warning");
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

        public static List<Soldier> GenerateCheatFile(List<Soldier> soldierListInstance, string outputFileName, int level, string overrideDirectory, bool? isLongWar2, bool? isRichards)
        {
            StringBuilder textFor = ComposeCheatText(soldierListInstance, level, isLongWar2, isRichards);
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();

                String initialDirectory = overrideDirectory;
                if (!Directory.Exists(initialDirectory))
                {
                    initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    MessageBox.Show("Overridden Directory is incorrect./r/n Using current My Documents Folder.", "Warning");
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
                MessageBox.Show(e.Message, "Error Generating cheat file. /r/n Cheat file not generated.");
            }

            return soldierListInstance;
        }

        private static StringBuilder ComposeCheatText(List<Soldier> soldierListInstance, int level, bool? isLongWar2, bool? isRichards)
        {
            StringBuilder textFor = new StringBuilder();

            textFor.AppendLine("; ---Date Generated : " + DateTime.Now.ToShortDateString() + "---");
            foreach (Soldier currentSoldier in soldierListInstance)
            {
                string composedClassName = string.Empty;
                string classPrefix = string.Empty;
                string classSuffix = string.Empty;
                bool isPsionicClass = false;
                string fullNameAndTrueFlag = ("  \"" + currentSoldier.FirstName + " " + currentSoldier.LastName + "\" True");
                if (isLongWar2 == true)
                {
                    //classPrefix = "LWS_";
                    classSuffix = string.Empty;
                }

                if (isRichards == true)
                {
                    //classPrefix = string.Empty;
                    classSuffix = "RS";
                }

                if (currentSoldier.SoldierClass.ToString() == WotCClass.Mage.ToString())
                {
                    PsiClassName = classPrefix + currentSoldier.SoldierClass + classSuffix;
                    isPsionicClass = true;
                }
                else if (currentSoldier.SoldierClass.ToString() == LWClass.Psionic.ToString())
                {
                    PsiClassName = classPrefix + currentSoldier.SoldierClass + classSuffix;
                    isPsionicClass = true;
                }
                if (isPsionicClass)
                {
                    composedClassName = PsiClassName;
                }
                else
                {
                    composedClassName = classPrefix + currentSoldier.SoldierClass.ToString() + classSuffix;
                }
                textFor.AppendLine("; ---Set Class---");
                textFor.AppendLine("MakeSoldierAClass \"" + currentSoldier.FirstName + " " + currentSoldier.LastName + "\" " + composedClassName);

                AddMajorStatsText(textFor, currentSoldier, fullNameAndTrueFlag);
                AddOffensiveStats(textFor, currentSoldier, fullNameAndTrueFlag);
                AddDefenseStats(textFor, currentSoldier, fullNameAndTrueFlag);

                AddSpecializedStats(level, isRichards, textFor, currentSoldier, composedClassName, fullNameAndTrueFlag);
                textFor.AppendLine("; ---NEXT SOLDIER PLEASE---");
                textFor.AppendLine(";");
            }
            if (level > 2)
            {
                textFor.AppendLine("LevelUpBarracks " + (level - 2).ToString());
            }

            return textFor;
        }

        private static void AddSpecializedStats(int level, bool? isRichards, StringBuilder textFor, Soldier s, string composedClassName, string fullNameAndTrueFlag)
        {
            textFor.AppendLine("; ---Other Stats---");

            int modifiedHackSkill = 0;
            if (s.SoldierClass == "Technician" || s.SoldierClass == "Specialist")
            {
                modifiedHackSkill = (s.Hack * 2);
            }
            else
            {
                modifiedHackSkill = s.Hack;
            }
            textFor.AppendLine("SetSoldierStat eStat_Hacking " + modifiedHackSkill + fullNameAndTrueFlag);

            //else
            //{
            //    textFor.AppendLine("SetSoldierStat eStat_Hacking " + s.Hack + " " + s.FirstName + " " + s.LastName);
            //}
            //textFor.AppendLine("SetSoldierStat eStat_UtilityItems " + s.UtilitySlots + " " + s.FirstName + " " + s.LastName);
            //textFor.AppendLine("SetSoldierStat eStat_BackpackSize 4" + " " + s.FirstName + " " + s.LastName);
            //textFor.AppendLine("SetSoldierStat eStat_CombatSims 2" + " " + s.FirstName + " " + s.LastName);
            textFor.AppendLine("SetSoldierStat eStat_Strength " + s.Strength + fullNameAndTrueFlag);

            if (composedClassName == PsiClassName && isRichards == false)
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
        }

        private static void AddDefenseStats(StringBuilder textFor, Soldier s, string fullNameAndTrueFlag)
        {
            textFor.AppendLine("; ---Defense Stats---");
            textFor.AppendLine("SetSoldierStat eStat_Will " + s.Will + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_Dodge " + (Math.Ceiling(s.Dodge * 1.5)).ToString() + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_Defense " + s.Dodge + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_ArmorMitigation " + s.Armor + fullNameAndTrueFlag);
        }

        private static void AddOffensiveStats(StringBuilder textFor, Soldier s, string fullNameAndTrueFlag)
        {
            textFor.AppendLine("; ---Offense Stats---");
            textFor.AppendLine("SetSoldierStat eStat_Offense " + s.Aim + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_CritChance " + s.CritChance + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_ArmorPiercing " + s.ArmorPierce + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_FlankingCritChance " + s.FlankingCrit + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_FlankingAimBonus " + s.FlankingAim + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_PsiOffense " + s.PsiOffensive + fullNameAndTrueFlag);
        }

        private static void AddMajorStatsText(StringBuilder textFor, Soldier s, string fullNameAndTrueFlag)
        {
            textFor.AppendLine("; ---General Stats---");
            textFor.AppendLine("SetSoldierStat eStat_HP " + s.Health + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_Mobility " + s.Mobility + fullNameAndTrueFlag);
            textFor.AppendLine("SetSoldierStat eStat_SightRadius " + s.SightRadius + fullNameAndTrueFlag);
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
            tr.Close();
            csvW.Dispose();
            tr.Dispose();
        }
    }
}