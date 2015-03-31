using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class GUIState
    {
        public bool SelectorActive { get; set; }
        public Vector2 SelectorPosition { get; set; }
        public Vector2 SelectedPosition { get; set; }
        public Unit SelectedUnit { get; set; }
        public List<Vector2> MovePositions { get { return movePositions; } }

        private List<Vector2> movePositions;

        public GUIState()
        {
            movePositions = new List<Vector2>();
        }
    }
}