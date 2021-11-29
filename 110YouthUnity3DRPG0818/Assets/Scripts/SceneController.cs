using UnityEngine;
using UnityEngine.SceneManagement;

namespace coffee
{
    /// <summary>
    /// ��������
    /// ���w�e���Y����
    /// ���}�C��
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// ���J���w����
        /// </summary>
        /// <param name="nameScene"></param>
        public void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }

        /// <summary>
        /// ���}�C��
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}