using System;
using System.Threading;

namespace Godsong
{
    public static class Util 
    {
        // Writes text slowly WITHOUT a newline at the end
        public static void TypeWriteInline(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            // Do NOT add Console.WriteLine() here
        }

        // Allows writing numbers the same way
        public static void TypeWriteInline(int number, int delay = 30)
        {
            TypeWriteInline(number.ToString(), delay);
        }

        // Writes text slowly WITH a newline at the end
        public static void TypeWrite(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }



        public static void TypeWriteMultiColor(     //Color helper
    (string text, ConsoleColor color)[] segments,
    int delay = 30,
    bool newLine = true)
        {
            foreach (var segment in segments)
            {
                Console.ForegroundColor = segment.color;
                foreach (char c in segment.text)
                {
                    Console.Write(c);
                    System.Threading.Thread.Sleep(delay);
                }
                Console.ResetColor();
            }
            if (newLine) Console.WriteLine();
        }
 // Apply a status effect to a target, cloning it automatically
    public static void ApplyStatusEffect(StatusEffect template, Player player, Enemy enemy)
    {
        // Create a new instance based on the template
        StatusEffect effectInstance = new StatusEffect(
            name: template.Name,
            description: template.Description,
            duration: template.Duration,
            power: template.Power,
            isBuff: template.IsBuff,
            tickEffect: template.TickEffect,
            onApplyEffect: template.OnApplyEffect,
            onExpireEffect: template.OnExpireEffect
        );

        // Apply to enemy
        enemy.AddEffect(effectInstance, player);
    }

        // Overload for applying to player
        public static void ApplyStatusEffect(StatusEffect template, Enemy enemy, Player player)
        {
            StatusEffect effectInstance = new StatusEffect(
                name: template.Name,
                description: template.Description,
                duration: template.Duration,
                power: template.Power,
                isBuff: template.IsBuff,
                tickEffect: template.TickEffect,
                onApplyEffect: template.OnApplyEffect,
                onExpireEffect: template.OnExpireEffect
            );

            player.AddEffect(effectInstance, enemy);
        }
            // Clone effect (so we can reuse library effects safely)


    }
}