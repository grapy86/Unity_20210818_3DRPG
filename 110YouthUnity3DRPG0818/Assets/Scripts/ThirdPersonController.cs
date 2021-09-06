using UnityEngine;  // 引用 API
using UnityEngine.Video;

/// <summary>
/// Coffee 20210906
/// 第三人稱控制器
/// 移動、跳躍
/// </summary>

// 修飾詞 類別 類別名稱 : 繼承類別
// 欄位 Field, 屬性 Property, 方法 Method, 事件 Event
public class ThirdPersonController : MonoBehaviour  
{
    #region Field(欄位)
    // 儲存遊戲資料，例如：移動速度、跳躍高度
    // 常用資料類型：int (整數), float (浮點數), string (字串), bool (布林值)
    // 語法：修飾詞 資料類型 欄位名稱 (指定 預設值) 結尾
    // 修飾詞
    //  public - 允許其他類別存取 - 在屬性面板顯示 - 需要調整的資料設為public
    //  private - 禁止其他類別存取 - 在屬性面板隱藏 - 預設值

    // Attribute (欄位屬性)：輔助欄位資料
    //  語法：[屬性名稱(屬性值)]
    //  Header (標題), Tooltip (提示), Range (數值範圍)
    // ※ Unity運作時屬性面板之設定值優先度高於程式碼

    [Header("移動速度"), Range(0, 500)]
    public float MovementSpeed = 10.5f;
    [Header("跳躍高度"), Range(0, 1000)]
    public float JumpHeight = 100;
    [Header("檢測地面資料")]
    [Tooltip("檢查角色是否在地面上")]
    public bool isGrounded = false;
    public Vector3 CheckGroundOffset;
    [Range(0, 3)]
    public float CheckGroundRadius = 0.2f;

    [Header("音效")]
    public AudioClip SoundJump;
    public AudioClip SoundFall;

    [Header("動畫參數")]
    public string AnimatorWalk = "WalkingSwitch";
    public string AnimatorRun = "RinningSwitch";
    public string AnimatorInjury = "InjuryTrigger";
    public string AnimatorDeath = "DeathTrigger";

    private AudioSource Aud;
    private Rigidbody Rig;
    private Animator AniCtrl;


    #region Unity 資料類型
    /*
    // Color (顏色)
    public Color color;  // 預設值
    public Color red = Color.red;  // 內建顏色
    public Color color1 = new Color(0.5f, 0.5f, 0, 0.5f);  // 自訂顏色，第四欄位 A (透明度) 可省略

    // Vector (座標) 維度 2 - 4
    public Vector2 v2;
    public Vector2 v2Right = Vector2.right;  // 指定特定方向之座標為1
    public Vector3 v3Forward = Vector3.forward;
    public Vector2 v2Custom = new Vector2(7.5f, 100.9f);  // 自訂座標

    // 按鍵 - enum (列舉資料)
    public KeyCode key;
    public KeyCode move = KeyCode.W;
    public KeyCode jump = KeyCode.Space;

    // 遊戲資料類型：不能指定預設值
    public AudioClip sound;  // 音效：支援 mp3, ogg, wav
    public VideoClip video;  // 影片：支援 mp4
    public Sprite sprite;  //圖片：支援 png, jpeg, 不支援 gif
    public Texture2D texture2D;  // 2D 圖片：支援 png, jpeg
    public Material material;  // 材質球

    [Header("元件")]
    // Component (元件) 在Inspector (屬性面板) 上可折疊
    public Transform tra;
    public Animation aniOld;
    public Animator aniNew;
    public Light lig;
    public Camera cam;
    */
    #endregion
    #endregion

    #region Property(屬性)

    #endregion

    #region Method(方法)

    #endregion

    #region Event(事件)
    // 特定時間點會執行的方法，程式的入口 Start，等於 Console Main
    #endregion
}
