using System;
using System.Runtime.InteropServices.Marshalling;

namespace Godsong
{
    public class Enemy
    {
        // 💀 Basic info
        public string Name { get; private set; }

        // ❤️ Stats
        public int MaxHP { get; private set; }
        public int HP { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }

        // 🎲 Random dice roll (for attack variation)
        public int DiceSides { get; private set; }
        
        //Rewards
        public int ExperienceReward {get; private set;}
        public int GoldReward {get; private set;}

        // Constructor - sets up the enemy
        public Enemy(string name, int maxHP, int attack, int defense, int diceSides, int expReward, int goldReward)
        {
            Name = name;
            MaxHP = maxHP;
            HP = MaxHP;
            Attack = attack;
            Defense = defense;
            DiceSides = diceSides;
            ExperienceReward = expReward;
            GoldReward = goldReward;
        }

        //Roll a dice 
        public int RollDice() 
        {
            Random rand = new Random();
            return rand.Next(1, DiceSides + 1);
        }

        //Attack the player
        public void AttackTarget(Player player)
        {
            int roll = RollDice();
            int damage = Attack + roll - player.Defense;

            if ( damage < 0)
            damage = 0;

            Console.WriteLine($"{Name} attacks {player.Name} and rolls a {roll}!");
            player.TakeDamage(damage);
        }

        //Take damage from player
        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP < 0) HP = 0;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Name}");
            Console.ResetColor();
            Console.WriteLine($" takes {amount} damage. HP remaining: {HP}/{MaxHP}");

            if (HP == 0)
            {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Name}");
            Console.ResetColor();
            Console.WriteLine(" has been defeated!");
            }
        }

        public bool IsDead()
        {
            return HP <= 0;
        }

        //Heal or regenerate (future)
        public void Heal(int amount)
        {
            HP += amount;
            if (HP > MaxHP) HP = MaxHP;
            Console.WriteLine($"{Name} heals {amount} HP. HP: {HP}/{MaxHP}");
        }

        public void DisplayHP()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{Name}: ");
            Console.ResetColor();
            Console.WriteLine($"{HP}/{MaxHP} HP");  
        }
    }
}