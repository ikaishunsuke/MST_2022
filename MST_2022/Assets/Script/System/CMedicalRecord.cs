/*==============================================================================
    [CMedicalRecord.cs]
    ・カルテ画面の表示
--------------------------------------------------------------------------------
    2021.10.11 @ MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            セーブロード機能もあるよ。(PlayerPrefs）
            あ、ちなみにfor文の最大値を2にしてるけどそれは今ステージ１の
            プロトだけだからって意味で
            まぁ全ステージ完成した暁には_iHint.Lengthにします。
            っていう自分用メモ残しとく
            
/*============================================================================*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMedicalRecord : MonoBehaviour
{
    private GameObject _gIcon;
    private GameObject _gPanel;
    public static GameObject _gNewMark; // まだカルテ詳細をみていないやつがある場合は必ずつく。
    public GameObject[] _gHint;
    private GameObject _gPausePanel;

    /* PlayerPrefsの関係上boolにしたかったけどintで我慢します。
       int = 0の時がfalse,int = 1の時がtrueとする。   */
    public static int[] _iHint = new int[8];

    private GameObject mainCamera;      // メインカメラ格納用
    private GameObject subCamera;       // サブカメラ格納用 

    public GameObject _gCursor;         // カーソル画像
    private Vector3[] _iCursorPosition = new Vector3[2];
    private int _iCursorCnt;

    // Start is called before the first frame update
    void Start()
    {
        // データのロード
        Load_MedicalRecord();

        //for (int index = 0; index < _iHint.Length; index++)
        for (int index = 0; index < 2; index++)
        {
            // カルテデータが開示されていない場合
            if (_iHint[index] == 0)
            {
                _gHint[index * 2].SetActive(true);
                _gHint[1 + index * 2].SetActive(false);
            }
            if (_iHint[index] == 1)
            {
                _gHint[index * 2].SetActive(false);
                _gHint[1 + index * 2].SetActive(true);
            }
        }

        _gIcon = GameObject.Find("MedicalRecord_Icon");

        _gNewMark = GameObject.Find("NewMark");
        _gNewMark.SetActive(false);
        _gCursor = GameObject.Find("Cursor");
        _gCursor.SetActive(false);

        _gPanel = GameObject.Find("Panel_MedicalRecord");
        _gPanel.SetActive(false);

        _gPausePanel = GameObject.Find("Panel_Pause");

        _iCursorPosition[0] = _gCursor.transform.position;

        for (int index = 1; index < _iCursorPosition.Length; index++)
        {
            Vector3 vec = _iCursorPosition[index - 1];
            vec.x += 20.0f;
            _iCursorPosition[index] = vec;
        }
        _iCursorCnt = 0;

        //メインカメラとサブカメラをそれぞれ取得
        mainCamera = GameObject.Find("MainCamera");
        subCamera = GameObject.Find("MedicalRecord_Camera");

        //サブカメラを非アクティブにする
        subCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gPausePanel.activeSelf)
        {
            if (CInputManager.GetButtonDown(INPUT_CODE.X))
            {
                if (!_gPanel.activeSelf)
                {
                    _gNewMark.SetActive(false);
                    _gIcon.SetActive(false);
                    _gPanel.SetActive(true);
                    
                    // カルテデータの更新
                    //for (int index = 0; index < _iHint.Length; index++)
                    for (int index = 0; index < 2; index++)
                    {
                        // カルテデータが開示されていない場合
                        if (_iHint[index] == 0)
                        {
                            _gHint[index * 2].SetActive(true);
                            _gHint[1 + index * 2].SetActive(false);
                        }
                        // カルテデータが開示されている場合
                        if (_iHint[index] == 1)
                        {
                            _gHint[index * 2].SetActive(false);
                            _gHint[1 + index * 2].SetActive(true);

                            _gCursor.SetActive(true);
                            _gCursor.transform.position = _iCursorPosition[0];
                        }
                    }
                }
                else
                {
                    _gPanel.SetActive(false);
                    _gIcon.SetActive(true);
                }

                // カルテが表示されている場合
                if (_gPanel.activeSelf)
                {
                    //サブカメラをアクティブに設定
                    mainCamera.SetActive(false);
                    subCamera.SetActive(true);
                }
                else
                {
                    //メインカメラをアクティブに設定
                    subCamera.SetActive(false);
                    mainCamera.SetActive(true);
                }
            }
            if (_gPanel)   // パネルが表示されている場合
            {
                if (CInputManager.GetButtonDown(INPUT_CODE.RIGHT))
                {
                    for (int index = 0; index < 2; index++)
                    {
                        // カルテデータが開示されていない場合
                        if (_iHint[index] == 0)
                        {
                        }
                        // カルテデータが開示されている場合
                        if (_iHint[index] == 1)
                        {
                            _iCursorCnt = 1;
                            _gCursor.transform.position = _iCursorPosition[_iCursorCnt];
                        }
                    }
                }
                if (CInputManager.GetButtonDown(INPUT_CODE.LEFT))
                {
                    for (int index = 0; index < 2; index++)
                    {
                        // カルテデータが開示されていない場合
                        if (_iHint[index] == 0)
                        {
                        }
                        // カルテデータが開示されている場合
                        if (_iHint[index] == 1)
                        {
                            _iCursorCnt = 0;
                            _gCursor.transform.position = _iCursorPosition[_iCursorCnt];
                        }
                    }
                }
            }
        }
    
    }

    // 該当するカルテデータ対象オブジェクトにくっつけとくやつ
    public static void OnHint_1()
    {
        if (_iHint[0] == 0)
        {
            _iHint[0] = 1;
            _gNewMark.SetActive(true);  // カルテデータが追加されたよ！マークを出す
        }
        // カルテに登録されている場合は何もしない。
    }
    public static void OnHint_2()
    {
        if (_iHint[1] == 0)
        {
            _iHint[1] = 1;
            _gNewMark.SetActive(true);  // カルテデータが追加されたよ！マークを出す
        }
        // カルテに登録されている場合は何もしない。
    }


    //====================================================== セーブ&ロード（カルテデータ（ヒント）取得状況）
    public void Save_MedicalRecord()
    {
        // カルテデータの取得状況のセーブ
        for (int index = 0; index < _iHint.Length; index++)
        {
            PlayerPrefs.SetInt($"HINT{index}", _iHint[index]);
        }

        Debug.Log("カルテデータのセーブ完了");

    }
    public void Load_MedicalRecord()
    {
        // カルテデータの取得状況のロード
        for (int index = 0; index < _iHint.Length; index++)
        {
            int iHint = PlayerPrefs.GetInt($"HINT{index}", 0);
            _iHint[index] = iHint;
        }
        Debug.Log("カルテデータのロード完了");
    }
}
