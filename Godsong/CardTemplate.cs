using System;
using System.Collections.Generic;

namespace Godsong
{
    // Template for any card or player unit
    public class CardTemplate
    {
        public string Name { get; private set; }
        public int MaxHP { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int DiceSides { get; private set; }
        public List<Skill> Skills { get; private set; }

        public CardTemplate(string name, int maxHP, int attack, int defense, int diceSides, List<Skill> skills)
        {
            Name = name;
            MaxHP = maxHP;
            Attack = attack;
            Defense = defense;
            DiceSides = diceSides;
            Skills = skills;
        }

        // Generate a card instance from template (if needed)
        public Card InstantiateCard()
        {
            return new Card(Name, MaxHP, Attack, Defense, DiceSides, Skills);
        }
    }

    // Example: Card class that NPCs or player can use in battle
    public class Card
    {
        public string Name;
        public int HP;
        public int MaxHP;
        public int Attack;
        public int Defense;
        public int DiceSides;
        public List<Skill> Skills;
        public List<StatusEffect> ActiveEffects = new List<StatusEffect>();

        public Card(string name, int maxHP, int attack, int defense, int diceSides, List<Skill> skills)
        {
            Name = name;
            MaxHP = maxHP;
            HP = maxHP;
            Attack = attack;
            Defense = defense;
            DiceSides = diceSides;
            Skills = skills;
        }

        // Modify stats (buffs, heals, etc.)
        public void ModifyStats(int attack = 0, int defense = 0, int heal = 0)
        {
            Attack += attack;
            Defense += defense;
            HP += heal;
            if (HP > MaxHP) HP = MaxHP;
            if (HP < 0) HP = 0;
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
            if (HP < 0) HP = 0;
        }

        public void AddEffect(StatusEffect effect, Card source)
        {
            ActiveEffects.Add(effect);
            effect.OnApplyEffect?.Invoke(source, this);
        }
    }
}