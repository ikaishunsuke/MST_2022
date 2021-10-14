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

[RequireComponent(typeof(BoxCollider))]
public class CPickedUpObject : MonoBehaviour
{
    
    [SerializeField] private bool _isCorrect = false;    // 当たりオブジェクトかどうか
    private BoxCollider _collider = null;          // 持つ判定用

    private Vector3 _vOffsetPos;                    // 元々あった場所
    private Quaternion _vOffsetRot;                 // 元々の角度
    private Vector3 _vOffsetColliderSize;           // 元々のCollider大きさ

    private CPutSpace _cTouchingPutSpace = null;    // 現在触れている置き場所
    private bool _isPlacedSpace = false;            // 置き場所に置かれているか


    private void Start()
    {
        // 現在位置を保存
        _vOffsetPos = transform.position;
        _vOffsetRot = transform.rotation;

        _collider = GetComponent<BoxCollider>();
        _vOffsetColliderSize = _collider.size;

        gameObject.AddComponent<BoxCollider>().isTrigger = true;

    }

    // PickedUp 持ち上げられる
    // 引数： Parent 持ち上げられた時の親オブジェクト、position 持ち上げられた時の位置
    public void PickedUp(Transform parent, Vector3 position)
    {

        // 自身を子オブジェクトにし、位置を変更
        transform.parent = parent;
        transform.localRotation = Quaternion.identity;
        transform.position = position + transform.forward * transform.localScale.x * 0.5f + transform.up * transform.localScale.y * 0.5f;

        // 置きスペースに置かれていたら、スペースに持ち上げられたことを伝える
        if(_isPlacedSpace)
        {
            _cTouchingPutSpace.RemoveObject(this);
            _isPlacedSpace = false;
            _collider.size = _vOffsetColliderSize;  // Collisionの大きさを元に戻す
        }

    }

    // Placed  置かれる
    public void Placed()
    {

        if(_cTouchingPutSpace != null)
        {
            // スペースに置く
            Transform spacePos = _cTouchingPutSpace.PlacedObject(this);
            if(spacePos == null)
            {
                // 置くスペースが無かったので元の場所に戻る
                transform.position = _vOffsetPos;
                transform.rotation = _vOffsetRot;
                return;
            }
            else
            {
                // スペースに置かれる
                //transform.parent = spacePos;
                transform.localRotation = Quaternion.identity;
                transform.position = spacePos.position + transform.up * transform.lossyScale.y;

                _collider.size = new Vector3(_vOffsetColliderSize.x * (8.0f / transform.lossyScale.x), _vOffsetColliderSize.y, _vOffsetColliderSize.z * (20.0f / transform.lossyScale.x));
                _isPlacedSpace = true;
            }

        }
        else
        {
            // 置ける位置じゃなかったら元の場所に戻る
            transform.position = _vOffsetPos;
            transform.rotation = _vOffsetRot;
        }

    }

    // 当たりのオブジェクトかどうか (Getter)
    public bool Get_isCorrect()
    {
        return _isCorrect;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        CPutSpace space = other.GetComponent<CPutSpace>();
        if (space != null)
        {
            // スペースにおける状態に
            _cTouchingPutSpace = space;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        CPutSpace space = other.GetComponent<CPutSpace>();
        if (space != null)
        {
            // スペースにおけない
            _cTouchingPutSpace = null;
        }
    }

}
