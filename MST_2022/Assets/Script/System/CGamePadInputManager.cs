/*==============================================================================
    [CGamePadInputManager.cs]
    ・ゲームパッドの入力を管理する
--------------------------------------------------------------------------------
    2021.09.13 @Fujiwara Aiko
================================================================================
    History
        2021.09.13 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/


/* -------------使用方法--------------------

三種類のstatic関数を使いたい箇所で使用。

・GetButtonDown(ゲームパッドコード)
    ゲームパッドコードのボタンが押されたときtrue（Trigger）
・GetButton(ゲームパッドコード)
    ゲームパッドコードのボタンが押されている間true（Press） 
 ・public static Vector2 GetStickStatus(ゲームパッドのスティックコード)
    ゲームパッドのスティックの状態を取得（Vector2）

＊使用例＊
// ゲームパッドのRスティックが左側に倒されたら（Trigger）
if (CGamePadInputManager.GetButtonDown(CGamePadInputManager.GAME_PAD_CODE.RSTICK_LEFT))
{
    // 何らかの処理～
}

--------------------------------------------*/


// このスクリプトを入れたとき
// ↓各名称をUnityのInputManagerで各プロパティーに割り当てる

//  D-PAD-H     6th-Axis
//  D-PAD-V     7th-Axis
//  L-Stick-H   X-Axis
//  L-Stick-V   Y-Axis
//  R-Stick-H   4th-Axis
//  R-Stick-V   5th-Axis


using UnityEngine;

public class CGamePadInputManager : MonoBehaviour
{

    // ゲームパッドのボタンコード
    public enum GAME_PAD_CODE
    {
        A,              // Aボタン
        B,              // Bボタン
        X,              // Xボタン
        Y,              // Yボタン
        LB,             // LBボタン
        RB,             // RBボタン
        LT,             // LTボタン
        RT,             // RTボタン
        BACK,           // Backボタン
        START,          // Startボタン
        SELECT,         // Selectボタン
        OPTION,         // Optionボタン
        VIEW,           // Viewボタン
        MENU,           // Menuボタン
        LSTICK_PUSH,     // LStick押し込みボタン
        RSTICK_PUSH,     // RStick押し込みボタン

        DPAD_LEFT,      // DPad 左ボタン
        DPAD_RIGHT,     // DPad 右ボタン
        DPAD_UP,        // DPad 上ボタン
        DPAD_DOWN,      // DPad 下ボタン
        RSTICK_LEFT,    // RStick 左に倒す
        RSTICK_RIGHT,   // RStick 右に倒す
        RSTICK_UP,      // RStick 上に倒す
        RSTICK_DOWN,    // RStick 下に倒す
        LSTICK_LEFT,    // LStick 左に倒す
        LSTICK_RIGHT,   // LStick 右に倒す
        LSTICK_UP,      // LStick 上に倒す
        LSTICK_DOWN,    // LStick 下に倒す

    }

    // ゲームパッドのスティック系の種類のコード
    public enum GAME_PAD_STICK_CODE
    {
        DPAD,      // DPad
        RSTICK,    // RStick
        LSTICK,    // LStick
    }

    private const float _fDead = 0.5f;   // 閾値


    // 判定用変数

    // *Trigger*
    // D-PAD
    private bool _bDPadLeftStay     = false;
    private bool _bDPadRightStay    = false;
    private bool _bDPadUpStay       = false;
    private bool _bDPadDownStay     = false;
    // R-Stick
    private bool _bRStickLeftStay   = false;
    private bool _bRStickRightStay  = false;
    private bool _bRStickUpStay     = false;
    private bool _bRStickDownStay   = false;     
    // L-Stick
    private bool _bLStickLeftStay   = false;
    private bool _bLStickRightStay  = false;
    private bool _bLStickUpStay     = false;
    private bool _bLStickDownStay   = false;

    // *Press*
    // D-PAD
    private bool _bDPadLeftDown     = false;
    private bool _bDPadRightDown    = false;
    private bool _bDPadUpDown       = false;
    private bool _bDPadDownDown     = false;
    // R-Stick
    private bool _bRStickLeftDown   = false;
    private bool _bRStickRightDown  = false;
    private bool _bRStickUpDown     = false;
    private bool _bRStickDownDown   = false;
    // L-Stick
    private bool _bLStickLeftDown   = false;
    private bool _bLStickRightDown  = false;
    private bool _bLStickUpDown     = false;
    private bool _bLStickDownDown   = false;

    // Singleton
    private static CGamePadInputManager _instance;
    public static CGamePadInputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CGamePadInputManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("CGamePadInputManager");
                    _instance = obj.AddComponent<CGamePadInputManager>();
                }
            }
            return _instance;
        }
    }

    // 現在のゲームパッドの状態を更新する
    void Update()
    {
        // DPADとスティックの状態を取得
        float dph = Input.GetAxis("D-PAD-H");
        float dpv = Input.GetAxis("D-PAD-V");
        float lsh = Input.GetAxis("L-Stick-H");
        float lsv = Input.GetAxis("L-Stick-V");
        float rsh = Input.GetAxis("R-Stick-H");
        float rsv = Input.GetAxis("R-Stick-V");

        // 状態初期化
        _bDPadLeftDown = false;
        _bDPadRightDown = false;
        _bDPadUpDown = false;
        _bDPadDownDown = false;
        _bRStickLeftDown = false;
        _bRStickRightDown = false;
        _bRStickUpDown = false;
        _bRStickDownDown = false;
        _bLStickLeftDown = false;
        _bLStickRightDown = false;
        _bLStickUpDown = false;
        _bLStickDownDown = false;


        // DPADの状態を更新
        if (dph > _fDead)   // 右
        {
            if (!_bDPadRightStay)
            {
                _bDPadRightDown = true;
            }
            _bDPadRightStay = true;
        }
        else
        {
            _bDPadRightStay = false;
        }

        if (dph < -_fDead)  // 左
        {
            if (!_bDPadLeftStay)
            {
                _bDPadLeftDown = true;
            }
            _bDPadLeftStay = true;
        }
        else
        {
            _bDPadLeftStay = false;
        }

        if (dpv > _fDead)   // 上
        {
            if (!_bDPadUpStay)
            {
                _bDPadUpDown = true;
            }
            _bDPadUpStay = true;
        }
        else
        {
            _bDPadUpStay = false;
        }

        if (dpv < -_fDead)  // 下
        {
            if (!_bDPadDownStay)
            {
                _bDPadDownDown = true;
            }
            _bDPadDownStay = true;
        }
        else
        {
            _bDPadDownStay = false;
        }
        

        // LStickの状態を更新
        if (lsh > _fDead)   // 右
        {
            if (!_bLStickRightStay)
            {
                _bLStickRightDown = true;
            }
            _bLStickRightStay = true;
        }
        else
        {
            _bLStickRightStay = false;
        }

        if (lsh < -_fDead)  // 左
        {
            if (!_bLStickLeftStay)
            {
                _bLStickLeftDown = true;
            }
            _bLStickLeftStay = true;
        }
        else
        {
            _bLStickLeftStay = false;
        }

        if (lsv > _fDead)   // 上
        {
            if (!_bLStickUpStay)
            {
                _bLStickUpDown = true;
            }
            _bLStickUpStay = true;
        }
        else
        {
            _bLStickUpStay = false;
        }

        if (lsv < -_fDead)  // 下
        {
            if (!_bLStickDownStay)
            {
                _bLStickDownDown = true;
            }
            _bLStickDownStay = true;
        }
        else
        {
            _bLStickDownStay = false;
        }
        

        // RStickの状態を更新
        if (rsh > _fDead)   // 右
        {
            if (!_bRStickRightStay)
            {
                _bRStickRightDown = true;
            }
            _bRStickRightStay = true;
        }
        else
        {
            _bRStickRightStay = false;
        }

        if (rsh < -_fDead)  // 左
        {
            if (!_bRStickLeftStay)
            {
                _bRStickLeftDown = true;
            }
            _bRStickLeftStay = true;
        }
        else
        {
            _bRStickLeftStay = false;
        }

        if (rsv > _fDead)   // 上
        {
            if (!_bRStickUpStay)
            {
                _bRStickUpDown = true;
            }
            _bRStickUpStay = true;
        }
        else
        {
            _bRStickUpStay = false;
        }

        if (rsv < -_fDead)  // 下
        {
            if (!_bRStickDownStay)
            {
                _bRStickDownDown = true;
            }
            _bRStickDownStay = true;
        }
        else
        {
            _bRStickDownStay = false;
        }

    }


    // Trigger ゲームパッドのボタンが押されたかどうか取得（押した瞬間のみ検知）
    // 引数： code どのボタンか
    // 戻り値：true 押された
    public static bool GetButtonDown(GAME_PAD_CODE code)
    {
        switch (code)
        {
            case GAME_PAD_CODE.A:
                return Input.GetKeyDown("joystick button 0");

            case GAME_PAD_CODE.B:
                return Input.GetKeyDown("joystick button 1");

            case GAME_PAD_CODE.X:
                return Input.GetKeyDown("joystick button 2");

            case GAME_PAD_CODE.Y:
                return Input.GetKeyDown("joystick button 3");

            case GAME_PAD_CODE.LB:
                return Input.GetKeyDown("joystick button 4");

            case GAME_PAD_CODE.RB:
                return Input.GetKeyDown("joystick button 5");

            case GAME_PAD_CODE.BACK:
            case GAME_PAD_CODE.SELECT:
            case GAME_PAD_CODE.VIEW:
                return Input.GetKeyDown("joystick button 6");

            case GAME_PAD_CODE.START:
            case GAME_PAD_CODE.OPTION:
            case GAME_PAD_CODE.MENU:
                return Input.GetKeyDown("joystick button 7");

            case GAME_PAD_CODE.LSTICK_PUSH:
                return Input.GetKeyDown("joystick button 8");

            case GAME_PAD_CODE.RSTICK_PUSH:
                return Input.GetKeyDown("joystick button 9");

            // D-Pad
            case GAME_PAD_CODE.DPAD_LEFT:
                return Instance._bDPadLeftDown;
            case GAME_PAD_CODE.DPAD_RIGHT:
                return Instance._bDPadRightDown;
            case GAME_PAD_CODE.DPAD_UP:
                return Instance._bDPadUpDown;
            case GAME_PAD_CODE.DPAD_DOWN:
                return Instance._bDPadDownDown;

            // R-Stick
            case GAME_PAD_CODE.RSTICK_LEFT:
                return Instance._bRStickLeftDown;
            case GAME_PAD_CODE.RSTICK_RIGHT:
                return Instance._bRStickRightDown;
            case GAME_PAD_CODE.RSTICK_UP:
                return Instance._bRStickUpDown;
            case GAME_PAD_CODE.RSTICK_DOWN:
                return Instance._bRStickDownDown;
                
            // L-Stick
            case GAME_PAD_CODE.LSTICK_LEFT:
                return Instance._bLStickLeftDown;
            case GAME_PAD_CODE.LSTICK_RIGHT:
                return Instance._bLStickRightDown;
            case GAME_PAD_CODE.LSTICK_UP:
                return Instance._bLStickUpDown;
            case GAME_PAD_CODE.LSTICK_DOWN:
                return Instance._bLStickDownDown;

            default:
                return false;
        }
    }

    // Press ゲームパッドのボタンが押されてるかどうか取得（押している間true）
    // 引数： code どのボタンか
    // 戻り値：true 押されている　false 押されてない
    public static bool GetButton(GAME_PAD_CODE code)
    {
        switch (code)
        {
            case GAME_PAD_CODE.A:
                return Input.GetKey("joystick button 0");

            case GAME_PAD_CODE.B:
                return Input.GetKey("joystick button 1");

            case GAME_PAD_CODE.X:
                return Input.GetKey("joystick button 2");

            case GAME_PAD_CODE.Y:
                return Input.GetKey("joystick button 3");

            case GAME_PAD_CODE.LB:
                return Input.GetKey("joystick button 4");

            case GAME_PAD_CODE.RB:
                return Input.GetKey("joystick button 5");

            case GAME_PAD_CODE.BACK:
            case GAME_PAD_CODE.SELECT:
            case GAME_PAD_CODE.VIEW:
                return Input.GetKey("joystick button 6");

            case GAME_PAD_CODE.START:
            case GAME_PAD_CODE.OPTION:
            case GAME_PAD_CODE.MENU:
                return Input.GetKey("joystick button 7");

            case GAME_PAD_CODE.LSTICK_PUSH:
                return Input.GetKey("joystick button 8");

            case GAME_PAD_CODE.RSTICK_PUSH:
                return Input.GetKey("joystick button 9");

            // D-Pad
            case GAME_PAD_CODE.DPAD_LEFT:
                return Instance._bDPadLeftStay;
            case GAME_PAD_CODE.DPAD_RIGHT:
                return Instance._bDPadRightStay;
            case GAME_PAD_CODE.DPAD_UP:
                return Instance._bDPadUpStay;
            case GAME_PAD_CODE.DPAD_DOWN:
                return Instance._bDPadDownStay;

            // R-Stick
            case GAME_PAD_CODE.RSTICK_LEFT:
                return Instance._bRStickLeftStay;
            case GAME_PAD_CODE.RSTICK_RIGHT:
                return Instance._bRStickRightStay;
            case GAME_PAD_CODE.RSTICK_UP:
                return Instance._bRStickUpStay;
            case GAME_PAD_CODE.RSTICK_DOWN:
                return Instance._bRStickDownStay;
                
            // L-Stick
            case GAME_PAD_CODE.LSTICK_LEFT:
                return Instance._bLStickLeftStay;
            case GAME_PAD_CODE.LSTICK_RIGHT:
                return Instance._bLStickRightStay;
            case GAME_PAD_CODE.LSTICK_UP:
                return Instance._bLStickUpStay;
            case GAME_PAD_CODE.LSTICK_DOWN:
                return Instance._bLStickDownStay;

            default:
                return false;
        }
    }

    // 現在のスティックの状態を取得
    // 引数： code どのスティックか
    // 戻り値： 倒している方向
    public static Vector2 GetStickStatus(GAME_PAD_STICK_CODE code)
    {
        switch (code)
        {
            // D-Pad
            case GAME_PAD_STICK_CODE.DPAD:
                return new Vector2(Input.GetAxis("D-PAD-H"), Input.GetAxis("D-PAD-V"));

            case GAME_PAD_STICK_CODE.RSTICK:
                return new Vector2(Input.GetAxis("L-Stick-H"), Input.GetAxis("L-Stick-V"));

            // L-Stick
            case GAME_PAD_STICK_CODE.LSTICK:
                return new Vector2(Input.GetAxis("R-Stick-H"), Input.GetAxis("R-Stick-V"));

            default:
                return Vector2.zero;
        }
    }

}
