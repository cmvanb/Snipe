using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class GUIState
    {
        public bool SelectorActive { get { return selectorActive; } }
        public Vector2 CursorPosition { get { return cursorPosition; } set { cursorPosition = value; } }

        private bool selectorActive = true;
        private Vector2 cursorPosition = Vector2.zero;

        public GUIState()
        {
        }
    }
}