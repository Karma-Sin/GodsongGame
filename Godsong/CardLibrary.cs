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
            AddTemplateCard1();
            AddTemplateCard2();
            AddTemplateCard3();
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

        // ------------------------------
        // Blank templates ready to fill
        // ------------------------------
        private static void AddTemplateCard1()
        {
            AllCards.Add(new CardTemplate(
                "Template1",
                "Description for Template1",
                0, 0, 0,
                new List<Skill>
                {
                    SkillLIbrary.HumanStrike,   // Replace with desired skill
                    SkillLIbrary.HumanLunge,    // Replace with desired skill
                    SkillLIbrary.HumanLaststand,// Replace with desired skill
                    SkillLIbrary.HumanCounter   // Replace with desired skill
                }
            ).CreateCard());
        }

        private static void AddTemplateCard2()
        {
            AllCards.Add(new CardTemplate(
                "Template2",
                "Description for Template2",
                0, 0, 0,
                new List<Skill>
                {
                    SkillLIbrary.GoblinSuckerPunch,
                    SkillLIbrary.GoblinLanceBarrage,
                    SkillLIbrary.GoblinTinkererArmor,
                    SkillLIbrary.GoblinOilFlare
                }
            ).CreateCard());
        }

        private static void AddTemplateCard3()
        {
            AllCards.Add(new CardTemplate(
                "Template3",
                "Description for Template3",
                0, 0, 0,
                new List<Skill>
                {
                    SkillLIbrary.HumanStrike, // Replace with desired skill
                    SkillLIbrary.GoblinOilFlare, // Replace with desired skill
                    SkillLIbrary.HumanLunge, // Replace with desired skill
                    SkillLIbrary.GoblinTinkererArmor // Replace with desired skill
                }
            ).CreateCard());
        }

        // -------------------------
        // Optional: Get card by name
        // -------------------------
        public static Card? GetCard(string name)
        {
            return AllCards.Find(c => c.Name == name);
        }
    }
}