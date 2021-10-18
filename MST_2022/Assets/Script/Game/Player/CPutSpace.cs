/*==============================================================================
    [CPutSpace.cs]
    �E�V���̎M�̏�B�܂蕨��u���ꏊ
--------------------------------------------------------------------------------
    2021.10.14 @Fujiwara Aiko
================================================================================
    History
        2021.10.14 Fujiwara Aiko
            �X�N���v�g�ǉ�
        2021.10.14 MISAKi SASAKI
            �΂𓔂��E����&�V���̈ʒu�𓮂����B����Ȃ��ǉ�
        2021.10.18 Fujiwara Aiko
            �N���A���聨�V��������������ǉ�
/*============================================================================*/

using UnityEngine;

public class CPutSpace : MonoBehaviour
{
    [SerializeField] private Transform[] _tPutSpaces = null;    // �u����ꏊ�ꗗ
    private CPickedUpObject[] _cPickedUpObjectSpaces;   // �X�y�[�X�ɒu����Ă�����̈ꗗ

    private int _iMaxPutNum = 3;        // �u���鐔�������̃I�u�W�F�N�g�̐�
    private int _iCorrectPutObjNum = 0;  // ���u���Ă���I�u�W�F�N�g�̒��Ő����̐�
    private bool _isClear = false;      // �N���A����t���O�i�S�Đ����̃I�u�W�F�N�g���u���ꂽ�Ƃ�True�j
    

    void Start()
    {
        // �u����ꏊ�ƒu���ꏊ�̌�����v������
        _cPickedUpObjectSpaces = new CPickedUpObject[_tPutSpaces.Length];
        _iMaxPutNum = _tPutSpaces.Length;
    }

    // PlacedObject �X�y�[�X�ɃI�u�W�F�N�g��z�u����
    // �����F�u���I�u�W�F�N�g
    // �߂�l�F�u�����ꏊ�i�u���̂Ɏ��s�����ꍇnull�j
    public Transform PlacedObject(CPickedUpObject obj)
    {
        if (_isClear) return null;  // �N���A�ς̏ꍇ�u���Ȃ�

        // �󂢂Ă�X�y�[�X��T��
        for (int i = 0; i < _cPickedUpObjectSpaces.Length; i++)
        {
            if(_cPickedUpObjectSpaces[i] == null)
            {
                // �󂢂Ă�X�y�[�X�ɕ���u��
                _cPickedUpObjectSpaces[i] = obj;

                if (obj.Get_isCorrect())
                {
                    // ������I�u�W�F�N�g��u�����Ƃ��̏���

                    // �g�[�`�ɉ΂�t����
                    CFire.Set_Fire(i, true);
                    // �V���̈ʒu����
                    CBalance.PlateUp();

                    _iCorrectPutObjNum++;
                    if(_iCorrectPutObjNum >= _iMaxPutNum)
                    {// �N���A
                        obj.transform.parent = transform;
                        CBalance.Instance.Disappear();
                        _isClear = true;

                        // ����Ă���d��͓������Ȃ�����i�X�N���v�g�������j
                        foreach(CPickedUpObject inSpaceObj in _cPickedUpObjectSpaces)
                        {
                            Destroy(inSpaceObj);
                        }
                    }

                }

                return _tPutSpaces[i];
            }
        }
        
        // �󂢂Ă�X�y�[�X����
        return null;
    }


    // RemoveObject �X�y�[�X�ɒu����Ă���I�u�W�F�N�g����菜��
    // �����F��菜���I�u�W�F�N�g
    public void RemoveObject(CPickedUpObject obj)
    {
        if (_isClear) return;  // �N���A�ς̏ꍇ���Ȃ�

        // �ǂ̃X�y�[�X�ɒu����Ă��邩
        for (int i = 0; i < _cPickedUpObjectSpaces.Length; i++)
        {
            if (_cPickedUpObjectSpaces[i] == obj)
            {
                // ������菜��
                _cPickedUpObjectSpaces[i] = null;

                if (obj.Get_isCorrect())
                {
                    // ������I�u�W�F�N�g����菜�������̏���

                    // �g�[�`�̉΂�����
                    CFire.Set_Fire(i, false);

                    // �V���̈ʒu����
                    CBalance.PlateDown();

                    _iCorrectPutObjNum--;

                }
            }
        }

    }

    // CanPlacedObject �u���邩�ǂ���
    public bool IsThereSpace()
    {
        if (_isClear) return false;  // �N���A�ς̏ꍇ�u���Ȃ�

        // �󂢂Ă�X�y�[�X��T��
        for (int i = 0; i < _cPickedUpObjectSpaces.Length; i++)
        {
            if (_cPickedUpObjectSpaces[i] == null)
            {
                return true;
            }
        }

        return false;
    }
}
