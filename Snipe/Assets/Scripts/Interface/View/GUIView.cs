using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class GUIView
    {
        private GameView gameView;
        private InterfaceView interfaceView;
        private bool guiInitialized = false;
        private GameObject selector;
        private GameObject selected;
        private GameObject[] moveObjects;
        private GameObject[] attackObjects;
        private GameObject[] healObjects;

        public GUIView(GameView gameView, InterfaceView interfaceView)
        {
            this.gameView = gameView;
            this.interfaceView = interfaceView;
        }

        public void Update(GUIState guiState)
        {
            if (!guiInitialized)
            {
                InitializeGUI(guiState);

                guiInitialized = true;
            }

            UpdateInterface(guiState);
            UpdateSelector(guiState);
            UpdateSelected(guiState);
            UpdateMoveObjects(guiState);
            UpdateAttackObjects(guiState);
            UpdateHealObjects(guiState);
        }

        private void InitializeGUI(GUIState guiState)
        {
            SpriteRenderer spriteRenderer;
            Sprite sprite;
            SpriteManager spriteManager = SpriteManager.Instance;

            // Selector.
            selector = new GameObject("Selector");

            spriteRenderer = selector.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 20;
            sprite = spriteManager.GetSprite(SpriteID.Selector);
            spriteRenderer.sprite = sprite;

            // Selected.
            selected = new GameObject("Selected");

            spriteRenderer = selected.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 9; // Behind units.
            sprite = spriteManager.GetSprite(SpriteID.Selected);
            spriteRenderer.sprite = sprite;

            // Move objects.
            moveObjects = new GameObject[8];

            for (int i = 0; i < moveObjects.Length; ++i)
            {
                moveObjects[i] = new GameObject("Move " + i);

                spriteRenderer = moveObjects[i].AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 20;
                sprite = spriteManager.GetSprite(SpriteID.Move);
                spriteRenderer.sprite = sprite;
            }

            // Attack objects.
            attackObjects = new GameObject[8];

            for (int i = 0; i < attackObjects.Length; ++i)
            {
                attackObjects[i] = new GameObject("Attack " + i);

                spriteRenderer = attackObjects[i].AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 20;
                sprite = spriteManager.GetSprite(SpriteID.Attack);
                spriteRenderer.sprite = sprite;
            }

            // Heal objects.
            healObjects = new GameObject[8];

            for (int i = 0; i < healObjects.Length; ++i)
            {
                healObjects[i] = new GameObject("Heal " + i);

                spriteRenderer = healObjects[i].AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 20;
                sprite = spriteManager.GetSprite(SpriteID.Heal);
                spriteRenderer.sprite = sprite;
            }
        }

        private void UpdateInterface(GUIState guiState)
        {
            for (int i = 0; i < interfaceView.Player1PortraitViews.Count; ++i)
            {
                if (i < guiState.Player1Portraits.Count)
                {
                    Portrait portrait = guiState.Player1Portraits[i];

                    interfaceView.Player1PortraitViews[i].SetName(portrait.Name);
                    interfaceView.Player1PortraitViews[i].SetClass(portrait.Class);
                    interfaceView.Player1PortraitViews[i].SetSprite(portrait.Sprite);
                }
                else
                {
                    interfaceView.Player1PortraitViews[i].gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < interfaceView.Player2PortraitViews.Count; ++i)
            {
                if (i < guiState.Player2Portraits.Count)
                {
                    Portrait portrait = guiState.Player2Portraits[i];

                    interfaceView.Player2PortraitViews[i].SetName(portrait.Name);
                    interfaceView.Player2PortraitViews[i].SetClass(portrait.Class);
                    interfaceView.Player2PortraitViews[i].SetSprite(portrait.Sprite);
                }
                else
                {
                    interfaceView.Player2PortraitViews[i].gameObject.SetActive(false);
                }
            }
        }

        private void UpdateSelector(GUIState guiState)
        {
            if (guiState.SelectorActive)
            {
                if (!selector.activeSelf)
                {
                    selector.SetActive(true);
                }

                selector.transform.localPosition = new Vector3(
                    guiState.SelectorPosition.x, guiState.SelectorPosition.y, 0);
            }
            else
            {
                if (selector.activeSelf)
                {
                    selector.SetActive(false);
                }
            }

        }

        private void UpdateSelected(GUIState guiState)
        {
            if (guiState.SelectedUnit != null)
            {
                if (!selected.activeSelf)
                {
                    selected.SetActive(true);
                }

                selected.transform.localPosition = new Vector3(
                    guiState.SelectedPosition.x, guiState.SelectedPosition.y, 0);
            }
            else
            {
                if (selected.activeSelf)
                {
                    selected.SetActive(false);
                }
            }
        }

        private void UpdateMoveObjects(GUIState guiState)
        {
            if (guiState.SelectedUnit != null)
            {
                if (guiState.MovePositions.Count > moveObjects.Length)
                {
                    throw new System.Exception("Not enough move objects.");
                }

                for (int i = 0; i < moveObjects.Length; ++i)
                {
                    if (i < guiState.MovePositions.Count)
                    {
                        if (!moveObjects[i].activeSelf)
                        {
                            moveObjects[i].SetActive(true);
                        }

                        moveObjects[i].transform.localPosition = new Vector3(
                            guiState.MovePositions[i].x, guiState.MovePositions[i].y, 0);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < moveObjects.Length; ++i)
                {
                    if (moveObjects[i].activeSelf)
                    {
                        moveObjects[i].SetActive(false);
                    }
                }
            }
        }

        private void UpdateAttackObjects(GUIState guiState)
        {
            if (guiState.SelectedUnit != null)
            {
                if (guiState.AttackPositions.Count > attackObjects.Length)
                {
                    throw new System.Exception("Not enough attack objects.");
                }

                for (int i = 0; i < attackObjects.Length; ++i)
                {
                    if (i < guiState.AttackPositions.Count)
                    {
                        if (!attackObjects[i].activeSelf)
                        {
                            attackObjects[i].SetActive(true);
                        }

                        attackObjects[i].transform.localPosition = new Vector3(
                            guiState.AttackPositions[i].x, guiState.AttackPositions[i].y, 0);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < attackObjects.Length; ++i)
                {
                    if (attackObjects[i].activeSelf)
                    {
                        attackObjects[i].SetActive(false);
                    }
                }
            }
        }

        private void UpdateHealObjects(GUIState guiState)
        {
            if (guiState.SelectedUnit != null)
            {
                if (guiState.HealPositions.Count > healObjects.Length)
                {
                    throw new System.Exception("Not enough heal objects.");
                }

                for (int i = 0; i < healObjects.Length; ++i)
                {
                    if (i < guiState.HealPositions.Count)
                    {
                        if (!healObjects[i].activeSelf)
                        {
                            healObjects[i].SetActive(true);
                        }

                        healObjects[i].transform.localPosition = new Vector3(
                            guiState.HealPositions[i].x, guiState.HealPositions[i].y, 0);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < healObjects.Length; ++i)
                {
                    if (healObjects[i].activeSelf)
                    {
                        healObjects[i].SetActive(false);
                    }
                }
            }
        }
    }
}