using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollowSubCamera : MonoBehaviour
{
    private GameObject _gSubCamera;              // メインカメラ情報格納用
    private GameObject _gPlayer;                  // プレイヤー情報格納用
    public float _fCameraRotateSpeed = 2.0f;      // 回転の速さ

    void Start()
    {
        // サブカメラの取得
        _gSubCamera = GameObject.Find("SubCamera");

        // プレイヤーの情報を取得
        this._gPlayer = GameObject.Find("Player");
    }

    void Update()
    {
        // 新しい座標の値を代入する
        transform.position = _gPlayer.transform.position;

        RotateCamera();
    }

    private void RotateCamera()
    {
        // Vector3でX,Y方向の回転の度合いを定義
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * _fCameraRotateSpeed,
                                    Input.GetAxis("Mouse Y") * _fCameraRotateSpeed, 0);

        // サブカメラを回転させる
        _gSubCamera.transform.RotateAround(_gPlayer.transform.position, Vector3.up, angle.x);
        _gSubCamera.transform.RotateAround(_gPlayer.transform.position, transform.right, angle.y);
    }
}