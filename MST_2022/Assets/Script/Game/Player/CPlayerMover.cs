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


    [SerializeField] private float _fWalkSpeed = 5.0f;     // 歩きのスピード
    [SerializeField] private float _fRotateSpeed = 5.0f;   // 振り向きスピード


    private Vector2 _vForward;

    private Rigidbody _rb;

    
    void Start()
    {

        _rb = GetComponent<Rigidbody>();
        _vForward = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Walk(Vector2 dir)
    {
        _rb.velocity = new Vector3(dir.x, 0.0f, dir.y).normalized * _fWalkSpeed;

        Turn(dir);
    }



    void Turn(Vector2 dir)
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
