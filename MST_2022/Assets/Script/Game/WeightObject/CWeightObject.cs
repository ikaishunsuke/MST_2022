/*==============================================================================
    [CWeightObject.cs]
    ・天秤の重り
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class CWeightObject : MonoBehaviour
{
    
    [SerializeField] private bool _isCorrect = false;    // 当たりオブジェクトかどうか
    private BoxCollider _collider = null;          // 持つ判定用

    private Vector3 _vOffsetPos;                    // 元々あった場所
    private Quaternion _vOffsetRot;                 // 元々の角度
    private Vector3 _vOffsetColliderSize;           // 元々のCollider大きさ

    private CPutSpace _cTouchingPutSpace = null;    // 現在触れている置き場所
    private bool _isPlacedSpace = false;            // 置き場所に置かれているか
    
    [HideInInspector] public UnityEvent _ueChangeCanPut = new UnityEvent();    // 置けるかどうか変化したときに呼び出される


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
        // 置きスペースに置かれていたら、スペースに持ち上げられたことを伝える
        if (_isPlacedSpace)
        {
            _cTouchingPutSpace.RemoveObject(this);
            _cTouchingPutSpace = null;
            _isPlacedSpace = false;
            _collider.size = _vOffsetColliderSize;  // Collisionの大きさを元に戻す

            transform.parent = null;
        }

        // 自身を子オブジェクトにし、位置を変更
        transform.parent = parent;
        transform.localRotation = Quaternion.identity;
        transform.position = position + transform.forward * transform.localScale.x * 0.5f + transform.up * transform.localScale.y * 0.5f;

    }

    // Placed  置かれる
    public void Placed()
    {

        if (_cTouchingPutSpace != null)
        {
            // スペースに置く
            Transform spacePos = _cTouchingPutSpace.PlacedObject(this);
            if (spacePos != null)
            {
                // スペースに置かれる
                transform.localRotation = Quaternion.identity;
                transform.position = spacePos.position + transform.up * transform.lossyScale.y * 0.5f;

                _collider.size = new Vector3(_vOffsetColliderSize.x * (8.0f / transform.lossyScale.x), _vOffsetColliderSize.y, _vOffsetColliderSize.z * (20.0f / transform.lossyScale.x));
                _isPlacedSpace = true;

                transform.parent = spacePos;
            }

        }
        else
        {
            // 置ける位置じゃなかったら元の場所に戻る
            StartCoroutine(DisappearAnim());// 消えるアニメーション開始

        }

    }
    
    // DisappearAnim 消えるアニメーション
    private IEnumerator DisappearAnim()
    {

        // オブジェクトのRendererを取ってきて透明度を下げる
        Renderer renderer = GetComponent<Renderer>();

        for (float time = 0.0f; time < 1.0f; time += Time.deltaTime)
        {
            Color color = renderer.material.color;
            color.a = (1.0f - time) * 2.0f;
            if (color.a > 1.0f) color.a = 1.0f;
            renderer.material.color = color;
            renderer.material.SetFloat("_Threshold", time);
            yield return null;
        }

        transform.parent = null;
        transform.position = _vOffsetPos;
        transform.rotation = _vOffsetRot;

        _cTouchingPutSpace = null;

        // 再出現する
        for (float time = 0.0f; time < 1.0f; time += Time.deltaTime)
        {
            Color color = renderer.material.color;
            color.a = time * 2.0f;
            if (color.a > 1.0f) color.a = 1.0f;
            renderer.material.color = color;
            renderer.material.SetFloat("_Threshold", 1.0f - time);
            yield return null;
        }

        // 初期状態に戻す
        Color c = renderer.material.color;
        c.a = 1.0f;
        renderer.material.color = c;
        renderer.material.SetFloat("_Threshold", 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        CPutSpace space = other.GetComponent<CPutSpace>();
        if (space != null && !_isPlacedSpace)
        {
            if (space.IsThereSpace())   // 置くスペースがあるか確認
            {
                // スペースにおける状態に
                _cTouchingPutSpace = space;
                _ueChangeCanPut.Invoke();
            }
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        CPutSpace space = other.GetComponent<CPutSpace>();
        if (space != null)
        {
            // スペースにおけない
            _cTouchingPutSpace = null;
            _ueChangeCanPut.Invoke();
        }
    }

    // CanPutSpace スペースに置けるかどうか
    public bool CanPutSpace()
    {
        if(_cTouchingPutSpace != null)
        {
            return true;
        }
        return false;
    }

    // 当たりのオブジェクトかどうか (Getter)
    public bool Get_isCorrect()
    {
        return _isCorrect;
    }

}
