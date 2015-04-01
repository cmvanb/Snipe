using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class NameStack
    {
        private static List<string> names = new List<string>()
        {
            "Clive",
            "Owen",
            "Robert",
            "Mike",
            "Tricky",
            "Linda",
            "Betty",
            "Lara",
            "Samir",
            "Farah",
            "Jenny",
            "Birdy"
        };

        public static string GetName()
        {
            if (names.Count == 0)
            {
                throw new System.Exception("Ran out of names.");
            }

            int index = Random.Range(0, names.Count);

            string name = names[index];

            names.RemoveAt(index);

            return name;
        }
    }
}