using System;

namespace Godsong
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player hero = new Player("Arin");
            Enemy goblin = new Enemy("Goblin Scout", 25, 4, 1, 6, 10, 5);

            BattleSystem battle = new BattleSystem(hero, goblin);
            battle.StartBattle();
        }
    }
}
