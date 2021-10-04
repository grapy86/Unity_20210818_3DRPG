using UnityEngine;

namespace coffee
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        #region Field
        [Header("�ؼЪ���")]
        public Transform target;
        [Header("�l�ܳt��"), Range(0, 100)]
        public float speedTrack = 1.5f;
        [Header("��������t��"), Range(0, 100)]
        public float speedTurnHorizontal = 5;
        [Header("��������t��"), Range(0, 100)]
        public float speedTurnVertical = 5;
        #endregion

        #region Property
        public float inputMouseX { get => Input.GetAxis("Mouse X"); }
        public float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region Event
        private void Update()
        {
            TurnCamera();
        }
        private void LateUpdate()
        {
            TrackTarget();
        }
        #endregion

        #region Method
        private void TrackTarget()
        {
            Vector3 posTarget = target.position;
            Vector3 posCamera = transform.position;
            posCamera = Vector3.Lerp(posCamera, posTarget, speedTrack * Time.deltaTime);
            transform.position = posCamera;
        }
        private void TurnCamera()
        {
            transform.Rotate(inputMouseY * Time.deltaTime * speedTurnVertical, inputMouseX * Time.deltaTime * speedTurnHorizontal, 0);
        }
        #endregion
    }
}