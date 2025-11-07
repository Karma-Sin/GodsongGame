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
        public int ExperienceReward { get; private set; }
        public int GoldReward { get; private set; }



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
            if (damage < 0)
                damage = 0;
            if (player.IsCountering)
            {
                Util.TypeWrite($"{player.Name} parries the attack!");
                Util.TypeWrite($"{player.Name} reflects {damage} damage back to {Name}!");
                TakeDamage(damage);
                player.DeactivateCounter();
            }
            else
            {
                Console.WriteLine($"{Name} attacks {player.Name} and rolls a {roll}!");
                player.TakeDamage(damage);
            }
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

        public void ModifyStats(int attack = 0, int maxHP = 0, int defense = 0, int heal = 0)
        {
            Attack += attack;
            MaxHP += maxHP;
            Defense += defense;
            if (heal > 0) Heal(heal);
        }


        public List<StatusEffect> ActiveEffects { get; private set; } = new List<StatusEffect>();

        public void AddEffect(StatusEffect effect, Player player)
        {
            var existing = ActiveEffects.Find(e => e.Name == effect.Name);
            if (existing != null)
            {
                existing.Stack(effect.Power, effect.Duration);
                Util.TypeWrite($"{effect.Name} stacks! Power: {existing.Power}, Duration: {existing.Duration}");
            }
            else
            {
                ActiveEffects.Add(effect);
                effect.Apply(player, this);
                Util.TypeWrite($"{Name} gains {effect.Name} for {effect.Duration} turn(s)!");
            }
        }


        public void ProcessEffects(Player player)
        {
            for (int i = ActiveEffects.Count - 1; i >= 0; i--)
            {
                ActiveEffects[i].Tick(player, this);
                if (ActiveEffects[i].IsExpired)
                    ActiveEffects.RemoveAt(i);
            }
        }

        //extra action
        public int ExtraActions { get; private set; } = 0;

        public void AddExtraAction(int amount)
        {
            ExtraActions += amount;
        }

        public void UseAction()
        {
            if (ExtraActions > 0)
                ExtraActions--;
        }

        // Inside Enemy class
        public bool HasStatus(string statusName)
        {
            return ActiveEffects.Exists(e => e.Name.Equals(statusName, StringComparison.OrdinalIgnoreCase) && !e.IsExpired);
        }
    }
}