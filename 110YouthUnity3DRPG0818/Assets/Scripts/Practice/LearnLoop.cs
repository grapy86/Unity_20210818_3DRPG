using UnityEngine;

namespace coffee.Practice
{
    /// <summary>
    /// �{�Ѱj��
    /// while, do while, for, foreach
    /// </summary>
    public class LearnLoop : MonoBehaviour
    {
        private void Start()
        {
            // while
            // if (bool) { content }    - boolean = true ����@��
            // while (bool) { content } - boolean = true �������
            int a = 1;
            while (a < 6) { print("while loop: " + a); a++; }

            // for
            // for (default; bool; do) { content }
            for (int i = 0; i < 6; i++) { print("for loop: " + i); }
        }

    }

}
