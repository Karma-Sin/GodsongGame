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
    
        public static StatusEffect PowerUp = new StatusEffect(
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

    // ========================
    // Add your custom statuses below
    // ========================
}

    }

