/*==============================================================================
    [CPlayerMover.cs]
    �E�v���C���[�̓�������
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            �X�N���v�g�ǉ�
            
/*============================================================================*/


using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CPlayerMover : MonoBehaviour
{

    [SerializeField] protected float _fWalkSpeed = 5.0f;     // �����̃X�s�[�h
    [SerializeField] protected float _fRotateSpeed = 5.0f;   // �U������X�s�[�h

    protected Vector2 _vForward;      // �O����
    protected Rigidbody _rb;          // �v���C���[��Rigidbody


    protected virtual void Start()
    {
        // �L���b�V��
        _rb = GetComponent<Rigidbody>();
        _vForward = new Vector2(transform.forward.x, transform.forward.z);
    }
    
    // Walk �@����
    // �����Fdir ����������
    public virtual void Walk(Vector2 dir)
    {
        _rb.velocity = new Vector3(dir.x, 0.0f, dir.y).normalized * _fWalkSpeed;

        Turn(dir);
    }


    // Turn ��]�i�ړ������������j
    protected virtual void Turn(Vector2 dir)
    {

        if (dir.x < -0.1f || dir.y < -0.1f ||
            dir.x > 0.1f || dir.y > 0.1f)
        {// ���������͂���Ă�����
            _vForward = dir;    // ������ύX����
        }

        // �������������ς���
        Quaternion nextRotation = Quaternion.LookRotation(new Vector3(_vForward.x, 0.0f, _vForward.y), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, nextRotation, Time.deltaTime * _fRotateSpeed);
    }

}
