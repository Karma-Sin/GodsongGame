using System;
using System.Collections.Generic;

namespace Godsong
{
    public class Card
    {
        public string Name { get; }
        public string Description { get; }
        public int AttackModifier { get; }
        public int HPModifier { get; }
        public int DefenseModifier { get; }
        public List<Skill> Skills { get; }
        

        public Card(string name, string description, int atkMod, int hpMod, int defMod, List<Skill>? skills = null)
        {
            Name = name;
            Description = description;
            AttackModifier = atkMod;
            HPModifier = hpMod;
            DefenseModifier = defMod;
            Skills = skills ?? new List<Skill>();
        }

    }
}