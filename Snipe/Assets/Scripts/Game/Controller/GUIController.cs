using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class GUIController
    {
        private GameState gameState;
        private GUIState guiState;

        public GUIController(GameState gameState, GUIState guiState)
        {
            this.gameState = gameState;
            this.guiState = guiState;
        }

        public void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                guiState.SelectorPosition = raycastHit.transform.position;

                Vector2 gridPosition = GridMath.GridPositionFromScreenPosition(raycastHit.transform.position);

                Grid grid = gameState.Grid;

                Cell cell = grid.GetCellAt((int)gridPosition.x, (int)gridPosition.y);

                Unit unit = cell.GetUnit();

                if (unit != null
                    && gameState.CurrentPlayer.Faction == unit.Faction)
                {
                    guiState.SelectorActive = true;

                    if (guiState.SelectedUnit != unit
                        && Input.GetMouseButton(0))
                    {
                        guiState.SelectedPosition = raycastHit.transform.position;
                        guiState.SelectedUnit = unit;

                        DisplayLegalMoves(guiState.SelectedUnit);
                    }
                }
                else
                {
                    guiState.SelectorActive = false;
                }
            }
            else
            {
                guiState.SelectorActive = false;
            }

            if (Input.GetMouseButton(1))
            {
                guiState.SelectedUnit = null;
            }
        }

        private void DisplayLegalMoves(Unit unit)
        {
            guiState.MovePositions.Clear();

            List<Cell> legalMoves = unit.GetLegalMoves();

            foreach (Cell cell in legalMoves)
            {
                Vector2 screenPosition = GridMath.ScreenPositionFromGridPosition(cell.Position);

                guiState.MovePositions.Add(screenPosition);
            }
        }
    }
}