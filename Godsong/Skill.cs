using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Godsong
{
    public enum SkillType
    {
        Attack,
        Heal,
        Buff,
        Debuff,
    }
    public class Skill
    {
        public string Name { get; }
        public string Description { get; }
        public int Power { get; }       //Base damage or heal
        public SkillType Type { get; }
        public Action<Player, Enemy> Effect { get; }

        public Skill(string name, string description, int power, SkillType type, Action<Player, Enemy> effect)
        {
            Name = name;
            Description = description;
            Power = power;
            Type = type;
            Effect = effect;
        }
        public void Use(Player player, Enemy enemy)
        {
            Effect.Invoke(player, enemy);     
        }
        
    }
}