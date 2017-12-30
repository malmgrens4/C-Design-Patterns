using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{

    enum CombatType
    {
        MAGIC,
        RANGE,
        MELEE
    }
    abstract class PenetrableArmor
    {
        PenetrableArmor next;
        CombatType defense;

        public PenetrableArmor(CombatType defense)
        {
            this.defense = defense;
        }

        public void SetNext(PenetrableArmor next)
        {
            this.next = next;
        }

        public void Handle(Attack attack)
        {
            if(defense == attack.AttackType)
            {
                Block(attack);
            }
            else if (next != null)
            {
                next.Handle(attack);
            }
            else
            {
                Console.WriteLine("Direct hit!");
            }
        }
        public abstract void Block(Attack attack);

    }

    class Attack
    {
        int damage;
        CombatType attackType;
        public Attack(CombatType attackType, int damage)
        {
            this.damage = damage;
            this.attackType = attackType;
        }
        public CombatType AttackType => attackType;
    }
    class FirstArmor : PenetrableArmor
    {

        public FirstArmor(CombatType defense) : base(defense) { }

        public override void Block(Attack attack)
        {
            Console.WriteLine("Blocked on the first armor! Second and Third Armor affected by how this handles it!");  
        }
    }
    class SecondArmor : PenetrableArmor
    {
        public SecondArmor(CombatType defense) : base(defense)
        {
        }

        public override void Block(Attack attack)
        {
            Console.WriteLine("Blocked on the second armor! Third Armor affected by how this handles it!");
        }
    }
    class ThirdArmor : PenetrableArmor
    {
        public ThirdArmor(CombatType defense) : base(defense)
        {
        }

        public override void Block(Attack attack)
        {
            Console.WriteLine("Blocked on the third armor! No other armors are affected!");
        }
    }

    class Hero
    {
        PenetrableArmor armor;
        public Hero(PenetrableArmor armor)
        {
            this.armor = armor;
        }
        public void Defend(Attack attack)
        {
            this.armor.Handle(attack);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PenetrableArmor genericArmor = new ThirdArmor(CombatType.MELEE);
            PenetrableArmor magicArmor = new SecondArmor(CombatType.MAGIC);
            PenetrableArmor rangeArmor = new FirstArmor(CombatType.RANGE);
            genericArmor.SetNext(magicArmor);
            magicArmor.SetNext(rangeArmor);
            Attack swordSwing = new Attack(CombatType.MELEE, 4);
            Attack magicSpell = new Attack(CombatType.MAGIC, 2);
            Attack bowShot = new Attack(CombatType.RANGE, 1);
            Hero gustalt = new Hero(genericArmor);
            gustalt.Defend(swordSwing);
            gustalt.Defend(magicSpell);
            gustalt.Defend(bowShot);
            Console.ReadKey();

        }
    }
}
