/*==============================================================================
    [CPlayerMover.cs]
    ・プレイヤーの動き部分
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/


using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CPlayerMover : MonoBehaviour
{

    [SerializeField] protected float _fWalkSpeed = 5.0f;     // 歩きのスピード
    [SerializeField] protected float _fRotateSpeed = 5.0f;   // 振り向きスピード

    protected Vector2 _vForward;      // 前方向
    protected Rigidbody _rb;          // プレイヤーのRigidbody


    protected virtual void Start()
    {
        // キャッシュ
        _rb = GetComponent<Rigidbody>();
        _vForward = new Vector2(transform.forward.x, transform.forward.z);
    }
    
    // Walk 　歩く
    // 引数：dir 向かう方向
    public virtual void Walk(Vector2 dir)
    {
        _rb.velocity = new Vector3(dir.x, 0.0f, dir.y).normalized * _fWalkSpeed;

        Turn(dir);
    }


    // Turn 回転（移動方向を向く）
    protected virtual void Turn(Vector2 dir)
    {

        if (dir.x < -0.1f || dir.y < -0.1f ||
            dir.x > 0.1f || dir.y > 0.1f)
        {// 方向が入力されていたら
            _vForward = dir;    // 向きを変更する
        }

        // 向きを少しずつ変える
        Quaternion nextRotation = Quaternion.LookRotation(new Vector3(_vForward.x, 0.0f, _vForward.y), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, nextRotation, Time.deltaTime * _fRotateSpeed);
    }

}
