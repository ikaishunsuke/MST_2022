/*==============================================================================
    [CObjectData.cs]
    ◆カルテに登録できるオブジェクトのなんやかんや
--------------------------------------------------------------------------------
    2021.10.11 @Author MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            カルテに登録とか、まぁカルテ画面以外でカルテに関わる
            なんやかんやはこっちに書いてます。カルテの内部的なアレは
            CMedicalRecord.csにあるよ
        
/*============================================================================*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CObjectData : MonoBehaviour
{
    public GameObject _gPanel;
    public GameObject _gIconCanActive;
    private GameObject _gObj;
    private bool _bIsRecord;  // １度でも確認している場合はtrue
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 vec = new Vector3(960.0f, 540.0f, 0.0f);

        _gObj = Instantiate(_gIconCanActive, this.transform.position, Quaternion.identity);
        _gObj.transform.SetParent(_gPanel.transform);
        _gObj.transform.position = vec;
        _gObj.SetActive(false);

        _bIsRecord = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        // プレイヤータグがついているオブジェクトの場合
        if (other.gameObject.tag == "Player")
        {
            if (!_bIsRecord)
            {
                // もしアイコンが出ていない場合はtrueにして表示
                if (!_gObj.activeSelf)
                {
                    _gObj.SetActive(true);
                }

                if (CInputManager.GetButtonDown(INPUT_CODE.SELECT))
                {
                    switch (this.name)
                    {
                        case "MedicalRecord_1":
                            CMedicalRecord.OnHint_1();
                            _bIsRecord = true;
                            _gObj.SetActive(false);

                            break;

                        case "MedicalRecord_2":
                            CMedicalRecord.OnHint_2();
                            _bIsRecord = true;
                            _gObj.SetActive(false);
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }


    private void OnTriggerExit()
    {
        // もしアイコンが表示されている場合は表示させないようにする
        if (_gObj.activeSelf)
        {
            _gObj.SetActive(false);
        }
    }
}
