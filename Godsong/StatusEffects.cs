using System;

namespace Godsong
{
    public class StatusEffect
    {
        // Basic info
        public string Name { get; }
        public string Description { get; }
        public int Duration { get; private set; }   // Remaining turns
        public int Power { get; private set; }      // Damage per tick or buff strength
        public bool IsBuff { get; }                 // True = buff, False = debuff

        // Turn-based effect callbacks
        public Action<Player, Enemy>? TickEffect { get; }
        public Action<Player, Enemy>? OnApplyEffect { get; }
        public Action<Player, Enemy>? OnExpireEffect { get; }

        public bool IsExpired => Duration <= 0;

        // Constructor
        public StatusEffect(
            string name,
            string description,
            int duration,
            int power,
            bool isBuff,
            Action<Player, Enemy>? tickEffect = null,
            Action<Player, Enemy>? onApplyEffect = null,
            Action<Player, Enemy>? onExpireEffect = null)
        {
            Name = name;
            Description = description;
            Duration = duration;
            Power = power;
            IsBuff = isBuff;
            TickEffect = tickEffect;
            OnApplyEffect = onApplyEffect;
            OnExpireEffect = onExpireEffect;
        }

        // Apply first-time effect
        public void Apply(Player player, Enemy enemy)
        {
            OnApplyEffect?.Invoke(player, enemy);
        }

        // Called each turn
        public void Tick(Player player, Enemy enemy)
        {
            TickEffect?.Invoke(player, enemy);
            Duration--;
            if (IsExpired)
            {
                Util.TypeWrite($"{Name} has faded.");
                OnExpireEffect?.Invoke(player, enemy);
            }
        }

        // Stack effect
        public void Stack(int extraPower, int extraDuration)
        {
            Power += extraPower;
            Duration += extraDuration;
        }

        // Clone effect (so library effects remain immutable)
        public StatusEffect Clone()
        {
            return new StatusEffect(
                Name, 
                Description, 
                Duration, 
                Power, 
                IsBuff, 
                TickEffect, 
                OnApplyEffect, 
                OnExpireEffect
            );
        }
    }
}
