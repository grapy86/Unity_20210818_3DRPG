using UnityEngine;

public class API_Non_Static : MonoBehaviour
{
    public Transform tra1;  // �׹��� �n�s���D�R�A�����O ���W��
    public Camera cam;
    public Light lig;
    void Start()
    {
        #region Non-Static Property
        // �P�R�A�t��
        // 1. �ݭn���骫��
        // 2. ���o���骫�� - �w�q���ñN�n�s��������s�J���
        // Get
        // �y�k: ���W��.�D�R�A�ݩ�
        print("Camera Position: " + tra1.position);
        print("Camera Depth: " + cam.depth);

        // Set
        // �y�k: ���W��.�D�R�A�ݩ� ���w ��
        tra1.position = new Vector3(99, 99, 99);
        cam.depth = 7;
        #endregion

        #region Non-Static Method
        // �I�s
        // �y�k: ���W��.�D�R�A��k�W��(�����޼�)
        lig.Reset();
        #endregion
    }

    void Update()
    {
        
    }
}
