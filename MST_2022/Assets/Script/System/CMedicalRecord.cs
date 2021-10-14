/*==============================================================================
    [CMedicalRecord.cs]
    ・カルテ画面の表示
--------------------------------------------------------------------------------
    2021.10.11 @ MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            スクリプト追加
            
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

    // PlayerPrefsの関係上boolにしたかったけどintで我慢します。
    // int = 0の時がfalse,int = 1の時がtrueとする。
    public static int[] _iHint = new int[8];


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
        _gPanel = GameObject.Find("Panel_MedicalRecord");
        _gPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
                    }
                }
            }
            else
            {
                _gPanel.SetActive(false);
                _gIcon.SetActive(true);
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
