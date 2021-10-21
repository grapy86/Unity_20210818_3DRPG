using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace coffee.Dialogue
{
    /// <summary>
    /// 2021.1021
    /// 對話系統
    /// 顯示對話框與文字浮現效果
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        [Header("對話系統介面物件")]
        public CanvasGroup groupDialogue;
        public Text textName;
        public Text textContent;
        public GameObject goTriangle;
        [Header("對話間隔"), Range(0, 10)]
        public float dialogueInterval = 0.3f;

        /// <summary>
        /// 開始對話
        /// </summary>
        public void Dialogue(DataDialogue data)
        {
            StartCoroutine(SwitchDialogueGroup());
            StartCoroutine(ShowDialogueContent(data));
        }
        private IEnumerator SwitchDialogueGroup()
        {
            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += 0.1f;
                yield return new WaitForSeconds(0.01f);
            }
        }
        private IEnumerator ShowDialogueContent(DataDialogue data)
        {
            textContent.text = "";
            textName.text = "";
            for(int i = 0; i < data.beforeMission[0].Length; i++)
            {
                textContent.text += data.beforeMission[0][i];
                yield return new WaitForSeconds(dialogueInterval);
            }
        }
    }
}

