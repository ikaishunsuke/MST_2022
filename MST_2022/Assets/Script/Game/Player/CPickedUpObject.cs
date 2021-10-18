/*==============================================================================
    [CPickedUpObject.cs]
    �E�v���C���[���E����I�u�W�F�N�g
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            �X�N���v�g�ǉ�
            
/*============================================================================*/


using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class CPickedUpObject : MonoBehaviour
{
    
    [SerializeField] private bool _isCorrect = false;    // ������I�u�W�F�N�g���ǂ���
    private BoxCollider _collider = null;          // ������p

    private Vector3 _vOffsetPos;                    // ���X�������ꏊ
    private Quaternion _vOffsetRot;                 // ���X�̊p�x
    private Vector3 _vOffsetColliderSize;           // ���X��Collider�傫��

    private CPutSpace _cTouchingPutSpace = null;    // ���ݐG��Ă���u���ꏊ
    private bool _isPlacedSpace = false;            // �u���ꏊ�ɒu����Ă��邩

    private Animator _animator;                     // �A�j���[�V�����L���b�V���i���ɖ߂�A�j���[�V�����p�j

    [HideInInspector] public UnityEvent _ueChangeCanPut = new UnityEvent();    // �u���邩�ǂ����ω������Ƃ��ɌĂяo�����


    private void Start()
    {
        // ���݈ʒu��ۑ�
        _vOffsetPos = transform.position;
        _vOffsetRot = transform.rotation;

        _collider = GetComponent<BoxCollider>();
        _vOffsetColliderSize = _collider.size;

        gameObject.AddComponent<BoxCollider>().isTrigger = true;

        _animator = GetComponent<Animator>();

    }

    // PickedUp �����グ����
    // �����F Parent �����グ��ꂽ���̐e�I�u�W�F�N�g�Aposition �����グ��ꂽ���̈ʒu
    public void PickedUp(Transform parent, Vector3 position)
    {

        // ���g���q�I�u�W�F�N�g�ɂ��A�ʒu��ύX
        transform.parent = parent;
        transform.localRotation = Quaternion.identity;
        transform.position = position + transform.forward * transform.localScale.x * 0.5f + transform.up * transform.localScale.y * 0.5f;

        // �u���X�y�[�X�ɒu����Ă�����A�X�y�[�X�Ɏ����グ��ꂽ���Ƃ�`����
        if(_isPlacedSpace)
        {
            _cTouchingPutSpace.RemoveObject(this);
            _cTouchingPutSpace = null;
            _isPlacedSpace = false;
            _collider.size = _vOffsetColliderSize;  // Collision�̑傫�������ɖ߂�
        }

    }

    // Placed  �u�����
    public void Placed()
    {

        if(_cTouchingPutSpace != null)
        {
            // �X�y�[�X�ɒu��
            Transform spacePos = _cTouchingPutSpace.PlacedObject(this);
            if(spacePos != null)
            {
                // �X�y�[�X�ɒu�����
                transform.parent = null;
                transform.localRotation = Quaternion.identity;
                transform.position = spacePos.position + transform.up * transform.lossyScale.y;

                _collider.size = new Vector3(_vOffsetColliderSize.x * (8.0f / transform.lossyScale.x), _vOffsetColliderSize.y, _vOffsetColliderSize.z * (20.0f / transform.lossyScale.x));
                _isPlacedSpace = true;
            }

        }
        else
        {
            // �u����ʒu����Ȃ������猳�̏ꏊ�ɖ߂�
            if (_animator != null)
            {
                _animator.Play("Disappear");    // ������A�j���[�V�����J�n
            }
            else
            {
                ReturnOffsetPos();
            }
        }

    }

    // ���̈ʒu�ɖ߂�
    // (������A�j���[�V�����I�����ɌĂ�)
    public void ReturnOffsetPos()
    {
        transform.parent = null;
        transform.position = _vOffsetPos;
        transform.rotation = _vOffsetRot;

        _cTouchingPutSpace = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        CPutSpace space = other.GetComponent<CPutSpace>();
        if (space != null && !_isPlacedSpace)
        {
            // ���s
            Transform spacePos = space.PlacedObject(this);

            if (spacePos != null)
            {
                // �X�y�[�X�ɂ������Ԃ�
                _cTouchingPutSpace = space;
                _ueChangeCanPut.Invoke();

                // ���s�����̂�������
                _cTouchingPutSpace.RemoveObject(this);
            }
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        CPutSpace space = other.GetComponent<CPutSpace>();
        if (space != null)
        {
            // �X�y�[�X�ɂ����Ȃ�
            _cTouchingPutSpace = null;
            _ueChangeCanPut.Invoke();
        }
    }

    // CanPutSpace �X�y�[�X�ɒu���邩�ǂ���
    public bool CanPutSpace()
    {
        if(_cTouchingPutSpace != null)
        {
            return true;
        }
        return false;
    }

    // ������̃I�u�W�F�N�g���ǂ��� (Getter)
    public bool Get_isCorrect()
    {
        return _isCorrect;
    }

}
