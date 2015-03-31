using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class GUIView
    {
        private GameView gameView;
        private bool guiInitialized = false;
        private GameObject selector;
        private GameObject selected;
        private GameObject[] moveObjects;

        public GUIView(GameView gameView)
        {
            this.gameView = gameView;
        }

        public void Update(GUIState guiState)
        {
            if (!guiInitialized)
            {
                InitializeGUI(guiState);

                guiInitialized = true;
            }

            UpdateSelector(guiState);
            UpdateSelected(guiState);
            UpdateMoveObjects(guiState);
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
    }
}