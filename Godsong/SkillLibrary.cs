using System;
using System.Collections.Generic;

namespace Godsong
{
    public static class SkillLibrary
    {
        private static Random rng = new Random(); // single shared RNG

        // =======================
        // Human Skills
        // =======================

        public static Skill HumanStrike = new Skill(
            "Quick Slash",
            "A swift strike dealing attack + dice damage",
            0,
            SkillType.Attack,
            (player, enemy) =>
            {
                int roll = rng.Next(1, player.DiceSides + 1);
                int damage = Math.Max(0, player.Attack + roll - enemy.Defense);
                Util.TypeWrite($"{player.Name} hits {enemy.Name} with Quick Slash for {damage} damage!");
                enemy.TakeDamage(damage);
            }
        );

        public static Skill HumanCounter = new Skill(
            "Full Counter",
            "Concentrate to fully parry and reflect damage",
            0,
            SkillType.Attack,
            (player, enemy) =>
            {
                Util.TypeWrite($"{player.Name} braces for Full Counter!");

                int roll = rng.Next(1, enemy.DiceSides + 1);
                int damage = Math.Max(0, enemy.Attack + roll - player.Defense);

                Util.TypeWrite($"{player.Name} reflects {damage} damage back to {enemy.Name}!");
                enemy.TakeDamage(damage);

                Util.TypeWrite($"{player.Name} takes no damage this turn!");
            }
        );

        public static Skill HumanLaststand = new Skill(
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
            "Thrust forward, poking a hole in the opponent's flesh causing Bleed",
            5,
            SkillType.Attack,
            (player, enemy) =>
            {
                int roll = player.RollDice();
                int damage = Math.Max(0, player.Attack + roll - enemy.Defense);
                Util.TypeWrite($"{player.Name} hits {enemy.Name} with Lunge for {damage} damage!");
                enemy.TakeDamage(damage);

                // Apply cloned Bleed effect
                StatusEffect bleed = StatusEffectLibrary.Bleed.Clone();
                enemy.AddEffect(bleed, player);
            }
        );

        // =======================
        // Goblin Skills
        // =======================

        public static Skill GoblinSuckerPunch = new Skill(
            "Sucker Punch",
            "Await a moment of distraction and strike",
            0,
            SkillType.Attack,
            (player, enemy) =>
            {
                int roll = rng.Next(1, player.DiceSides + 1);
                int damage = Math.Max(0, player.Attack + roll - enemy.Defense);
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
                for (int i = 1; i <= 3; i++)
                {
                    int roll = rng.Next(1, player.DiceSides + 1);
                    int damage = Math.Max(0, player.Attack + roll - enemy.Defense);
                    Util.TypeWrite($"{player.Name} throws spear {i} for {damage} damage!");
                    enemy.TakeDamage(damage);
                }
            }
        );

        public static Skill GoblinTinkererArmor = new Skill(
            "Tinkerer's Armor",
            "Muster up various items from your loot bag to form Armor (Crumbles every turn)",
            0,
            SkillType.Buff,
            (player, enemy) =>
            {
                StatusEffect shield = StatusEffectLibrary.Shield.Clone();
                shield.Power = 10;
                shield.Duration = 3;

                shield.OnApplyEffect = (p, e) =>
                {
                    p.ModifyStats(defense: 10);
                    Util.TypeWrite($"{p.Name} forms crude armor from scraps! (+10 Defense)");
                };

                shield.TickEffect = (p, e) =>
                {
                    p.ModifyStats(defense: -2);
                    Util.TypeWrite($"{p.Name}'s armor crumbles slightly (-2 Defense)");
                };

                shield.OnExpireEffect = (p, e) =>
                {
                    p.ModifyStats(defense: -10);
                    Util.TypeWrite($"{p.Name}'s makeshift armor falls apart completely.");
                };

                player.AddEffect(shield, enemy);
            }
        );

        public static Skill GoblinOilFlare = new Skill(
            "Oil Flare",
            "Spray flammable oil, causing the target to burn over time",
            3,
            SkillType.Attack,
            (player, enemy) =>
            {
                int roll = rng.Next(1, player.DiceSides + 1);
                int damage = Math.Max(0, player.Attack + roll - enemy.Defense);
                Util.TypeWrite($"{player.Name} hits {enemy.Name} with Oil Flare for {damage} damage!");
                enemy.TakeDamage(damage);

                // Apply cloned Burn effect
                StatusEffect burnEffect = StatusEffectLibrary.Burn.Clone();
                enemy.AddEffect(burnEffect, player);
            }
        );
    }
}