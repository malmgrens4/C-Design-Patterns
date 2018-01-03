using System;

namespace PrototypePattern
{
    interface IGoblin
    {
        string Color { get; set; }
        string Weapon { get; set; }
        string Commander{ get; set; }
        void BattleCry();

    }

    interface IPrototypable
    {
        IPrototypable Clone();
    }

    class GruntGoblin : IGoblin, IPrototypable
    {
        string color;
        string weapon;
        string commander;

        public GruntGoblin(string color, string weapon, string commander)
        {
            this.color = color;
            this.weapon = weapon;
            this.commander = commander;
        }
        public string Color { get => color; set => color = value; }
        public string Weapon { get => weapon; set => weapon = value; }
        public string Commander { get => commander; set => commander = value; }

        public void BattleCry()
        {
            Console.WriteLine("I the {0} goblin am charging into battle with my {1} lead by {2}", color, weapon, commander.ToString());
        }

        public IPrototypable Clone()
        {
            return (IPrototypable)this.MemberwiseClone();
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        { 
            GruntGoblin greenGoblin = new GruntGoblin("green", "spear", "spiderman");
            //we want the orange and blue to have the same weapon 
            //and commander so we clone orange to reduce the overhead of "new" objects.
            GruntGoblin orangeGoblin = new GruntGoblin("orange", "battleaxe", "Billy the Goblin");
            GruntGoblin blueGoblin = (GruntGoblin)orangeGoblin.Clone();
            greenGoblin.BattleCry();
            orangeGoblin.BattleCry();
            blueGoblin.Color = "blue";
            blueGoblin.BattleCry();
            Console.ReadKey();
        }
    }
}
