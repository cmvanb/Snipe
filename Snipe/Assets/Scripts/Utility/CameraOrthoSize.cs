using UnityEngine;

namespace Snipe
{
    public delegate void ResolutionChangedHandler(int width, int height);

    public class CameraOrthoSize : MonoBehaviour 
    {
        public event ResolutionChangedHandler ResolutionChangedEvent;

        private new Camera camera;

        private int bufferedScreenHeight; 

        void Awake()
        {
            camera = GetComponent<Camera>();
        }

    	void Update() 
        {
            if (bufferedScreenHeight != Screen.height)
            {
                Debug.Log("Resolution changed: " + Screen.width + " " + Screen.height);

                bufferedScreenHeight = Screen.height;

                camera.orthographicSize = (bufferedScreenHeight / 2.0f) * (1 / Constants.PixelsPerUnit);

                if (ResolutionChangedEvent != null)
                {
                    ResolutionChangedEvent(Screen.width, Screen.height);
                }
            }
    	}
    }
}