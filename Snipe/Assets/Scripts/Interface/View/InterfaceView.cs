using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class InterfaceView : MonoBehaviour
    {
        public List<PortraitView> Player1PortraitViews { get { return player1PortraitViews; } }
        public List<PortraitView> Player2PortraitViews { get { return player2PortraitViews; } }

        [SerializeField]
        private List<PortraitView> player1PortraitViews = new List<PortraitView>();

        [SerializeField]
        private List<PortraitView> player2PortraitViews = new List<PortraitView>();
    }
}