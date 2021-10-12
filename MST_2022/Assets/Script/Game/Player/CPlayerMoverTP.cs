/*==============================================================================
    [CPlayerMoverTP.cs]
    ・前方向に進むプレイヤー
--------------------------------------------------------------------------------
    2021.10.12 @Fujiwara Aiko
================================================================================
    History
        2021.10.12 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/


using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CPlayerMoverTP : CPlayerMover
{

    protected override void Start()
    {
        // キャッシュ
        _rb = GetComponent<Rigidbody>();
        _vForward = new Vector2(transform.forward.x, transform.forward.z);
    }

    // Walk 　歩く
    // 引数：dir 向かう方向
    public override void Walk(Vector2 dir)
    {
        // 前に進む
        _rb.velocity = transform.forward * dir.y * _fWalkSpeed;

        // 左右に回転する
        if (dir.x < -0.1f || dir.x > 0.1f)
        {
            Debug.Log(dir.x);
            _rb.MoveRotation(transform.rotation * Quaternion.AngleAxis(Time.deltaTime * _fRotateSpeed * dir.x, Vector3.up));
            
        }
        else
        {
            _rb.angularVelocity = Vector3.zero;
        }
    }

}
