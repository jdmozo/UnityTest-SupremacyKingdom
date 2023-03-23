using UnityEngine;

namespace SupremacyKingdom
{
    /// <summary>
    /// To do
    /// </summary>
    public class ParallaxScrolling : MonoBehaviour
    {
        private Camera _mainCamera;
        private Vector3 previous_CameraTransform;
        public float ParallaxFactor;


        void Start()
        {
            _mainCamera = Camera.main;
            previous_CameraTransform = _mainCamera.transform.position;
        }

        void Update()
        {
            Vector3 delta = _mainCamera.transform.position - previous_CameraTransform;
            delta.y = 0; delta.z = 0;
            transform.position += delta / ParallaxFactor;
            previous_CameraTransform = _mainCamera.transform.position;
        }


    }
}