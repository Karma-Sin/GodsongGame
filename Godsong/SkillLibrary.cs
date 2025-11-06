using System;
using System.Collections.Generic;

namespace Godsong
{
    public static class SkillLIbrary
    {

        public static Skill HumanStrike = new Skill(
            "Quick Slash",

            "A swift strike dealing attack + dice damage",

            0,

            SkillType.Attack,

            (player, enemy) =>
            {
                Random rng = new Random();
                int roll = rng.Next(1, player.DiceSides + 1);
                int damage = player.Attack + roll - enemy.Defense;
                if (damage < 0) damage = 0;
                Util.TypeWrite($"{player.Name} hits {enemy.Name} with Quick Slash for {damage} damage!");
                enemy.TakeDamage(damage);
            }
        );

        public static Skill HumanCounter = new Skill(
        "Full Counter",
        "Concentate to fully parry and reflect damage",
        0,
        SkillType.Attack,
        (player, enemy) =>
        {
            Util.TypeWrite($"{player.Name} braces for Full Counter!");

            // Simulate enemy attack to calculate damage
            int roll = new Random().Next(1, enemy.DiceSides + 1);
            int damage = enemy.Attack + roll - player.Defense;
            if (damage < 0) damage = 0;

            // Reflect damage back to enemy
            Util.TypeWrite($"{player.Name} reflects {damage} damage back to {enemy.Name}!");
            enemy.TakeDamage(damage);

            // Optional: player takes no damage this turn (fully parried)
            Util.TypeWrite($"{player.Name} takes no damage this turn!");
        }
        );
        public static Skill HumanLaststand = new Skill
        (
            "Last Stand",
            "Gain 5 defense, and heal 20 HP",
            0,
            SkillType.Buff,
            (player, enemy) =>
            {
                player.ApplyBuff(defense: 5, heal: 20);
            }
        );
public static Skill HumanLunge = new Skill(
"",
"",
0,
SkillType.Attack,
(player, enemy)=>)
    }

}