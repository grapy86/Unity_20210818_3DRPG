using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace coffee
{
    /// <summary>
    /// Coffee 20210906
    /// 第三人稱控制器
    /// 移動、跳躍
    /// </summary>

    // 修飾詞 類別 類別名稱 : 繼承類別
    // 欄位 Field, 屬性 Property, 方法 Method, 事件 Event
    public class ThirdPersonController : MonoBehaviour
    {
        #region Field (欄位)
        [Header("移動速度"), Range(0, 500)]
        public float speed = 10.5f;
        [Header("跳躍高度"), Range(0, 1000)]
        public float jump = 100;
        [Header("檢測地面資料")]
        [Tooltip("檢查角色是否在地面上")]
        public bool isGrounded = false;
        public Vector3 checkGroundOffset;
        [Range(0, 3)]
        public float checkGroundRadius = 0.2f;
        [Header("音效")]
        public AudioClip soundJump;
        public AudioClip soundGround;
        [Header("動畫參數")]
        public string animatorParWalk = "WalkingSwitch";
        public string animatorParRun = "RinningSwitch";
        public string animatorParInjury = "InjuryTrigger";
        public string animatorParDeath = "DeathTrigger";
        public string animatorParJump = "JumpTrigger";
        public string animatorParIsGrounded = "IsGrounded";

        [Header("玩家角色物件")]
        public GameObject playerObject;

        private AudioSource aud;
        private Rigidbody rig;
        private Animator aniCtrl;
        #endregion

        #region Property (屬性)
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        private float volumeRandom { get => Random.Range(0.7f, 1.5f); }
        #endregion

        #region Method (方法)
        private void Move(float speedMove)
        {
            rig.velocity =
                Vector3.forward * MoveInput("Vertical") * speedMove +
                Vector3.right * MoveInput("Horizontal") * speedMove +
                Vector3.up * rig.velocity.y;
        }
        private float MoveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }
        private bool CheckGround()
        {
            Collider[] hits = Physics.OverlapSphere(
                transform.position +
                transform.right * checkGroundOffset.x +
                transform.up * checkGroundOffset.y +
                transform.forward * checkGroundOffset.z,
                checkGroundRadius, 1 << 3);
            if (!isGrounded && hits.Length > 0) aud.PlayOneShot(soundGround, volumeRandom);
            isGrounded = hits.Length > 0;
            return hits.Length > 0;
        }
        private void Jump()
        {
            if (CheckGround() && keyJump) 
            { 
                rig.AddForce(transform.up * jump);
                aud.PlayOneShot(soundJump, volumeRandom);
            }
        }
        private void UpdateAnimation()
        {
            aniCtrl.SetBool(animatorParWalk, MoveInput("Vertical") != 0 || MoveInput("Horizontal") != 0);
            aniCtrl.SetBool(animatorParIsGrounded, isGrounded);
            if (keyJump) aniCtrl.SetTrigger(animatorParJump);
        }
        #endregion

        #region Event (事件)
        private void Start()
        {
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            rig = gameObject.GetComponent<Rigidbody>();
            aniCtrl = GetComponent<Animator>();
        }
        private void Update()
        {
            Jump();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            Move(speed);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(
                transform.position +
                transform.right * checkGroundOffset.x +
                transform.up * checkGroundOffset.y +
                transform.forward * checkGroundOffset.z,
                checkGroundRadius);
        }
        #endregion
    }

}
