using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public delegate void ClickedEndTurnButtonHandler();
    public delegate void ClickedNewGameButtonHandler();
    public delegate void PressedPeekButtonHandler();
    public delegate void ReleasedPeekButtonHandler();

    public class InterfaceView : MonoBehaviour
    {
        public event ClickedEndTurnButtonHandler ClickedEndTurnButtonEvent;
        public event ClickedNewGameButtonHandler ClickedNewGameButtonEvent;
        public event PressedPeekButtonHandler PressedPeekButtonEvent;
        public event ReleasedPeekButtonHandler ReleasedPeekButtonEvent;

        public List<PortraitView> Player1PortraitViews { get { return player1PortraitViews; } }
        public List<PortraitView> Player2PortraitViews { get { return player2PortraitViews; } }
        public GameObject GameOverView { get { return gameOverView; } }

        [SerializeField]
        private List<PortraitView> player1PortraitViews = new List<PortraitView>();

        [SerializeField]
        private List<PortraitView> player2PortraitViews = new List<PortraitView>();

        [SerializeField]
        private GameObject gameOverView;

        public void ClickEndTurnButton()
        {
            if (ClickedEndTurnButtonEvent != null)
            {
                ClickedEndTurnButtonEvent();
            }
        }

        public void ClickNewGameButton()
        {
            if (ClickedNewGameButtonEvent != null)
            {
                ClickedNewGameButtonEvent();
            }
        }

        public void PressPeekButton()
        {
            if (PressedPeekButtonEvent != null)
            {
                PressedPeekButtonEvent();
            }
        }

        public void ReleasePeekButton()
        {
            if (ReleasedPeekButtonEvent != null)
            {
                ReleasedPeekButtonEvent();
            }
        }
    }
}