using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace coffee
{
    /// <summary>
    /// �ĤH�欰
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region Field Public
        [Header("���ʳt��"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("�����O"), Range(0, 200)]
        public float attack = 35;
        [Header("�d��G�l�ܻP����")]
        public float rangeAttack = 5;
        public float rangeTrack = 15;
        [Header("�����N�o�ɶ�"), Range(0, 5)]
        public float timeAttack = 2.5f;
        [Header("��������ǰe�ˮ`�ɶ�"), Range(0, 5)]
        public float delaySendDamage = 0.5f;

        [Header("�����H�����")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("�����H�����")]
        public Vector2 v2RandomWalk = new Vector2(3, 7);

        [Header("�����ϰ�첾�P�ؤo")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;

        [Header("���V���a�t��"), Range(0, 50)]
        public float speedLookAt = 5;
        #endregion

        #region Field Private
        [SerializeField]
        private StateEnemy state;
        private bool isIdle;
        private bool isWalk;
        private bool isTrack;
        private bool isAttack;
        private bool targetIsDead;
        private Animator ani;
        private NavMeshAgent nma;
        private string parameterIdleWalk = "WalkingSwitch";
        private string parameterAttack = "AttackTrigger";
        /// <summary>
        /// �H���樫�y��
        /// </summary>
        private Vector3 v3RandomWalk { get => Random.insideUnitSphere * rangeTrack + transform.position; }
        /// <summary>
        /// �̲��H���樫�y�СG�z�L API ���o���椺�i���쪺��m
        /// </summary>
        private Vector3 v3RandomWalkFinal;
        /// <summary>
        /// ���a�O�_�b�p�F�d��
        /// </summary>
        private bool playerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }

        #endregion

        private void OnDrawGizmos()
        {
            #region �����d��B�l�ܽd��P�H���樫�y��
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);

            if (state == StateEnemy.Walk)
            {
                Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
                Gizmos.DrawSphere(v3RandomWalkFinal, 0.3f);
            }
            #endregion

            #region �����I���P�w�ϰ�]���Ķˮ`�d��^
            Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position + transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
            #endregion
        }

        #region Event
        private Transform traPlayer;
        private string namePlayer = "Brad";

        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = speed;

            traPlayer = GameObject.Find(namePlayer).transform;

            nma.SetDestination(transform.position);
        }

        private void Update()
        {
            StateManager();
        }
        #endregion

        #region Method
        /// <summary>
        /// ���A�޲z
        /// </summary>
        private void StateManager()
        {
            switch (state)
            {
                case StateEnemy.Idle:
                    Idle();
                    break;
                case StateEnemy.Walk:
                    Walk();
                    break;
                case StateEnemy.Track:
                    Track();
                    break;
                case StateEnemy.Attack:
                    Attack();
                    break;
                case StateEnemy.Hurt:
                    break;
                case StateEnemy.Dead:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ���ݡG�H����ƫ�i�J�������A
        /// </summary>
        private void Idle()
        {
            if (!targetIsDead && playerInTrackRange) state = StateEnemy.Track;

            if (isIdle) return;
            isIdle = true;

            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(IdleEffect());
        }

        private IEnumerator IdleEffect()
        {
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);

            state = StateEnemy.Walk;
            isIdle = false;
        }

        /// <summary>
        /// �����G�H����ƫ�i�J���ݪ��A
        /// ���˨��x�s�樫�ϰ줺���檺�I����T��A�A�H������i�樫���y�Ь��̲׮y��
        /// </summary>
        private void Walk()
        {
            #region �������ϰ�
            if (!targetIsDead && playerInTrackRange) state = StateEnemy.Track;

            nma.SetDestination(v3RandomWalkFinal);
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.1f);
            #endregion

            if (isWalk) return;
            isWalk = true;

            NavMeshHit hit;
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack, NavMesh.AllAreas);
            v3RandomWalkFinal = hit.position;

            StartCoroutine(WalkEffect());
        }

        private IEnumerator WalkEffect()
        {
            float randomWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            yield return new WaitForSeconds(randomWalk);

            state = StateEnemy.Idle;
            isWalk = false;
        }

        /// <summary>
        /// �l�ܪ��a
        /// ���a�i�J�l�ܽd�򤺮ɡA�Ǫ��}�l�l�ܪ��a�A�i�J�����d�򤺤����ܧ������A
        /// </summary>
        private void Track()
        {
            if (!isTrack)
            {
                StopAllCoroutines();
            }
            isTrack = true;

            nma.isStopped = false;
            nma.SetDestination(traPlayer.position);
            ani.SetBool(parameterIdleWalk, true);

            if (nma.remainingDistance <= rangeAttack) state = StateEnemy.Attack;
        }

        /// <summary>
        /// �������a
        /// �N���a�y�г]���e�i�ؼСA�i�J�i�����d��K��������欰�A���}�d��ɤ����ܰl�ܪ��A
        /// </summary>
        private void Attack()
        {
            nma.isStopped = true;
            ani.SetBool(parameterIdleWalk, false);
            nma.SetDestination(traPlayer.position);
            LookAtPlayer();

            if (nma.remainingDistance > rangeAttack) state = StateEnemy.Track;

            if (isAttack) return;
            isAttack = true;

            ani.SetTrigger(parameterAttack);
            StartCoroutine(DelaySendDamageToTarget());
        }

        /// <summary>
        /// ����ǰe�ˮ`��ؼ�
        /// �t�X����ʵe�H�ŦX��ı�ĪG
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelaySendDamageToTarget()
        {
            yield return new WaitForSeconds(delaySendDamage);

            Collider[] hits = Physics.OverlapBox(
                            transform.position + transform.right * v3AttackOffset.x +
                            transform.up * v3AttackOffset.y +
                            transform.forward * v3AttackOffset.z,
                            v3AttackSize / 2, Quaternion.identity, 1 << 6);

            if (hits.Length > 0) targetIsDead = hits[0].GetComponent<HurtSystem>().Hurt(attack);
            if (targetIsDead) TargetDead();

            float waitToNextAttack = timeAttack - delaySendDamage;
            yield return new WaitForSeconds(waitToNextAttack);
            isAttack = false;
        }

        /// <summary>
        /// �ؼЦ��`
        /// </summary>
        private void TargetDead()
        {
            state = StateEnemy.Walk;
            isIdle = false;
            isWalk = false;
            nma.isStopped = false;
        }

        /// <summary>
        /// �ϩǪ����¦V���a
        /// </summary>
        private void LookAtPlayer()
        {
            Quaternion angle = Quaternion.LookRotation(traPlayer.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            ani.SetBool(parameterIdleWalk, transform.rotation != angle);
        }
    }
    #endregion
}