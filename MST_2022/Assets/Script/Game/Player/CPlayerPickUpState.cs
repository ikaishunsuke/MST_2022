/*==============================================================================
    [CPlayerPickUpState.cs]
    ・持ち上げたり置いたりできるプレイヤー状態
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/


using UnityEngine;

public class CPlayerPickUpState : MonoBehaviour, IPlayerState
{
    [SerializeField] CPlayerMover _cPlayerMover = null;     // プレイヤーを動かす用

    private CPickedUpObject _gPickUpObject;   // 持ち上げているオブジェクト

    private CPickedUpObject _gForwardObject;    // 目の前にあるオブジェクト


    // Move 動く
    // 引数： direction 方向
    public void Move(Vector2 direction)
    {
        _cPlayerMover.Walk(direction);
    }
    
    // Action アクション ～持ち上げる置く～
    public void Action()
    {
        // 現在の状態によって持ち上げるor置くor何もしない

        if(_gForwardObject != null &&
            _gPickUpObject == null)
        {// 持ち上げる
            _gPickUpObject = _gForwardObject;
            _gPickUpObject.PickedUp(transform.parent, transform.position);
        }
        else if (_gPickUpObject != null)
        {// 置く
            _gPickUpObject.transform.parent = null;
            _gPickUpObject.Put();
            _gPickUpObject = null;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        CPickedUpObject obj = other.GetComponent<CPickedUpObject>();
        if(obj != null)
        {
            _gForwardObject = obj;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CPickedUpObject obj = other.GetComponent<CPickedUpObject>();
        if (obj == _gForwardObject)
        {
            _gForwardObject = null;
        }
    }

}
