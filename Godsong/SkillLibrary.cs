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
                player.ModifyStats(defense: 5, heal: 20);
            }
        );
        public static Skill HumanLunge = new Skill(
        "Lunge",
        "Thrust forward, poking a hole in the opponents flesh causing Bleed",
        5,      //base damage
        SkillType.Attack,
        (player, enemy) =>
        {   //deal damage
            int roll = player.RollDice();
            int damage = player.Attack + roll - enemy.Defense;
            if (damage < 0) damage = 0;
            enemy.TakeDamage(damage);
            //Apply effect
            StatusEffect bleed = StatusEffectLibrary.Bleed.Clone();
            enemy.AddEffect(bleed, player);
        }
        );

public static Skill GoblinSuckerPunch = new Skill(
"Sucker Punch",
"Await a moment of distraction and strike",
0,
SkillType.Attack,
(player, enemy) =>
{
Random rng = new Random();
                int roll = rng.Next(1, player.DiceSides + 1);
                int damage = player.Attack + roll - enemy.Defense;
                if (damage < 0) damage = 0;
                Util.TypeWrite($"{player.Name} hits {enemy.Name} with Sucker Punch for {damage} damage!");
                enemy.TakeDamage(damage);
}
);

public static Skill GoblinLanceBarrage = new Skill(
"Lance Barrage",
"Throw 3 makeshift spears",
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

Random rng = new Random();
                int roll = rng.Next(1, player.DiceSides + 1);
                int damage = player.Attack + roll - enemy.Defense;
                if (damage < 0) damage = 0;
                Util.TypeWrite($"{player.Name} hits {enemy.Name} with Quick Slash for {damage} damage!");
                enemy.TakeDamage(damage);

Random rng = new Random();
                int roll = rng.Next(1, player.DiceSides + 1);
                int damage = player.Attack + roll - enemy.Defense;
                if (damage < 0) damage = 0;
                Util.TypeWrite($"{player.Name} hits {enemy.Name} with Quick Slash for {damage} damage!");
                enemy.TakeDamage(damage);
}
);

public static Skill GoblinTinkererArmor = new Skill(
"",
"",
5,
SkillType.Attack,
(player, enemy) =>
{
            StatusEffect bleed = StatusEffectLibrary.Bleed.Clone();
            enemy.AddEffect(bleed, player);
}
);

    }

}