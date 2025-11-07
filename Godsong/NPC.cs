using System;
using System.Collections.Generic;

namespace Godsong
{
    public class NPC
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int DiceSides { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();

        public NPC(string name, int hp, int attack, int defense, int diceSides, List<Skill> skills)
        {
            Name = name;
            MaxHP = hp;
            HP = hp;
            Attack = attack;
            Defense = defense;
            DiceSides = diceSides;
            Skills = skills;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0) HP = 0;
        }

        public void UseSkill(int skillIndex, Card target)
        {
            if (skillIndex < 0 || skillIndex >= Skills.Count) return;
            Skills[skillIndex].Activate(this, target);
        }
    }
}