using UnityEngine;
using System.Collections;

namespace coffee
{
    /// <summary>
    /// 敵人行為
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region Field Public
        [Header("移動速度"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("攻擊力"), Range(0, 200)]
        public float attack = 35;
        [Header("範圍：追蹤與攻擊")]
        public float rangeAttack = 5;
        public float rangeTrack = 15;

        [Header("等待隨機秒數")]
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
        /// 狀態管理
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
        /// 等待：隨機秒數後進入走路狀態
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
