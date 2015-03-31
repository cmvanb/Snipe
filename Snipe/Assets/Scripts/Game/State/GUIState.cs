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
        public List<Vector2> AttackPositions { get { return attackPositions; } }
        public List<Vector2> HealPositions { get { return healPositions; } }

        private List<Vector2> movePositions;
        private List<Vector2> attackPositions;
        private List<Vector2> healPositions;

        public GUIState()
        {
            movePositions = new List<Vector2>();
            attackPositions = new List<Vector2>();
            healPositions = new List<Vector2>();
        }
    }
}