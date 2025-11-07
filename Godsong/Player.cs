using System;
using System.Collections.Generic;

namespace Godsong
{
    public class Player
    {
        // 🔸 Basic Information
        public string Name { get; set; }

        // 🔸 Base Stats
        public int BaseHP { get; private set; } = 50;
        public int BaseAttack { get; private set; } = 5;
        public int BaseDefense { get; private set; } = 2;

        // 🔸 Current Stats
        public int MaxHP { get; private set; }
        public int HP { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }

        // 🔸 Dice
        public int DiceSides { get; private set; } = 6;
        private static readonly Random rand = new Random();

        // 🔸 Progression
        public int Gold { get; private set; }
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; }

        // 🔸 Card / Class System
        public Card CurrentCard { get; private set; }

        // 🔸 Skills
        public List<string> Skills { get; private set; }

        // 🔸 States / Buffs
        public bool IsCountering { get; private set; } = false;
        public List<StatusEffect> ActiveEffects { get; private set; } = new List<StatusEffect>();


        // ============================================================
        // 🔹 Constructor
        // ============================================================
        public Player(string name)
        {
            Name = name;
            MaxHP = BaseHP;
            HP = MaxHP;
            Attack = BaseAttack;
            Defense = BaseDefense;
            Gold = 0;
            Experience = 0;
            Skills = new List<string>();

            // Equip default Human card
            Card defaultCard = CardLibrary.GetCard("Human");
            EquipCard(defaultCard);
        }


        // ============================================================
        // 🎲 Dice Roll
        // ============================================================
        public int RollDice()
        {
            return rand.Next(1, DiceSides + 1);
        }


        // ============================================================
        // ⚔️ Combat
        // ============================================================
        public void AttackTarget(Enemy target)
        {
            int roll = RollDice();
            int damage = Attack + roll - target.Defense;
            if (damage < 0) damage = 0;

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
                Util.TypeWrite(" has fallen.");
            }
        }


        // ============================================================
        // 💚 Healing
        // ============================================================
        public void Heal(int amount)
        {
            HP += amount;
            if (HP > MaxHP) HP = MaxHP;

            Util.TypeWriteMultiColor(new (string, ConsoleColor)[]
            {
                ($"{Name}", ConsoleColor.Green),
                (" heals", ConsoleColor.White),
                ($" {amount}", ConsoleColor.Yellow),
                ($" HP. HP: {HP}/{MaxHP}", ConsoleColor.White)
            });
        }


        // ============================================================
        // 🪄 Card / Class System
        // ============================================================
        public void EquipCard(Card card)
        {
            CurrentCard = card;
            Attack = BaseAttack + card.AttackModifier;
            MaxHP = BaseHP + card.HPModifier;
            Defense = BaseDefense + card.DefenseModifier;
            HP = MaxHP;
        }


        // ============================================================
        // 📈 Stat Changes
        // ============================================================
        public void ModifyStats(int attack = 0, int maxHP = 0, int defense = 0, int heal = 0)
        {
            Attack += attack;
            MaxHP += maxHP;
            Defense += defense;
            if (heal > 0) Heal(heal);
        }


        // ============================================================
        // 🛡️ Counter System
        // ============================================================
        public void ActivateCounter() => IsCountering = true;
        public void DeactivateCounter() => IsCountering = false;


        // ============================================================
        // ☠️ Status Effects
        // ============================================================
        public void AddEffect(StatusEffect effect, Enemy enemy)
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
                effect.Apply(this, enemy);
                Util.TypeWrite($"{Name} gains {effect.Name} for {effect.Duration} turn(s)!");
            }
        }

        public void ProcessEffects(Enemy enemy)
        {
            for (int i = ActiveEffects.Count - 1; i >= 0; i--)
            {
                ActiveEffects[i].Tick(this, enemy);
                if (ActiveEffects[i].IsExpired)
                    ActiveEffects.RemoveAt(i);
            }
        }
    }
}
