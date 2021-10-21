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
using UnityEngine.Events;

public class CPlayerPickUpState : MonoBehaviour, IPlayerState
{
    [SerializeField] CPlayerMover _cPlayerMover = null;     // プレイヤーを動かす用
    private CWeightObject _gPickUpObject;   // 持ち上げているオブジェクト
    private CWeightObject _gForwardObject;    // 目の前にあるオブジェクト

    [HideInInspector] public UnityEvent _ueChangeCanAction = new UnityEvent();       // 今できるアクションの変化を通知


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
            _gPickUpObject._ueChangeCanPut.AddListener(ChangeCanAction);
            _ueChangeCanAction.Invoke();
        }
        else if (_gPickUpObject != null)
        {// 置く
            _gPickUpObject.Placed();
            _gPickUpObject._ueChangeCanPut.RemoveListener(ChangeCanAction);
            _gPickUpObject = null;
            _gForwardObject = null;
            _ueChangeCanAction.Invoke();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        CWeightObject obj = other.GetComponent<CWeightObject>();
        if(obj != null)
        {
            _gForwardObject = obj;
            _ueChangeCanAction.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CWeightObject obj = other.GetComponent<CWeightObject>();
        if (obj == _gForwardObject)
        {
            _gForwardObject = null;
            _ueChangeCanAction.Invoke();
        }
    }

    // 今できるアクションが変化
    void ChangeCanAction()
    {
        _ueChangeCanAction.Invoke();
    }

    // CanPickUp 拾えるオブジェクトがあるかどうかを取得
    public bool CanPickUp()
    {

        if (_gForwardObject != null &&
            _gPickUpObject == null)
        {
            return true;
        }
        return false;
    }

    // CanPutSpace スペースに置けるかどうかを取得
    public bool CanPutSpace()
    {
        if (_gPickUpObject != null)
        {
            return _gPickUpObject.CanPutSpace();
        }
        return false;
    }

    // CanDiscard 捨てられるかどうかを取得
    public bool CanDiscard()
    {
        if (_gPickUpObject != null)
        {
            return !_gPickUpObject.CanPutSpace();
        }
        return false;
    }

}
