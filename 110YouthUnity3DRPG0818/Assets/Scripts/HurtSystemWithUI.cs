using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace coffee
{
    /// <summary>
    /// �]�t���������˨t��
    /// �i�H�B�z�����s
    /// </summary>
    public class HurtSystemWithUI : HurtSystem
    {
        [Header("�n��s�����")]
        public Image imgHp;

        /// <summary>
        /// ����ĪG�M�Ϊ�����e��q
        /// </summary>
        private float hpEffectOriginal;

        // ���Ƽg�����O�������ϥ� override
        public override bool Hurt(float damage)
        {
            hpEffectOriginal = hp;

            // �Ӧ����������O��(base).�����O�������e
            // ����N��Ӥ����O���Ҧ����e�A�Y�R������h���|����
            base.Hurt(damage);
            StartCoroutine(HpBarEffect());

            return hp <= 0;
        }

        /// <summary>
        /// �������ĪG
        /// </summary>
        private IEnumerator HpBarEffect()
        {
            while (hpEffectOriginal != hp)
            {
                hpEffectOriginal--;
                imgHp.fillAmount = hpEffectOriginal / hpMax;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}

