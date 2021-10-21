/*==============================================================================
    [CPlayerMoverTPFixed.cs]
    �E�J�����O�����ɐi�ރv���C���[
    �J�����ړ������B�������ŉ�]
--------------------------------------------------------------------------------
    2021.10.12 @Fujiwara Aiko
================================================================================
    History
        2021.10.12 Fujiwara Aiko
            �X�N���v�g�ǉ�
            
/*============================================================================*/


using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CPlayerMoverTPFixed : CPlayerMover
{

    protected override void Start()
    {
        // �L���b�V��
        _rb = GetComponent<Rigidbody>();
        _vForward = new Vector2(transform.forward.x, transform.forward.z);
    }

    // Walk �@����
    // �����Fdir ����������
    public override void Walk(Vector2 dir)
    {
        // �O�ɐi��
        _rb.velocity = transform.forward * dir.y * _fWalkSpeed;

        // ���E�ɉ�]����
        if (dir.x < -0.1f || dir.x > 0.1f)
        {
            Debug.Log(dir.x);
            _rb.MoveRotation(transform.rotation * Quaternion.AngleAxis(Time.deltaTime * _fRotateSpeed * dir.x, Vector3.up));
            
        }
        else
        {
            _rb.angularVelocity = Vector3.zero;
        }
    }

}
