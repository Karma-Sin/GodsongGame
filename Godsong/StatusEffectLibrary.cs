using System;

namespace Godsong
{
    public static class StatusEffectLibrary
    {
// ========================
// Status Effect Skeleton
// ========================

    // Example: Bleed (damage over time)
    public static StatusEffect Bleed = new StatusEffect(
        name: "Bleed",
        description: "Lose HP each turn",
        duration: 3,       // lasts 3 turns
        power: 2,          // damage per tick
        isBuff: false,
        tickEffect: (player, enemy) =>
        {
            // What happens each tick
            enemy.TakeDamage(2); // or use 'power'
            Util.TypeWrite($"{enemy.Name} suffers 2 bleed damage!");
        },
        onApplyEffect: (player, enemy) =>
        {
            // Optional: when first applied
            Util.TypeWrite($"{enemy.Name} starts bleeding!");
        },
        onExpireEffect: (player, enemy) =>
        {
            // Optional: when effect ends
            Util.TypeWrite($"{enemy.Name} stops bleeding.");
        }
    );

    // Example: Poison
    public static StatusEffect Poison = new StatusEffect(
        name: "Poison",
        description: "Lose HP each turn",
        duration: 4,
        power: 3,
        isBuff: false,
        tickEffect: (player, enemy) =>
        {
            enemy.TakeDamage(3);
            Util.TypeWrite($"{enemy.Name} takes 3 poison damage!");
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

    // Example: Shield (temporary defense buff)
    public static StatusEffect Shield = new StatusEffect(
        name: "Shield",
        description: "Gain extra defense for 2 turns",
        duration: 2,
        power: 5, // extra defense
        isBuff: true,
        tickEffect: (player, enemy) =>
        {
            // Could apply each turn if needed
        },
        onApplyEffect: (player, enemy) =>
        {
            player.ModifyStats(defense: 5);
            Util.TypeWrite($"{player.Name} gains a shield (+5 Defense)!");
        },
        onExpireEffect: (player, enemy) =>
        {
            player.ModifyStats(defense: -5);
            Util.TypeWrite($"{player.Name}'s shield fades (-5 Defense).");
        }
    );

        // Example: Stun (skip turn)
        public static StatusEffect Stun = new StatusEffect(
            name: "Stun",
            description: "Cannot act for 1 turn",
            duration: 1,
            power: 0,
            isBuff: false,
            tickEffect: (player, enemy) =>
            {
                // Effect handled elsewhere, e.g., skip turn in BattleSystem
            },
            onApplyEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} is stunned and cannot act!");
            },
            onExpireEffect: (player, enemy) =>
            {
                Util.TypeWrite($"{enemy.Name} is no longer stunned.");
            }
        );
    
        public static StatusEffect AttackUp = new StatusEffect(
        name: "AttackUp",
        description: "Attack is increased by 10",
        duration: 2,
        power: 10,
        isBuff: true,
        tickEffect: (player, enemy) =>
        {
            // Effect handled elsewhere, e.g., skip turn in BattleSystem
        },
        onApplyEffect: (player, enemy) =>
        {
            Util.TypeWrite($"{enemy.Name} is powered up, all attacks deal increased damage");
        },
        onExpireEffect: (player, enemy) =>
        {
            Util.TypeWrite($"{enemy.Name} is no longer powered up");
        }
    );

 // Reduces target's Defense
        public static StatusEffect DefenseBreak = new StatusEffect(
            name: "Defense Break",
            description: "Reduces target's Defense",
            duration: 3,         // default, can be modified
            power: 2,            // how much Defense is reduced
            isBuff: false,
            tickEffect: (player, enemy) =>
            {
                // Example: could be empty if applied immediately
            },
            onApplyEffect: (player, enemy) =>
            {
                enemy.ModifyStats(defense: -2); // reduce defense
            },
            onExpireEffect: (player, enemy) =>
            {
                enemy.ModifyStats(defense: 2);  // restore defense
            }
        );

        // Reduces target's Attack
        public static StatusEffect Weakness = new StatusEffect(
            name: "Weakness",
            description: "Reduces target's Attack",
            duration: 3,
            power: 2,
            isBuff: false,
            tickEffect: null,
            onApplyEffect: (player, enemy) =>
            {
                enemy.ModifyStats(attack: -2);
            },
            onExpireEffect: (player, enemy) =>
            {
                enemy.ModifyStats(attack: 2);
            }
        );

        // Increases speed or extra action
public static StatusEffect Haste = new StatusEffect(
    name: "Haste",
    description: "Grants 1 extra action per turn",
    duration: 2,
    power: 1, // extra actions
    isBuff: true,
    tickEffect: null,
    onApplyEffect: (player, enemy) =>
    {
        player.AddExtraAction(1);
        Util.TypeWrite($"{player.Name} is hasted! Gains 1 extra action this turn.");
    },
    onExpireEffect: (player, enemy) =>
    {
        player.UseAction(); // remove 1 extra action if still present
        Util.TypeWrite($"{player.Name}'s Haste fades.");
    }
);
        // Boosts next skill's power, but could cause recoil
        public static StatusEffect Overcharge = new StatusEffect(
            name: "Overcharge",
            description: "Boost next skill power, but take recoil",
            duration: 1,
            power: 5, // example boost
            isBuff: true,
            tickEffect: null,
            onApplyEffect: (player, enemy) =>
            {
                player.ModifyStats(); // modify next skill power
            },
            onExpireEffect: (player, enemy) =>
            {
                // handle recoil or removal
            }
        );

        // Each time you take damage, gain Attack +1 (stacks)
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
        // Apply Burn effect
        StatusEffect burn = new StatusEffect(
            name: "Burn",
            description: "Lose HP each turn from burning",
            duration: 3,
            power: 2, // damage per turn
            isBuff: false,
            tickEffect: (p, e) =>
            {
                e.TakeDamage(2);
                Util.TypeWrite($"{e.Name} suffers 2 burn damage!");
            },
            onApplyEffect: (p, e) =>
            {
                Util.TypeWrite($"{e.Name} is set on fire!");
            },
            onExpireEffect: (p, e) =>
            {
                Util.TypeWrite($"{e.Name} stops burning.");
            }
        );

}

    }

