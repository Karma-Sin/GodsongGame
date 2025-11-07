using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Godsong
{
    public enum BattleResult
    {
        Victory, Defeat
    }
    public class BattleSystem
    {
        private Player player;
        private Enemy enemy;
        private Random rng = new Random();

        public BattleSystem(Player player, Enemy enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

       

        public BattleResult StartBattle()
        {
            // Battle start message
            Util.TypeWrite("-----------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Util.TypeWriteInline(player.Name);
            Console.ResetColor();
            Util.TypeWriteInline(" vs ");
            Console.ForegroundColor = ConsoleColor.Red;
            Util.TypeWrite(enemy.Name);
            Console.ResetColor();
            Util.TypeWrite("-----------------------------------");


            while (player.HP > 0 && !enemy.IsDead())
            {
                PlayerTurn();
                if (enemy.IsDead()) break;

                EnemyTurn();
            }

            // Battle result
            if (player.HP > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Util.TypeWriteInline($"\n🏆 {player.Name} defeated ");
                Console.ForegroundColor = ConsoleColor.Red;
                Util.TypeWrite($"{enemy.Name}!");
                return BattleResult.Victory;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Util.TypeWrite($"\n💀 {player.Name} has fallen...");
                Console.ResetColor();
                return BattleResult.Defeat;
            }
        }
      

        

        private void PlayerTurn()
        {
            Util.TypeWrite(" ");
            Console.ForegroundColor = ConsoleColor.Green;
            Util.TypeWrite($"\n{player.Name}'s turn!");
            Console.ResetColor();

            player.ProcessEffects(enemy);

            Util.TypeWrite("Choose an action");
            Util.TypeWrite("1. Attack");
            Util.TypeWrite("2. Use Skill");
            Util.TypeWrite("3. Use Item");
            Util.TypeWrite(">");

//Enter key to trigger menu
            ConsoleKeyInfo key = Console.ReadKey(intercept: true); // 'true' hides thekey press
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    UseSkill();
                    break;
                case ConsoleKey.D2:
                    UseItem();
                    break;
                default:
                    Util.TypeWrite("Invalid choice!");
                    
                    break;

            }

        }

        private void UseSkill()
        {
            var skills = player.CurrentCard.Skills;

            if (skills.Count == 0)
            {
                Util.TypeWrite("No skills available!");
                return;
            }

            // Display skill list
            Util.TypeWrite($"{player.Name}'s Skills:");
            for (int i = 0; i < skills.Count; i++)
            {
                Skill skill = skills[i];
                // Example: "1. Last Stand - Gain 5 Defense and heal 20 HP"
                Util.TypeWriteMultiColor(new (string, ConsoleColor)[]
                {
                    ($"{i + 1}. ", ConsoleColor.White),
                    ($"{skill.Name}", ConsoleColor.Yellow),
                    (" - ", ConsoleColor.White),
                    ($"{skill.Description}", ConsoleColor.Gray)
                });
            }

            Util.TypeWrite("> Choose a skill by number: ");

            // Read key input without Enter
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            int choice = key.KeyChar - '1'; // Convert '1' → 0, '2' → 1

            Console.WriteLine(); // move to next line after input

            if (choice >= 0 && choice < skills.Count)
            {
                Skill selectedSkill = skills[choice];

                // Show skill usage
                Util.TypeWriteMultiColor(new (string, ConsoleColor)[]
                {
                    ($"{player.Name}", ConsoleColor.Green),
                    (" uses ", ConsoleColor.White),
                    ($"{selectedSkill.Name}!", ConsoleColor.Yellow)
                });

                // Execute skill effect
                selectedSkill.Use(player, enemy);
            }
            else
            {
                Util.TypeWrite("Invalid choice! You skip your turn.");
            }
        }


        private void UseItem()
        {
            
        }
        private void EnemyTurn()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Util.TypeWrite($"\n{enemy.Name}'s turn!");
            Console.ResetColor();
            
            enemy.ProcessEffects(player);

            int roll = rng.Next(1, enemy.DiceSides + 1);
            int damage = enemy.Attack + roll - player.Defense;
            if (damage < 0) damage = 0;

            // Single-line, colored output
            Console.ForegroundColor = ConsoleColor.Red;
            Util.TypeWriteInline(enemy.Name);
            Console.ResetColor();
            Util.TypeWriteInline(" rolls a ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Util.TypeWriteInline(roll);
            Console.ResetColor();
            Util.TypeWriteInline(" and attacks for ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Util.TypeWriteInline(damage);
            Console.ResetColor();
            Util.TypeWriteInline(" damage to ");
            Console.ForegroundColor = ConsoleColor.Green;
            Util.TypeWrite(player.Name + "!");
            Console.ResetColor();

            player.TakeDamage(damage);
            
        }
    }
}
