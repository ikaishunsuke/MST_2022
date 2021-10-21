/*==============================================================================
    [CPlayerPickUpState.cs]
    �E�����グ����u������ł���v���C���[���
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            �X�N���v�g�ǉ�
            
/*============================================================================*/


using UnityEngine;
using UnityEngine.Events;

public class CPlayerPickUpState : MonoBehaviour, IPlayerState
{
    [SerializeField] CPlayerMover _cPlayerMover = null;     // �v���C���[�𓮂����p
    private CWeightObject _gPickUpObject;   // �����グ�Ă���I�u�W�F�N�g
    private CWeightObject _gForwardObject;    // �ڂ̑O�ɂ���I�u�W�F�N�g

    [HideInInspector] public UnityEvent _ueChangeCanAction = new UnityEvent();       // ���ł���A�N�V�����̕ω���ʒm


    // Move ����
    // �����F direction ����
    public void Move(Vector2 direction)
    {
        _cPlayerMover.Walk(direction);
    }
    
    // Action �A�N�V���� �`�����グ��u���`
    public void Action()
    {
        // ���݂̏�Ԃɂ���Ď����グ��or�u��or�������Ȃ�

        if(_gForwardObject != null &&
            _gPickUpObject == null)
        {// �����グ��
            _gPickUpObject = _gForwardObject;
            _gPickUpObject.PickedUp(transform.parent, transform.position);
            _gPickUpObject._ueChangeCanPut.AddListener(ChangeCanAction);
            _ueChangeCanAction.Invoke();
        }
        else if (_gPickUpObject != null)
        {// �u��
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

    // ���ł���A�N�V�������ω�
    void ChangeCanAction()
    {
        _ueChangeCanAction.Invoke();
    }

    // CanPickUp �E����I�u�W�F�N�g�����邩�ǂ������擾
    public bool CanPickUp()
    {

        if (_gForwardObject != null &&
            _gPickUpObject == null)
        {
            return true;
        }
        return false;
    }

    // CanPutSpace �X�y�[�X�ɒu���邩�ǂ������擾
    public bool CanPutSpace()
    {
        if (_gPickUpObject != null)
        {
            return _gPickUpObject.CanPutSpace();
        }
        return false;
    }

    // CanDiscard �̂Ă��邩�ǂ������擾
    public bool CanDiscard()
    {
        if (_gPickUpObject != null)
        {
            return !_gPickUpObject.CanPutSpace();
        }
        return false;
    }

}
