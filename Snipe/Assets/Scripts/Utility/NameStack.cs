using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class NameStack
    {
        private static readonly List<string> allNames = new List<string>()
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
            "Birdy",
            "Steve",
            "Charlie",
            "Olga",
            "Mary"
        };

        private static List<string> namesLeft = new List<string>();

        public static string GetName()
        {
            if (namesLeft.Count == 0)
            {
                Reset();
            }

            int index = Random.Range(0, namesLeft.Count);

            string name = namesLeft[index];

            namesLeft.RemoveAt(index);

            //Debug.Log(names.Count + " names left");

            return name;
        }

        public static void Reset()
        {
            namesLeft.Clear();
            namesLeft.AddRange(allNames);
        }
    }
}