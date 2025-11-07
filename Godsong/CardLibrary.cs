using System;
using System.Collections.Generic;

namespace Godsong
{
    public static class CardLibrary
    {
        public static List<Card> AllCards { get; } = new List<Card>();

        static CardLibrary()
        {
            AddHumanCard();
            AddGoblinCard();
        }

        private static void AddHumanCard()
        {
            AllCards.Add(new CardTemplate(
                "Human",
                "Balanced warrior of endurance",
                0, 0, 0,
                new List<Skill>
                {
                    SkillLIbrary.HumanStrike,
                    SkillLIbrary.HumanLunge,
                    SkillLIbrary.HumanLaststand,
                    SkillLIbrary.HumanCounter
                }
            ).CreateCard());
        }

        private static void AddGoblinCard()
        {
            AllCards.Add(new CardTemplate(
                "Goblin",
                "Devious trickster wielding scraps and spite",
                2, 1, 1,
                new List<Skill>
                {
                    SkillLIbrary.GoblinSuckerPunch,
                    SkillLIbrary.GoblinLanceBarrage,
                    SkillLIbrary.GoblinTinkererArmor,
                    SkillLIbrary.GoblinOilFlare
                }
            ).CreateCard());
        }

        public static Card? GetCard(string name)
        {
            return AllCards.Find(c => c.Name == name);
        }
    }
}