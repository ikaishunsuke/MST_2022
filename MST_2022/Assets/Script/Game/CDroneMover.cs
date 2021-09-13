/*==============================================================================
    [CDroneMove.cs]
    ・ドローン操作
--------------------------------------------------------------------------------
    2021.09.13 @Fujiwara Aiko
================================================================================
    History
        2021.09.13 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CDroneMover : MonoBehaviour
{

    Rigidbody _rigidbody;   // 自身のRigidBody
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); // 参照を取得
    }
    

    void Update()
    {
        // 入力を検知
        Vector2 dir = Vector2.zero; // 移動方向

        if (Input.GetKey(KeyCode.D)) // 右
        {
            dir += new Vector2(1.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.A)) // 左
        {
            dir += new Vector2(-1.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.W)) // 前
        {
            dir += new Vector2(0.0f, 1.0f);
        }

        if (Input.GetKey(KeyCode.S)) // 後ろ
        {
            dir += new Vector2(0.0f, -1.0f);
        }

        // 移動
        Move(dir);

    }

    // dir方向に移動する
    // 引数： dir 移動方向
    public void Move(Vector2 dir)
    {
        Vector3 force = transform.right * dir.x + transform.forward * dir.y;
        _rigidbody.AddForce(force, ForceMode.Force);
    }

}
