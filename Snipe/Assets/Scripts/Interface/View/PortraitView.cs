using UnityEngine;
using UnityEngine.UI;

namespace Snipe
{
    public class PortraitView : MonoBehaviour
    {
        [SerializeField]
        private Text nameText;

        [SerializeField]
        private Text classText;

        [SerializeField]
        private Image portraitImage;

        public void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetClass(string _class)
        {
            classText.text = _class;
        }

        public void SetSprite(Sprite sprite)
        {
            portraitImage.sprite = sprite;
        }
    }
}