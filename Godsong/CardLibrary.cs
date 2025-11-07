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
            AddRageMageCard();
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

    private static void AddRageMageCard()
{
    AllCards.Add(new CardTemplate(
        "Rage Mage",
        "A spellcaster fueled by fury, striking harder as their rage builds.",
        1,  // Attack modifier
        0,  // HP modifier
        0,  // Defense modifier
        new List<Skill>
        {
            SkillLIbrary.FuryBolt,
            SkillLIbrary.FurySurge,
            SkillLIbrary.SeethingStrike,
            SkillLIbrary.ArcaneEmber
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