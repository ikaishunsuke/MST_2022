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

    CPickedUpObject _gPickUpObject;   // 持ち上げる

    CPickedUpObject _gForwardObject;    // 目の前にあるオブジェクト

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 direction)
    {
        _cPlayerMover.Walk(direction);
    }

    public void Action()
    {
        Debug.Log(_gForwardObject);
        Debug.Log(_gPickUpObject);
        if (_gPickUpObject != null)
        {
            _gPickUpObject.transform.parent = null;
            _gPickUpObject.Put();
            _gPickUpObject = null;

        }
        else if(_gForwardObject != null &&
            _gPickUpObject == null)
        {
            _gPickUpObject = _gForwardObject;
            _gPickUpObject.PickedUp(transform.parent, transform.position + Vector3.up);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        CPickedUpObject obj = other.GetComponent<CPickedUpObject>();
        if(obj != null)
        {
            _gForwardObject = obj;
        }
        Debug.Log(other + "enter");
    }

    void OnTriggerExit(Collider other)
    {
        CPickedUpObject obj = other.GetComponent<CPickedUpObject>();
        if (obj == _gForwardObject)
        {
            _gForwardObject = null;
        }
        Debug.Log(other + "exit");
    }


}
