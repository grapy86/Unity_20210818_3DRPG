using UnityEngine;  // �ޥ� API
using UnityEngine.Video;

/// <summary>
/// Coffee 20210906
/// �ĤT�H�ٱ��
/// ���ʡB���D
/// </summary>

// �׹��� ���O ���O�W�� : �~�����O
// ��� Field, �ݩ� Property, ��k Method, �ƥ� Event
public class ThirdPersonController : MonoBehaviour  
{
    #region Field(���)
    // �x�s�C����ơA�Ҧp�G���ʳt�סB���D����
    // �`�θ�������Gint (���), float (�B�I��), string (�r��), bool (���L��)
    // �y�k�G�׹��� ������� ���W�� (���w �w�]��) ����
    // �׹���
    //  public - ���\��L���O�s�� - �b�ݩʭ��O��� - �ݭn�վ㪺��Ƴ]��public
    //  private - �T���L���O�s�� - �b�ݩʭ��O���� - �w�]��

    // Attribute (����ݩ�)�G���U�����
    //  �y�k�G[�ݩʦW��(�ݩʭ�)]
    //  Header (���D), Tooltip (����), Range (�ƭȽd��)
    // �� Unity�B�@���ݩʭ��O���]�w���u���װ���{���X
    [Header("���ʳt��"), Tooltip("��J�ƭȽվ㨤�Ⲿ�ʳt��"), Range(1, 500)]
    public float speed = 10.5f;

    #region Unity �������
    // Color (�C��)
    public Color color;  // �w�]��
    public Color red = Color.red;  // �����C��
    public Color color1 = new Color(0.5f, 0.5f, 0, 0.5f);  // �ۭq�C��A�ĥ|��� A (�z����) �i�ٲ�

    // Vector (�y��) ���� 2 - 4
    public Vector2 v2;
    public Vector2 v2Right = Vector2.right;  // ���w�S�w��V���y�Ь�1
    public Vector3 v3Forward = Vector3.forward;
    public Vector2 v2Custom = new Vector2(7.5f, 100.9f);  // �ۭq�y��

    // ���� - enum (�C�|���)
    public KeyCode key;
    public KeyCode move = KeyCode.W;
    public KeyCode jump = KeyCode.Space;

    // �C����������G������w�w�]��
    public AudioClip sound;  // ���ġG�䴩 mp3, ogg, wav
    public VideoClip video;  // �v���G�䴩 mp4
    public Sprite sprite;  //�Ϥ��G�䴩 png, jpeg, ���䴩 gif
    public Texture2D texture2D;  // 2D �Ϥ��G�䴩 png, jpeg
    public Material material;  // ����y
    #endregion
    #endregion

    #region Property(�ݩ�)

    #endregion

    #region Method(��k)

    #endregion

    #region Event(�ƥ�)

    #endregion
}
