using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2CheatFileCreator
{
    internal class Soldier
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        //public LWClass SoldierClass { get; set; }
        public string SoldierClass { get; set; }

        public int Strength { get; set; }

        public int Health { get; set; }

        public int Mobility { get; set; }

        public int SightRadius { get; set; }

        public int Aim { get; set; }

        public int CritChance { get; set; }

        public int ArmorPierce { get; set; }

        public int FlankingAim { get; set; }

        public int FlankingCrit { get; set; }

        public int PsiOffensive { get; set; }

        public int Will { get; set; }

        public int Dodge { get; set; }

        public int Armor { get; set; }

        public int Hack { get; set; }

        //disabled
        public int UtilitySlots
        {
            get
            {
                return 1;
            }
            private set
            {
                value = new int();
            }
        }

        //disabled
        public int Backpack
        {
            get { return 3; }
            private set
            {
                value = new int();
            }
        }

        //disabled
        public int Total
        {
            get { return 45; }
            private set
            {
                value = new int();
            }
        }

        public static Soldier NewSoldier()
        {
            Soldier currentSoldier = new Soldier();
            currentSoldier.FirstName = "Jay";
            currentSoldier.LastName = "Doe";
            //currentSoldier.SoldierClass = LWClass.Grenadier;
            currentSoldier.SoldierClass = "Grenadier";
            currentSoldier.Strength = 10;
            currentSoldier.Health = 5;
            currentSoldier.Mobility = 13;
            currentSoldier.SightRadius = 27;
            currentSoldier.Aim = 70;
            currentSoldier.CritChance = 0;
            currentSoldier.ArmorPierce = 0;
            currentSoldier.FlankingAim = 10;
            currentSoldier.FlankingCrit = 10;
            currentSoldier.PsiOffensive = 0;
            currentSoldier.Will = 40;
            currentSoldier.Dodge = 0;
            currentSoldier.Armor = 0;
            currentSoldier.Hack = 10;
            return currentSoldier;
        }
    }
}