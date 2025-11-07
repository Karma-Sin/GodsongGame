using System;
using System.Collections.Generic;

namespace Godsong
{
    public static class CardLibrary
    {
        public static List<Card> AllCards { get; } = new List<Card>();

        static CardLibrary()
        {
            AddHumanCards();
            AddGoblinCards();
            //TODO add more card types
        }

        private static void AddHumanCards()
        {
            var humanSkills = new List<Skill>
            {
                SkillLIbrary.HumanStrike,
                SkillLIbrary.HumanLunge,
                SkillLIbrary.HumanLaststand,
                SkillLIbrary.HumanCounter

            };

            AllCards.Add(new Card("Human", "Balanced warrior of endurance", 0, 0, 0, humanSkills));
        }

        private static void AddGoblinCards()
        {
            var goblinSkills = new List<Skill>
            {
                SkillLibrary.GoblinSuckerPunch,
SkillLibary,GoblinSpearBarrage,
SkillLibrary.GoblinTinkererArmor,
SkillLibrary.GoblinOilFlare
            };

            AllCards.Add(new Card("Goblin", "Devious trickster wielding scraps and spite", 2, 1, 1));
          }
  

        public static Card? GetCard(string name)
        {
            return AllCards.Find(c => c.Name == name);
        }
    }
}