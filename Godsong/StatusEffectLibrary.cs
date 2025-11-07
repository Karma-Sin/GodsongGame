using System;

namespace Godsong
{
    public static class StatusEffectLibrary
    {
        // =====================
        // Damage over Time
        // =====================

        public static StatusEffect Bleed = new StatusEffect(
            name: "Bleed",
            description: "Lose HP each turn",
            duration: 3,
            power: 2,
            isBuff: false,
            tickEffect: (player, enemy) =>
            {
                enemy.TakeDamage(Bleed.Power);
                Util.TypeWrite($"{enemy.Name} suffers {Bleed.Power} bleed damage!");
            },
            onApplyEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} starts bleeding!");
            },
            onExpireEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} stops bleeding.");
            }
        );

        public static StatusEffect Burn = new StatusEffect(
            name: "Burn",
            description: "Lose HP each turn from burning",
            duration: 3,
            power: 2,
            isBuff: false,
            tickEffect: (player, enemy) =>
            {
                enemy.TakeDamage(Burn.Power);
                Util.TypeWrite($"{enemy.Name} suffers {Burn.Power} burn damage!");
            },
            onApplyEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} is set on fire!");
            },
            onExpireEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} stops burning.");
            }
        );

        public static StatusEffect Poison = new StatusEffect(
            name: "Poison",
            description: "Lose HP each turn",
            duration: 4,
            power: 3,
            isBuff: false,
            tickEffect: (player, enemy) =>
            {
                enemy.TakeDamage(Poison.Power);
                Util.TypeWrite($"{enemy.Name} takes {Poison.Power} poison damage!");
            },
            onApplyEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} is poisoned!");
            },
            onExpireEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} recovers from poison.");
            }
        );

        // =====================
        // Buffs
        // =====================

        public static StatusEffect Shield = new StatusEffect(
            name: "Shield",
            description: "Gain extra defense",
            duration: 2,
            power: 5,
            isBuff: true,
            tickEffect: null,
            onApplyEffect: (player, enemy) =>
            {
                player.ModifyStats(defense: Shield.Power);
                Util.TypeWrite($"{player.Name} gains a shield (+{Shield.Power} Defense)!");
            },
            onExpireEffect: (player, enemy) =>
            {
                player.ModifyStats(defense: -Shield.Power);
                Util.TypeWrite($"{player.Name}'s shield fades (-{Shield.Power} Defense).");
            }
        );

        public static StatusEffect AttackUp = new StatusEffect(
            name: "Attack Up",
            description: "Increases Attack",
            duration: 2,
            power: 10,
            isBuff: true,
            tickEffect: null,
            onApplyEffect: (player, enemy) =>
            {
                player.ModifyStats(attack: AttackUp.Power);
                Util.TypeWrite($"{player.Name} is powered up! (+{AttackUp.Power} Attack)");
            },
            onExpireEffect: (player, enemy) =>
            {
                player.ModifyStats(attack: -AttackUp.Power);
                Util.TypeWrite($"{player.Name} is no longer powered up.");
            }
        );

        public static StatusEffect Haste = new StatusEffect(
            name: "Haste",
            description: "Grants extra action per turn",
            duration: 2,
            power: 1,
            isBuff: true,
            tickEffect: null,
            onApplyEffect: (player, enemy) =>
            {
                player.AddExtraAction(Haste.Power);
                Util.TypeWrite($"{player.Name} is hasted! Gains {Haste.Power} extra action(s) this turn.");
            },
            onExpireEffect: (player, enemy) =>
            {
                player.UseAction(); // remove one extra action if still present
                Util.TypeWrite($"{player.Name}'s Haste fades.");
            }
        );

        // =====================
        // Debuffs
        // =====================

        public static StatusEffect Stun = new StatusEffect(
            name: "Stun",
            description: "Cannot act for 1 turn",
            duration: 1,
            power: 0,
            isBuff: false,
            tickEffect: null,
            onApplyEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} is stunned and cannot act!");
            },
            onExpireEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} is no longer stunned.");
            }
        );

        public static StatusEffect DefenseBreak = new StatusEffect(
            name: "Defense Break",
            description: "Reduces Defense",
            duration: 3,
            power: 2,
            isBuff: false,
            tickEffect: null,
            onApplyEffect: (player, enemy) =>
            {
                enemy.ModifyStats(defense: -DefenseBreak.Power);
                Util.TypeWrite($"{enemy.Name}'s Defense is reduced by {DefenseBreak.Power}!");
            },
            onExpireEffect: (player, enemy) =>
            {
                enemy.ModifyStats(defense: DefenseBreak.Power);
                Util.TypeWrite($"{enemy.Name}'s Defense is restored by {DefenseBreak.Power}.");
            }
        );

        public static StatusEffect Weakness = new StatusEffect(
            name: "Weakness",
            description: "Reduces Attack",
            duration: 3,
            power: 2,
            isBuff: false,
            tickEffect: null,
            onApplyEffect: (player, enemy) =>
            {
                enemy.ModifyStats(attack: -Weakness.Power);
                Util.TypeWrite($"{enemy.Name}'s Attack is reduced by {Weakness.Power}!");
            },
            onExpireEffect: (player, enemy) =>
            {
                enemy.ModifyStats(attack: Weakness.Power);
                Util.TypeWrite($"{enemy.Name}'s Attack is restored by {Weakness.Power}.");
            }
        );

        // =====================
        // Special / Misc
        // =====================

        public static StatusEffect Rage = new StatusEffect(
            name: "Rage",
            description: "Gain Attack each time you take damage",
            duration: 3,
            power: 1,
            isBuff: true,
            tickEffect: null,
            onApplyEffect: null,
            onExpireEffect: null
        );

        public static StatusEffect Overcharge = new StatusEffect(
            name: "Overcharge",
            description: "Boost next skill, but take recoil",
            duration: 1,
            power: 5,
            isBuff: true,
            tickEffect: null,
            onApplyEffect: null,
            onExpireEffect: null
        );
    }
}