using UnityEngine;  // �ޥ� API
using UnityEngine.Video;


namespace coffee.Practice
{
    /// <summary>
    /// Coffee 20210906
    /// �ĤT�H�ٱ��
    /// ���ʡB���D
    /// </summary>

    // �׹��� ���O ���O�W�� : �~�����O
    // ��� Field, �ݩ� Property, ��k Method, �ƥ� Event
    public class ThirdPersonController : MonoBehaviour
    {
        #region Field (���)
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
        [Header("���ʳt��"), Range(0, 500)]
        public float speed = 10.5f;
        [Header("���D����"), Range(0, 1000)]
        public float jump = 100;
        [Header("�˴��a�����")]
        [Tooltip("�ˬd����O�_�b�a���W")]
        public bool isGrounded = false;
        public Vector3 checkGroundOffset;
        [Range(0, 3)]
        public float checkGroundRadius = 0.2f;
        [Header("����")]
        public AudioClip audioJump;
        public AudioClip audioFall;
        [Header("�ʵe�Ѽ�")]
        public string animatorParWalk = "WalkingSwitch";
        public string animatorParRun = "RinningSwitch";
        public string animatorParInjury = "InjuryTrigger";
        public string animatorParDeath = "DeathTrigger";
        public string animatorParJump = "JumpTrigger";
        public string animatorParIsGrounded = "IsGrounded";

        [Header("���a���⪫��")]
        public GameObject playerObject;

        private AudioSource aud;
        private Rigidbody rig;
        private Animator aniCtrl;


        #region Unity �������
        /*
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

        [Header("����")]
        // Component (����) �bInspector (�ݩʭ��O) �W�i���|
        public Transform tra;
        public Animation aniOld;
        public Animator aniNew;
        public Light lig;
        public Camera cam;
        */
        #endregion
        #endregion

        #region Property (�ݩ�)
        #region �ݩʽm��
        /*
        // �x�s��ơA�P���ۦP
        // �t���G�ݩʥi�H�]�w�s���v�� - Get, Set
        // �y�k�G�׹��� ������� �ݩʦW�� { ��; �s; }
        public int ReadAndWrite { get; set; }
        // ��Ū�ݩʡG�u�� Get (���o)
        public int Read { get; }
        // ��Ū�ݩʡG�z�L Get �]�w�w�]�ȡA����r Return ���Ǧ^��
        public int ReadValue
        {
            get
            {
                return 77;
            }
        }

        // �߼g�ݩʡG�T��A������ Get
        // public int write { set: }

        private int _HP;
        public int HP
        {
            get
            {
                return _HP;
            }
            set
            {
                _HP = value;
            }
        }
        */
        #endregion

        // C# 7.0 �s���l �i�H�ϥ� Lambda => �B��l
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        #endregion

        #region Method (��k)
        // �w�q�P��@�������{�����϶��A�\��
        // �y�k�G�׹��� �Ǧ^������� ��k�W�� (�Ѽ�1, ... , �Ѽ�N) { �{���϶� }
        // �`�ζǦ^�����Gvoid (�L�Ǧ^)

        private void Move(float speedMove)
        {
            rig.velocity =
                Vector3.forward * MoveInput("Vertical") * speedMove +
                Vector3.right * MoveInput("Horizontal") * speedMove +
                Vector3.up * rig.velocity.y;
        }
        private float MoveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }
        private bool CheckGround()
        {
            Collider[] hits = Physics.OverlapSphere(
                transform.position +
                transform.right * checkGroundOffset.x +
                transform.up * checkGroundOffset.y +
                transform.forward * checkGroundOffset.z,
                checkGroundRadius, 1 << 3);
            isGrounded = hits.Length > 0;
            return hits.Length > 0;
        }
        private void Jump()
        {
            if (CheckGround() && keyJump) { rig.AddForce(transform.up * jump); }
        }
        private void UpdateAnimation()
        {
            aniCtrl.SetBool(animatorParWalk, MoveInput("Vertical") != 0 || MoveInput("Horizontal") != 0);
            aniCtrl.SetBool(animatorParIsGrounded, isGrounded);
            if (keyJump) aniCtrl.SetTrigger(animatorParJump);
        }

        #region �m��
        /*
        private void Test()
        {
            print("Method Test");
        }

        private int ReturnJump()
        {
            return 999;
        }

        // �Ѽƻy�k�G������� �ѼƦW�� ���w �w�]��
        // ���w�]�Ȫ��Ѽƥi�H����J�޼ơA��񦡰Ѽ�
        private void Skill(int damage, string effect = "Ash", string sound = "gah")
        {
            print("�Ѽƪ��� - �ˮ`�ȡG" + damage);
            print("�Ѽƪ��� - �ޯ�S��" + effect);
            print("�Ѽƪ��� - ����" + sound);
        }
        */
        // �C�Ĳv�@�k
        /*
        private void Skill100()
        {
            print("�ˮ`�ȡG" + 100);
            print("�ޯ�S��");
        }
        private void Skill150()
        {
            print("�ˮ`�ȡG" + 150);
            print("�ޯ�S��");
        }
        private void Skill200()
        {
            print("�ˮ`�ȡG" + 200);
            print("�ޯ�S��");
        }
        */
        #endregion

        #region BMI�m��
        /*
        // BMI
        /// <summary>
        /// �p�� BMI ��k
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="name"></param>
        /// <returns>BMI ���G</returns>
        private float BMI(float weight, float height, string name = "human")
        {
            print(name + "'s BMI");
            return weight / (height * height);
        }
        */
        #endregion

        #endregion

        #region Event (�ƥ�)
        // �S�w�ɶ��I�|���檺��k�A�{�����J�f Start�A���� Console Main
        // �}�l�ƥ�G�C���}�l�ɰ���@��
        // �B�z��l�ơB������
        private void Start()
        {
            #region ��X��k
            /*
            print("Hello MDFK!");

            Debug.Log("�@��T��");
            Debug.LogWarning("ĵ�i�T��");
            Debug.LogError("���~�T��");
            */
            #endregion

            #region �ݩʽm��
            /*
            // ���P�ݩ� Get (���o) �P Set (�]�w)
            print("����� - ���ʳt�סG" + MovementSpeed);
            print("�ݩʸ�� - Ū�g�ݩʡG" + ReadAndWrite);
            MovementSpeed = 20.5f;
            ReadAndWrite = 90;
            print("�ק�᪺���");
            print("����� - ���ʳt�סG" + MovementSpeed);
            print("�ݩʸ�� - Ū�g�ݩʡG" + ReadAndWrite);
            // ��Ū�ݩ�
            // Read = 7;  // ��Ū�ݩʤ���Set (�]�w)
            print("��Ū�ݩʡG" + Read);
            print("��Ū�ݩʡA���w�]�ȡG" + ReadValue);

            // �ݩʦs���m��
            print("HP: " + HP);
            HP = 100;
            print("HP: " + HP);
            */
            #endregion

            #region �C�Ĳv�@�k
            /*
            Skill100();
            Skill150();
            Skill200();
            */
            #endregion

            /* BMI �m��
            print(BMI(60, 1.66f, "Coffee"));
            */

            #region �m��
            /*
            // �I�s���ѼƤ�k�ɡA������J�������޼�
            Skill(300);  // �ϥο�񦡰Ѽ�
            Skill(999, "Explosion");  // �ۭq�Ѽ�
            Skill(500, sound: "shu");  // ���Ƽƿ�񦡰ѼƮɥi�ϥΫ��W�Ѽƻy�k�G�ѼƦW��: ��


            // �I�s�ۭq��k�y�k�G��k�W��();
            Test();
            // �I�s���Ǧ^�Ȫ� Method
            //  1. �ϰ��ܼƫ��w�Ǧ^�� - �ϰ��ܼƶȯ�b�����c���s��
            int j = ReturnJump();
            print("���D�ȡG" + j);
            //  2. �N�Ǧ^��k���Ȩϥ�
            print("���D�� (��)�G" + (ReturnJump() + 1));
            */
            #endregion

            // �覡1. �������W��.���o����(����(��������)) ��@ ��������;
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            // �覡2. ���}���C������.���o����<�x��>();
            // gameObject�Ψӫ��N��e�}���ұ������C�����󥻨�
            rig = gameObject.GetComponent<Rigidbody>();
            // �覡3. ���o����<�x��>();
            aniCtrl = GetComponent<Animator>();
        }

        // ��s�ƥ�G�C������� 60 �� (60 FPS)
        // �B�z����ʹB�ʡB���ʪ���B��ť���a��J����
        private void Update()
        {
            Jump();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            Move(speed);
        }

        // ø�s�ϥܨƥ�G�bUnity Editor��ø�s�ϥܻ��U�}�o�A�o����|�۰�����
        private void OnDrawGizmos()
        {
            // 1. ���w�C��F2. ø�s�ϧ�
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(
                transform.position +
                transform.right * checkGroundOffset.x +
                transform.up * checkGroundOffset.y +
                transform.forward * checkGroundOffset.z,
                checkGroundRadius);
        }
        #endregion
    }
}