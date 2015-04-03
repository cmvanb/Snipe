using UnityEngine;
using UnityEngine.UI;

namespace Snipe
{
    public class ActionsView : MonoBehaviour
    {
        [SerializeField]
        private Image ap1;

        [SerializeField]
        private Image ap2;

        [SerializeField]
        private Sprite empty;

        [SerializeField]
        private Sprite full;

        public void SetActionPoints(int actionPoints)
        {
            switch (actionPoints)
            {
                case 2:
                    ap1.sprite = full;
                    ap2.sprite = full;
                    break;
                case 1:
                    ap1.sprite = full;
                    ap2.sprite = empty;
                    break;
                case 0:
                default:
                    ap1.sprite = empty;
                    ap2.sprite = empty;
                    break;
            }
        }
    }
}