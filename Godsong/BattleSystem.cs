using System;

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
            Util.TypeWrite($"\n{player.Name}'s turn!");
            player.ProcessEffects(enemy);

            int actionsUsed = 0;
            bool hasSwitched = false;

            while (actionsUsed < 2)
            {
                Util.TypeWrite("\nChoose an action:");
                Util.TypeWrite("1. Use Skill");
                if (!hasSwitched && actionsUsed == 1)
                    Util.TypeWrite("2. Switch Card");
                Util.TypeWrite(">");

                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                Console.WriteLine();

                if (key.Key == ConsoleKey.D1)
                {
                    if (UseSkill()) actionsUsed++;
                }
                else if (key.Key == ConsoleKey.D2 && !hasSwitched && actionsUsed == 1)
                {
                    if (SwitchCard())
                    {
                        hasSwitched = true;
                    }
                }
                else
                {
                    Util.TypeWrite("Invalid choice!");
                }

                // Force skill after switch
                if (hasSwitched && actionsUsed == 1)
                {
                    Util.TypeWrite("\nChoose a skill after switching:");
                    if (UseSkill()) actionsUsed++;
                }
            }

            Util.TypeWrite("Turn ends.");
        }

        private bool UseSkill()
        {
            var skills = player.CurrentCard.Skills;

            if (skills.Count == 0)
            {
                Util.TypeWrite("No skills available!");
                return false;
            }

            // Display skill list
            Util.TypeWrite($"{player.Name}'s Skills:");
            for (int i = 0; i < skills.Count; i++)
            {
                Skill skill = skills[i];
                Util.TypeWriteMultiColor(new (string, ConsoleColor)[]
                {
                    ($"{i + 1}. ", ConsoleColor.White),
                    ($"{skill.Name}", ConsoleColor.Yellow),
                    (" - ", ConsoleColor.White),
                    ($"{skill.Description}", ConsoleColor.Gray)
                });
            }

            Util.TypeWrite("> Choose a skill by number: ");
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            int choice = key.KeyChar - '1';
            Console.WriteLine();

            if (choice >= 0 && choice < skills.Count)
            {
                Skill selectedSkill = skills[choice];
                Util.TypeWriteMultiColor(new (string, ConsoleColor)[]
                {
                    ($"{player.Name}", ConsoleColor.Green),
                    (" uses ", ConsoleColor.White),
                    ($"{selectedSkill.Name}!", ConsoleColor.Yellow)
                });

                selectedSkill.Use(player, enemy);
                return true;
            }
            else
            {
                Util.TypeWrite("Invalid choice! Turn skipped.");
                return false;
            }
        }

        private bool SwitchCard()
        {
            // Display available cards
            Util.TypeWrite("Available Cards:");
            var allCards = CardLibrary.AllCards;
            for (int i = 0; i < allCards.Count; i++)
            {
                Card card = allCards[i];
                Util.TypeWriteMultiColor(new (string, ConsoleColor)[]
                {
                    ($"{i + 1}. ", ConsoleColor.White),
                    ($"{card.Name}", ConsoleColor.Yellow),
                    (" - ", ConsoleColor.White),
                    ($"{card.Description}", ConsoleColor.Gray)
                });
            }

            Util.TypeWrite("> Choose a card by number: ");
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            int choice = key.KeyChar - '1';
            Console.WriteLine();

            if (choice >= 0 && choice < allCards.Count)
            {
                player.EquipCard(allCards[choice]);
                Util.TypeWrite($"{player.Name} switches to {allCards[choice].Name}!");
                return true;
            }
            else
            {
                Util.TypeWrite("Invalid choice! Cannot switch.");
                return false;
            }
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