using UnityEngine;

namespace Snipe
{
    public class CameraOrthoSize : MonoBehaviour 
    {
        private Camera camera;

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
            }
    	}
    }
}