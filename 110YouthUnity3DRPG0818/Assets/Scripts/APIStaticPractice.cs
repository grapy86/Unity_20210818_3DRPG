using UnityEngine;

public class APIStaticPractice : MonoBehaviour
{
    void Start()
    {
        int cameraCount = Camera.allCamerasCount;
        print("�Ҧ���v���ƶq�G" + cameraCount);

        Vector2 gravity2D = Physics2D.gravity;
        gravity2D = new Vector2(0, -20f);
        Physics2D.gravity = gravity2D;
        print("2D���O�G" + gravity2D);

        float pi = Mathf.PI;
        print("��P�v�G" + pi);  
        
        Time.timeScale = 0.5f;
        print("�ɶ��t�סG" + Time.timeScale);

        print("9.999�L����˥h�G" + Mathf.FloorToInt(9.999f));

        print("���I�Z���G" + Vector3.Distance(new Vector3(1, 1, 1), new Vector3(22, 22, 22)));

        Application.OpenURL("https://unity.com/");

    }

    void Update()
    {
        bool anyKey = (Input.anyKey);
        print(anyKey);
        bool space = Input.GetKeyDown(KeyCode.Space);
        print(space);
        print("�g�L�ɶ��G" + Time.timeSinceLevelLoad);
    }
}
