using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace coffee
{
    /// <summary>
    /// 2021.1124
    /// �����t��
    /// ���a���������ť
    /// �����ϰ�B�����O�P�y���ˮ`
    /// </summary>
    public class AttackSystem : MonoBehaviour
    {
        #region Field Public
        [Header("�����O"), Range(0, 500)]
        public float attack = 20;
        [Header("�����N�o�ɶ�"), Range(0, 5)]
        public float timeAttack = 1.3f;
        [Header("����ǰe�ˮ`�ɶ�"), Range(0, 3)]
        public float delaySendDamage = 0.2f;
        [Header("�����ϰ�ؤo�P�첾")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;
        [Header("�����P�����ʵe�Ѽ�")]
        public string parameterAttack = "AttackLayerTrigger";
        public string parameterWalk = "WalkingSwitch";
        [Header("�����ƥ�")]
        public UnityEvent onAttcck;
        [Header("�����ϼh�B���")]
        public AvatarMask maskAttack;
        #endregion

        #region Field Private
        private Animator ani;
        private bool isAttack;
        #endregion

        #region Property Private
        public bool keyAttack { get => Input.GetKeyDown(KeyCode.Mouse0); }
        #endregion

        #region Event
        private void Awake()
        {
            ani = GetComponent<Animator>();
        }

        private void Update()
        {
            Attack();
        }
        #endregion

        #region DrawGizmos
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.5f, 0.2f, 0.3f);
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
        }
        #endregion

        #region Method Private
        private void Attack()
        {
            bool isWalk = ani.GetBool(parameterWalk);

            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftLeg, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightLeg, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftFootIK, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightFootIK, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.Root, !isWalk);

            if (keyAttack && !isAttack)
            {
                onAttcck.Invoke();
                isAttack = true;
                ani.SetTrigger(parameterAttack);
                StartCoroutine(DelayHit());
            }
        }

        private IEnumerator DelayHit()
        {
            yield return new WaitForSeconds(delaySendDamage);

            Collider[] hits = Physics.OverlapBox(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 7);

            if (hits.Length > 0) hits[0].GetComponent<HurtSystem>().Hurt(attack);

            float waitToNextAttack = timeAttack - delaySendDamage;
            yield return new WaitForSeconds(waitToNextAttack);
            isAttack = false;
        }
        #endregion
    }
}