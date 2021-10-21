using UnityEngine;

namespace coffee
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        /// <summary>
        /// 2021.1004
        /// �ĤT�H����v���t��
        /// �l�ܫ��w�ؼ�
        /// �i�T������P�������d��
        /// </summary>
        #region Field
        [Header("�ؼЪ���")]
        public Transform target;
        [Header("�l�ܳt��"), Range(0, 100)]
        public float speedTrack = 1.5f;
        [Header("��������t��"), Range(0, 100)]
        public float speedTurnHorizontal = 5;
        [Header("��������t��"), Range(0, 100)]
        public float speedTurnVertical = 5;
        [Header("X�b��������T�׭���")]
        public Vector2 limitAngleX = new Vector2(-0.2f, 0.2f);
        [Header("��v���b����e�諫�����୭��")]
        public Vector2 limitAngleFromTarget = new Vector2(-0.2f, 0);

        /// <summary>
        /// ��v���e��y�лP�Z��
        /// </summary>
        private Vector3 _posForward;
        private float lengthForward = 3;
        #endregion

        #region Property
        /// <summary>
        /// ���o�ƹ������P�����y��
        /// </summary>
        private float inputMouseX { get => Input.GetAxis("Mouse X"); }
        private float inputMouseY { get => Input.GetAxis("Mouse Y"); }

        public Vector3 posForward
        {
            get
            {
                _posForward = transform.position + transform.forward * lengthForward;
                _posForward.y = target.position.y;
                return _posForward;
            }
        }
        #endregion

        #region Event
        private void Update()
        {
            TurnCamera();
            LimitAngleXandZFromTarget();
            FreezeAngleZ();
        }
        private void LateUpdate()
        {
            TrackTarget();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.2f, 0, 1, 0.3f);
            _posForward = transform.position + transform.forward * lengthForward;
            _posForward.y = target.position.y;
            Gizmos.DrawSphere(_posForward, 0.15f);
        }
        #endregion

        #region Method
        /// <summary>
        /// �l�ܥؼ�
        /// </summary>
        private void TrackTarget()
        {
            Vector3 posTarget = target.position;
            Vector3 posCamera = transform.position;
            posCamera = Vector3.Lerp(posCamera, posTarget, speedTrack * Time.deltaTime);
            transform.position = posCamera;
        }
        /// <summary>
        /// ������v��
        /// </summary>
        private void TurnCamera()
        {
            transform.Rotate(
                inputMouseY * Time.deltaTime * speedTurnVertical, 
                inputMouseX * Time.deltaTime * speedTurnHorizontal, 0);
        }
        private void LimitAngleXandZFromTarget()
        {
            Quaternion angle = transform.rotation;
            angle.x = Mathf.Clamp(angle.x, limitAngleX.x, limitAngleX.y);
            angle.z = Mathf.Clamp(angle.z, limitAngleFromTarget.x, limitAngleFromTarget.y);
            transform.rotation = angle;
        }
        /// <summary>
        /// �ᵲZ�b����
        /// </summary>
        private void FreezeAngleZ()
        {
            Vector3 angle = transform.eulerAngles;
            angle.z = 0;
            transform.eulerAngles = angle;
        }
        #endregion
    }
}