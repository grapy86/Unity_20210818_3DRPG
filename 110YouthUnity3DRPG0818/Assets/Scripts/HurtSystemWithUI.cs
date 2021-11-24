using UnityEngine;
using UnityEngine.UI;

namespace coffee
{
    /// <summary>
    /// 包含介面的受傷系統
    /// 可以處理血條更新
    /// </summary>
    public class HurtSystemWithUI : HurtSystem
    {
        [Header("要更新的血條")]
        public Image imgHp;

        // 欲複寫父類別成員須使用 override
        public override void Hurt(float damage)
        {
            // 該成員的父類別基底(base).父類別內的內容
            // 此行代表該父類別的所有內容，若刪除此行則不會執行
            base.Hurt(damage);

            imgHp.fillAmount = hp / hpMax;
        }
    }
}

