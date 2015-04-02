using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class GUIView
    {
        private InterfaceView interfaceView;
        private bool guiInitialized = false;
        private GameObject selector;
        //private GameObject selected;
        private GameObject[] moveObjects;
        private GameObject[] attackObjects;
        private GameObject[] healObjects;

        public GUIView(InterfaceView interfaceView)
        {
            this.interfaceView = interfaceView;
        }

        public void Update(GUIModel guiModel)
        {
            if (!guiInitialized)
            {
                InitializeGUI(guiModel);

                guiInitialized = true;
            }

            UpdateInterface(guiModel);
            UpdateSelector(guiModel);
            //UpdateSelected(guiModel);
            UpdateMoveObjects(guiModel);
            UpdateAttackObjects(guiModel);
            UpdateHealObjects(guiModel);

            if (guiModel.ShowGameOver)
            {
                interfaceView.GameOverView.SetActive(true);
            }
            else
            {
                interfaceView.GameOverView.SetActive(false);
            }
        }

        private void InitializeGUI(GUIModel guiModel)
        {
            SpriteRenderer spriteRenderer;
            Sprite sprite;
            SpriteManager spriteManager = SpriteManager.Instance;

            // Selector.
            selector = new GameObject("Selector");

            selector.transform.localScale = new Vector3(2f, 2f, 1f);

            spriteRenderer = selector.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 20;
            sprite = spriteManager.GetSprite(SpriteID.Selector);
            spriteRenderer.sprite = sprite;

            /*
            // Selected.
            selected = new GameObject("Selected");

            spriteRenderer = selected.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 9; // Behind units.
            sprite = spriteManager.GetSprite(SpriteID.Selected);
            spriteRenderer.sprite = sprite;
            */

            // Move objects.
            moveObjects = new GameObject[8];

            for (int i = 0; i < moveObjects.Length; ++i)
            {
                moveObjects[i] = new GameObject("Move " + i);

                moveObjects[i].transform.localScale = new Vector3(2f, 2f, 1f);

                spriteRenderer = moveObjects[i].AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 8;
                sprite = spriteManager.GetSprite(SpriteID.Move);
                spriteRenderer.sprite = sprite;
            }

            // Attack objects.
            attackObjects = new GameObject[8];

            for (int i = 0; i < attackObjects.Length; ++i)
            {
                attackObjects[i] = new GameObject("Attack " + i);

                attackObjects[i].transform.localScale = new Vector3(2f, 2f, 1f);

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

                healObjects[i].transform.localScale = new Vector3(2f, 2f, 1f);

                spriteRenderer = healObjects[i].AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 20;
                sprite = spriteManager.GetSprite(SpriteID.Heal);
                spriteRenderer.sprite = sprite;
            }

            // Interface.
            interfaceView.GameOverView.SetActive(false);
        }

        private void UpdateInterface(GUIModel guiModel)
        {
            for (int i = 0; i < interfaceView.Player1PortraitViews.Count; ++i)
            {
                if (i < guiModel.Player1Portraits.Count)
                {
                    Portrait portrait = guiModel.Player1Portraits[i];

                    if (!interfaceView.Player1PortraitViews[i].gameObject.activeSelf)
                    {
                        interfaceView.Player1PortraitViews[i].gameObject.SetActive(true);
                    }

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
                if (i < guiModel.Player2Portraits.Count)
                {
                    Portrait portrait = guiModel.Player2Portraits[i];

                    if (!interfaceView.Player2PortraitViews[i].gameObject.activeSelf)
                    {
                        interfaceView.Player2PortraitViews[i].gameObject.SetActive(true);
                    }

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

        private void UpdateSelector(GUIModel guiModel)
        {
            if (guiModel.SelectorActive)
            {
                if (!selector.activeSelf)
                {
                    selector.SetActive(true);
                }

                selector.transform.localPosition = new Vector3(
                    guiModel.SelectorPosition.x - (Constants.UnitsPerPixel * 12),
                    guiModel.SelectorPosition.y + (Constants.UnitsPerPixel * 28), 
                    0);
            }
            else
            {
                if (selector.activeSelf)
                {
                    selector.SetActive(false);
                }
            }

        }

        /*
        private void UpdateSelected(GUIModel guiModel)
        {
            if (guiModel.SelectedUnit != null)
            {
                if (!selected.activeSelf)
                {
                    selected.SetActive(true);
                }

                selected.transform.localPosition = new Vector3(
                    guiModel.SelectedPosition.x, guiModel.SelectedPosition.y, 0);
            }
            else
            {
                if (selected.activeSelf)
                {
                    selected.SetActive(false);
                }
            }
        }
        */

        private void UpdateMoveObjects(GUIModel guiModel)
        {
            if (guiModel.SelectedUnit != null)
            {
                if (guiModel.MovePositions.Count > moveObjects.Length)
                {
                    throw new System.Exception("Not enough move objects.");
                }

                for (int i = 0; i < moveObjects.Length; ++i)
                {
                    if (i < guiModel.MovePositions.Count)
                    {
                        if (!moveObjects[i].activeSelf)
                        {
                            moveObjects[i].SetActive(true);
                        }

                        moveObjects[i].transform.localPosition = new Vector3(
                            guiModel.MovePositions[i].x, guiModel.MovePositions[i].y, 0);
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

        private void UpdateAttackObjects(GUIModel guiModel)
        {
            if (guiModel.SelectedUnit != null)
            {
                if (guiModel.AttackPositions.Count > attackObjects.Length)
                {
                    throw new System.Exception("Not enough attack objects.");
                }

                for (int i = 0; i < attackObjects.Length; ++i)
                {
                    if (i < guiModel.AttackPositions.Count)
                    {
                        if (!attackObjects[i].activeSelf)
                        {
                            attackObjects[i].SetActive(true);
                        }

                        attackObjects[i].transform.localPosition = new Vector3(
                            guiModel.AttackPositions[i].x - (Constants.UnitsPerPixel * 12),
                            guiModel.AttackPositions[i].y + (Constants.UnitsPerPixel * 28), 
                            0);
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

        private void UpdateHealObjects(GUIModel guiModel)
        {
            if (guiModel.SelectedUnit != null)
            {
                if (guiModel.HealPositions.Count > healObjects.Length)
                {
                    throw new System.Exception("Not enough heal objects.");
                }

                for (int i = 0; i < healObjects.Length; ++i)
                {
                    if (i < guiModel.HealPositions.Count)
                    {
                        if (!healObjects[i].activeSelf)
                        {
                            healObjects[i].SetActive(true);
                        }

                        healObjects[i].transform.localPosition = new Vector3(
                            guiModel.HealPositions[i].x, guiModel.HealPositions[i].y, 0);
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