/*==============================================================================
    [CBioPlayerMover.cs]
    �E�o�C�I�J�����p�v���C���[
    �J�����ړ������B
--------------------------------------------------------------------------------
    2021.10.17 @Fujiwara Aiko
================================================================================
    History
        2021.10.17 Fujiwara Aiko
            �X�N���v�g�ǉ�
            
/*============================================================================*/


using UnityEngine;

public class CBioPlayerMover : CPlayerMover
{
    private Transform _tCamera = null;          // �v���C���[�̃J�����i�i�s�����ɂȂ�j

    private Vector2 _vBeforeDir = new Vector2(0.0f, 0.0f);          // �P�t���[���O�̓��͕���
    private Vector3 _vBeforeDirection = new Vector3(0.0f, 0.0f, 0.0f);    // �P�t���[���O�̈ړ�����

    // Walk �@����
    // �����Fdir ����������
    public override void Walk(Vector2 dir)
    {
        Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
        if (_vBeforeDir == dir)
        {// ���͂��ς��Ȃ�������ړ������͕ϓ����Ȃ�
            direction = _vBeforeDirection;
        }
        else
        {
            Vector3 forward = new Vector3(_tCamera.forward.x, 0.0f, _tCamera.forward.z);
            forward.Normalize();
            Vector3 right = new Vector3(_tCamera.right.x, 0.0f, _tCamera.right.z);
            forward.Normalize();
            direction = forward * dir.y + right * dir.x;
            direction.Normalize();
            // ����
            _rb.velocity = direction * _fWalkSpeed;
        }


        _rb.velocity = direction * _fWalkSpeed;


        Turn(new Vector2(direction.x, direction.z));

        // ���͂�ۑ�
        _vBeforeDir = dir;
        // �ړ�������ۑ�
        _vBeforeDirection = direction;
    }


    // ���݂̃J�������Z�b�g
    public void Set_tCamera(Transform camera)
    {
        _tCamera = camera;
    }

}