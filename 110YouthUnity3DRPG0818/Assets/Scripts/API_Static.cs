using UnityEngine;

public class API_Static : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region Static Property
        // Get
        // �y�k: ���O�W��.�R�A�ݩ�
        float r = Random.value;
        print("���o�R�A�ݩ� �H����: " + r);

        // Set
        // �y�k: ���O�W��.�R�A�ݩ� ���w ��;
        Cursor.visible = false;
        #endregion

        #region Static Method
        // �I�s �ѼơB�Ǧ^
        // ñ�� �ѼơB�Ǧ^
        // �y�k: ���O�W��.�R�A��k(�����޼�)
        float rangeFloat = Random.Range(10.5f, 20.9f);
        print("�B�I���H���d�� 10.5 - 20.9: " + rangeFloat);

        int rangeInt = Random.Range(1, 5);
        print("����H���d�� 1 - 5: " + rangeInt);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Static Property
        print("�g�L�ɶ�: " + Time.timeSinceLevelLoad);
        #endregion

        #region Static Method
        float h = Input.GetAxis("Horizontal");
        print("������: " + h);
        #endregion
    }
}
