using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraControl : MonoBehaviour
{
    private GameObject _gMainCamera;      // メインカメラ格納用
    private GameObject _gSubCamera;       // サブカメラ格納用 

    void Start()
    {
        // メインカメラとサブカメラをそれぞれ取得
        _gMainCamera = GameObject.Find("MainCamera");
        _gSubCamera = GameObject.Find("SubCamera");

        // サブカメラを非アクティブにする
        _gSubCamera.SetActive(false);
    }

    void Update()
    {
        // スペースキーが押されている間サブカメラをアクティブにする（暫定）
        if (Input.GetKey("space"))
        {
            // サブカメラをアクティブに設定
            _gMainCamera.SetActive(false);
            _gSubCamera.SetActive(true);
        }
        else
        {
            // メインカメラをアクティブに設定
            _gSubCamera.SetActive(false);
            _gMainCamera.SetActive(true);
        }
    }
}
