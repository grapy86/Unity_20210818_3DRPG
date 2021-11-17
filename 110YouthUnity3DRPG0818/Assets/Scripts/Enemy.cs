using UnityEngine;
using System.Collections;

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

        [Header("�����H�����")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);

        #endregion

        #region Field Private
        [SerializeField]
        private StateEnemy state;
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);
        }

        #region Event
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
                    break;
                case StateEnemy.Walk:
                    break;
                case StateEnemy.Track:
                    break;
                case StateEnemy.Attack:
                    break;
                case StateEnemy.Hurt:
                    break;
                case StateEnemy.Dead:
                    break;
                default:
                    break;
            }
        }

        private bool isIdle;

        /// <summary>
        /// ���ݡG�H����ƫ�i�J�������A
        /// </summary>
        private void Idle()
        {
            if (isIdle) return;
            isIdle = true;

            StartCoroutine(IdleEffect());
        }

        private IEnumerator IdleEffect()
        {
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);

            state = StateEnemy.Walk;
            isIdle = false;
        }
        #endregion
    }

}
