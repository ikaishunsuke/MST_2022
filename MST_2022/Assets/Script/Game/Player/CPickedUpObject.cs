/*==============================================================================
    [CPickedUpObject.cs]
    ・プレイヤーが拾えるオブジェクト
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/


using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CPickedUpObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb = null;          // 自身のRigidbody
    [SerializeField] private Collider _collider = null;     // 自身のCollider  

    [SerializeField] private Vector3 _vSize = new Vector3(1.0f, 1.0f, 1.0f);    // オブジェクトの大きさ

    // PickedUp 持ち上げられる
    // 引数： Parent 持ち上げられた時の親オブジェクト、position 持ち上げられた時の位置
    public void PickedUp(Transform parent, Vector3 position)
    {
        _rb.isKinematic = true;     // 重力をかからなくする
        _collider.enabled = false;  // 判定を一時的にオフにする

        // 自身を子オブジェクトにし、位置を変更
        transform.parent = parent;
        transform.localRotation = Quaternion.identity;
        transform.position = position + transform.forward * _vSize.x * 0.5f + transform.up * _vSize.y * 0.5f;

    }

    // Placed  置かれる
    public void Put()
    {
        // Rigidbodyと判定の状態を元に戻す
        _rb.isKinematic = false;
        _collider.enabled = true;
    }

}
