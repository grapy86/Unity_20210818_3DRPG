using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace coffee
{
    /// <summary>
    /// �C���޲z��
    /// �����B�z
    /// 1. ���ȧ���
    /// 2. ���a���`
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Field
        [Header("�s�ժ���")]
        public CanvasGroup groupFinal;
        [Header("�����e�����D")]
        public Text textTitle;

        private string titleWin = "You Win";
        private string titleLose = "You Failed...";
        #endregion

        #region Method Public
        public void StartFadeFinalUI(bool win)
        {
            StartCoroutine(FadeFinalUI(win ? titleWin : titleLose));
        }
        #endregion

        #region Method Private
        private IEnumerator FadeFinalUI(string title)
        {
            textTitle.text = title;
            groupFinal.interactable = true;
            groupFinal.blocksRaycasts = true;

            for(int i = 0; i < 10; i++)
            {
                groupFinal.alpha += 0.1f;
                yield return new WaitForSeconds(0.02f);
            }
        }
        #endregion
    }
}