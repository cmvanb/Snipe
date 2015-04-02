using UnityEngine;

namespace Snipe
{
    public class Portrait
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public Sprite Sprite { get; set; }

        public Portrait(string name, string _class, Sprite sprite)
        {
            Name = name;
            Class = _class;
            Sprite = sprite;
        }
    }
}