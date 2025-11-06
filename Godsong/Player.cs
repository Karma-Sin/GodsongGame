using System;
using System.Collections.Generic;

namespace Godsong
{

    public class Player
    {
        //Basic information 
        public string Name { get; set; }

        // Stats
        public int MaxHP { get; private set; }
        public int HP { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }

        //Dice system
        public int DiceSides { get; private set; }

        //Progression
        public int Gold { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }

        //Cards(stance/class)
        public Card CurrentCard { get; private set; }

        //Skills
        public List<string> Skills { get; private set; }

        //Constructor
        public Player(string name)
        {
            Name = name;
            MaxHP = 50;
            HP = MaxHP;
            Attack = 5;
            Defense = 2;
            DiceSides = 6;
            Gold = 0;
            Level = 1;
            Experience = 0;
            Skills = new List<string>();
            Card defaultCard = CardLibrary.GetCard("Human");
            EquipCard(defaultCard);
        }

        //Roll a dice (1-6)
        public int RollDice()
        {
            Random rand = new Random();
            return rand.Next(1, DiceSides + 1);
        }

        // Attacking
        public void AttackTarget(Enemy target)
        {
            int roll = RollDice();
            int damage = Attack + roll - target.Defense;

            if (damage < 0)
                damage = 0;
            Console.WriteLine($"{Name} attacks {target.Name} and rolls a {roll}!");
            target.TakeDamage(damage);
        }

        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP < 0) HP = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Util.TypeWriteInline($"{Name}");
            Console.ResetColor();
            Util.TypeWrite($" takes {amount} damage. HP remaining: {HP}/{MaxHP}");

            if (HP == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Util.TypeWriteInline($"{Name}");
                Console.ResetColor();
                Util.TypeWrite(" has fallen");
            }
        }

        public void Heal(int amount)
        {
            HP += amount;
            if (HP > MaxHP)
            {
                HP = MaxHP;
                Util.TypeWriteMultiColor(new (string, ConsoleColor)[]
                {
                ($"{Name}",ConsoleColor.Green),
                (" heals",ConsoleColor.White),
                ($" {amount}", ConsoleColor.Yellow),
                ($" HP. HP: {HP}/{MaxHP}", ConsoleColor.White)
                });
            }
        }

        public void EquipCard(Card card)
        {
            CurrentCard = card;
            Attack = 5 + card.AttackModifier;
            MaxHP = 50 + card.HPModifier;
            Defense = 2 + card.DefenseModifier;
            HP = MaxHP;
        }

        //In combat stat changes

        public void ApplyBuff(int attack = 0, int maxHP = 0, int defense = 0, int heal = 0)
        {
            Attack += attack;
            MaxHP += maxHP;
            Defense += defense;
            Heal(heal);
        }

        public void ApplyDebuff(int attack = 0, int maxHP = 0, int defense = 0)
        {
            Attack += attack;
            MaxHP += maxHP;
            Defense += defense;
        }
    } 
}