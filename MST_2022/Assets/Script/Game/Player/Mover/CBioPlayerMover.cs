/*==============================================================================
    [CBioPlayerMover.cs]
    ・バイオカメラ用プレイヤー
    カメラ移動無し。
--------------------------------------------------------------------------------
    2021.10.17 @Fujiwara Aiko
================================================================================
    History
        2021.10.17 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/


using UnityEngine;

public class CBioPlayerMover : CPlayerMover
{
    private Transform _tCamera = null;          // プレイヤーのカメラ（進行方向になる）

    private Vector2 _vBeforeDir = new Vector2(0.0f, 0.0f);          // １フレーム前の入力方向
    private Vector3 _vBeforeDirection = new Vector3(0.0f, 0.0f, 0.0f);    // １フレーム前の移動方向

    // Walk 　歩く
    // 引数：dir 向かう方向
    public override void Walk(Vector2 dir)
    {
        Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
        if (_vBeforeDir == dir)
        {// 入力が変わらなかったら移動方向は変動しない
            direction = _vBeforeDirection;
        }
        else
        {
            Vector3 forward = new Vector3(_tCamera.forward.x, 0.0f, _tCamera.forward.z);
            forward.Normalize();
            Vector3 right = new Vector3(_tCamera.right.x, 0.0f, _tCamera.right.z);
            forward.Normalize();
            direction = forward * dir.y + right * dir.x;
            direction.Normalize();
            // 歩く
            _rb.velocity = direction * _fWalkSpeed;
        }


        _rb.velocity = direction * _fWalkSpeed;


        Turn(new Vector2(direction.x, direction.z));

        // 入力を保存
        _vBeforeDir = dir;
        // 移動方向を保存
        _vBeforeDirection = direction;
    }


    // 現在のカメラをセット
    public void Set_tCamera(Transform camera)
    {
        _tCamera = camera;
    }

}