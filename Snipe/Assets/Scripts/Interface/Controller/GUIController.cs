using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class GUIController
    {
        private GameModel gameModel;
        private GUIModel guiModel;
        private bool triggeredGameOver = false;

        public GUIController(GameModel gameModel, GUIModel guiModel)
        {
            this.gameModel = gameModel;
            this.guiModel = guiModel;

            UpdatePortraits();
        }

        public void Update()
        {
            UpdatePortraits();

            if (triggeredGameOver)
            {
                // In case we reset the game.
                if (!gameModel.GameOver)
                {
                    guiModel.ShowGameOver = false;
                    triggeredGameOver = false;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (gameModel.GameOver)
                {
                    guiModel.ClearSelection();
                    guiModel.ShowGameOver = true;
                    triggeredGameOver = true;
                    return;
                }
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                guiModel.SelectorPosition = raycastHit.transform.position;

                Vector2 gridPosition = GridMath.GridPositionFromScreenPosition(raycastHit.transform.position);

                Grid grid = gameModel.Grid;

                Cell cell = grid.GetCellAt((int)gridPosition.x, (int)gridPosition.y);

                Unit unit = cell.GetUnit();

                if (unit != null
                    && gameModel.CurrentPlayer.Faction == unit.Faction
                    && unit.IsAlive
                    && !unit.IsWounded)
                {
                    guiModel.SelectorActive = true;

                    if (guiModel.SelectedUnit != unit
                        && Input.GetMouseButtonUp(0)
                        && gameModel.CurrentPlayer.HasActionPointsLeft())
                    {
                        guiModel.SelectedPosition = raycastHit.transform.position;
                        guiModel.SelectedUnit = unit;

                        DisplayLegalMoves(guiModel.SelectedUnit);
                        DisplayLegalAttacks(guiModel.SelectedUnit);
                        DisplayLegalHeals(guiModel.SelectedUnit);
                    }
                }
                else
                {
                    guiModel.SelectorActive = false;

                    if (guiModel.SelectedUnit != null
                        && Input.GetMouseButtonUp(0)
                        && gameModel.CurrentPlayer.HasActionPointsLeft())
                    {
                        List<Cell> legalMoves = guiModel.SelectedUnit.GetLegalMoves();

                        foreach (Cell legalMove in legalMoves)
                        {
                            if (legalMove.Position == gridPosition)
                            {
                                gameModel.CurrentPlayer.UseActionPoint();
                                guiModel.SelectedUnit.Move(legalMove);
                                guiModel.SelectedUnit = null;

                                return;
                            }
                        }

                        List<Cell> legalAttacks = guiModel.SelectedUnit.GetLegalAttacks();

                        foreach (Cell legalAttack in legalAttacks)
                        {
                            if (legalAttack.Position == gridPosition)
                            {
                                gameModel.CurrentPlayer.UseActionPoint();
                                guiModel.SelectedUnit.Attack(legalAttack);
                                guiModel.SelectedUnit = null;

                                return;
                            }
                        }

                        List<Cell> legalHeals = guiModel.SelectedUnit.GetLegalHeals();

                        foreach (Cell legalHeal in legalHeals)
                        {
                            if (legalHeal.Position == gridPosition)
                            {
                                gameModel.CurrentPlayer.UseActionPoint();
                                guiModel.SelectedUnit.Heal(legalHeal);
                                guiModel.SelectedUnit = null;

                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                guiModel.ClearSelection();
            }

            if (Input.GetMouseButtonUp(1))
            {
                guiModel.ClearSelection();
            }
        }

        private void UpdatePortraits()
        {
            guiModel.Player1Portraits.Clear();

            List<Unit> units1 = gameModel.Grid.GetUnits(gameModel.Players[0].Faction);

            for (int i = 0; i < units1.Count; ++i)
            {
                Unit unit = units1[i];

                Sprite sprite = SpriteManager.Instance.GetSprite(SpriteID.Portrait1Normal);

                string _class = unit.IsRevealed ? unit.UnitType.ToString() : UnitType.Unknown.ToString();

                Portrait portrait = new Portrait(unit.GameName, _class, sprite);

                guiModel.Player1Portraits.Add(portrait);
            }

            guiModel.Player2Portraits.Clear();

            List<Unit> units2 = gameModel.Grid.GetUnits(gameModel.Players[1].Faction);

            for (int i = 0; i < units2.Count; ++i)
            {
                Unit unit = units2[i];

                Sprite sprite = SpriteManager.Instance.GetSprite(SpriteID.Portrait1Normal);

                string _class = unit.IsRevealed ? unit.UnitType.ToString() : UnitType.Unknown.ToString();

                Portrait portrait = new Portrait(unit.GameName, _class, sprite);

                guiModel.Player2Portraits.Add(portrait);
            }
        }

        private void DisplayLegalMoves(Unit unit)
        {
            guiModel.MovePositions.Clear();

            List<Cell> legalMoves = unit.GetLegalMoves();

            foreach (Cell legalMove in legalMoves)
            {
                Vector2 screenPosition = GridMath.ScreenPositionFromGridPosition(legalMove.Position);

                guiModel.MovePositions.Add(screenPosition);
            }
        }

        private void DisplayLegalAttacks(Unit unit)
        {
            guiModel.AttackPositions.Clear();

            List<Cell> legalAttacks = unit.GetLegalAttacks();

            foreach (Cell legalAttack in legalAttacks)
            {
                Vector2 screenPosition = GridMath.ScreenPositionFromGridPosition(legalAttack.Position);

                guiModel.AttackPositions.Add(screenPosition);
            }
        }

        private void DisplayLegalHeals(Unit unit)
        {
            guiModel.HealPositions.Clear();

            List<Cell> legalHeals = unit.GetLegalHeals();

            foreach (Cell legalHeal in legalHeals)
            {
                Vector2 screenPosition = GridMath.ScreenPositionFromGridPosition(legalHeal.Position);

                guiModel.HealPositions.Add(screenPosition);
            }
        }
    }
}