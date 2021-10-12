/*==============================================================================
    [CTargetFollowCamera.cs]
    ・ターゲットから一定間隔あけて追従するカメラ
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/


using UnityEngine;

public class CTargetFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _tTarget = null;    // 追いかける対象

    [SerializeField] private bool _isApplyCurrentOffset = false;
    [SerializeField] private Vector3 _vOffsetPosition = new Vector3(0.0f, 0.0f, 0.0f);  // ターゲットとどれくらい離れているか
    private Quaternion _vOffsetRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);  // 角度

    // Start is called before the first frame update
    void Start()
    {
        if(_tTarget == null)
        {
            Debug.LogWarning("カメラの追従対象がいません。");
            Destroy(this);
        }

        // 現在のターゲットとの関係をオフセットに設定
        if(_isApplyCurrentOffset)
        {
            _vOffsetPosition = transform.position - _tTarget.position;
            _vOffsetRotation = transform.rotation;
        }
    }
    

    void LateUpdate()
    {
        transform.position = _tTarget.position + _vOffsetPosition;
        //transform.position = Vector3.Lerp(transform.position, _tTarget.position + _vOffsetPosition, Time.deltaTime * 2.0f);
    }
}
