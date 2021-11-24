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

        // ���Ƽg�����O�������ϥ� override
        public override void Hurt(float damage)
        {
            // �Ӧ����������O��(base).�����O�������e
            // ����N��Ӥ����O���Ҧ����e�A�Y�R������h���|����
            base.Hurt(damage);

            imgHp.fillAmount = hp / hpMax;
        }
    }
}

