using System;

namespace FootballPlayers.Models
{
    public enum Sex
    {
        Female,
        Male
    }

    public static class SexExtensions
    {
        public static string GetName(this Sex sex)
        {
            switch (sex)
            {
                case Sex.Female:
                    return "Женский";
                case Sex.Male:
                    return "Мужской";
                default:
                    throw new ArgumentException("Нет такого пола");
            }
        }
    }
}
