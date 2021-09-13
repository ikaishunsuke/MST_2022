using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollowMainCamera : MonoBehaviour
{
    private GameObject _gMainCamera;              // メインカメラ情報格納用
    private GameObject _gPlayer;                  // プレイヤー情報格納用
    private Vector3 _vOffset;                     // 相対距離取得用
    public float _fCameraRotateSpeed = 2.0f;      // 回転の速さ

    void Start()
    {
        // メインカメラの取得
        _gMainCamera = GameObject.Find("MainCamera");

        // プレイヤーの情報を取得
        this._gPlayer = GameObject.Find("Player");

        // MainCamera(自分自身)とPlayerとの相対距離を求める
        _vOffset = transform.position - _gPlayer.transform.position;

    }

    void Update()
    {
        // 新しい座標の値を代入する
        transform.position = _gPlayer.transform.position + _vOffset;

        RotateCamera();
    }

    private void RotateCamera()
    {
        // Vector3でX,Y方向の回転の度合いを定義
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * _fCameraRotateSpeed, 
                                    Input.GetAxis("Mouse Y") * _fCameraRotateSpeed, 0);

        // メインカメラを回転させる
        _gMainCamera.transform.RotateAround(_gPlayer.transform.position, Vector3.up, angle.x);
        _gMainCamera.transform.RotateAround(_gPlayer.transform.position, transform.right, angle.y);
    }
}
