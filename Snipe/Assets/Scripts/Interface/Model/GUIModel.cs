using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class GUIModel
    {
        public bool SelectorActive { get; set; }
        public Vector2 SelectorPosition { get; set; }
        public Vector2 SelectedPosition { get; set; }
        public Unit SelectedUnit { get; set; }
        public bool ShowGameOver { get; set; }
        public List<Vector2> MovePositions { get { return movePositions; } }
        public List<Vector2> AttackPositions { get { return attackPositions; } }
        public List<Vector2> HealPositions { get { return healPositions; } }
        public List<Portrait> Player1Portraits { get { return player1Portraits; } }
        public List<Portrait> Player2Portraits { get { return player2Portraits; } }

        private List<Vector2> movePositions;
        private List<Vector2> attackPositions;
        private List<Vector2> healPositions;
        private List<Portrait> player1Portraits;
        private List<Portrait> player2Portraits;

        public GUIModel()
        {
            movePositions = new List<Vector2>();
            attackPositions = new List<Vector2>();
            healPositions = new List<Vector2>();
            player1Portraits = new List<Portrait>();
            player2Portraits = new List<Portrait>();
        }

        public void ClearSelection()
        {
            SelectorActive = false;
            SelectedUnit = null;
            movePositions.Clear();
            attackPositions.Clear();
            healPositions.Clear();
        }
    }
}