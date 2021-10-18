/*==============================================================================
	[CWeightActionUI.cs]
	�E�d��Ɋւ���A�N�V������UI�\�����Ǘ�
--------------------------------------------------------------------------------
	2021.10.17 @Fujiwara Aiko
================================================================================
	History
		2021.10.17 Fujiwara Aiko
			�X�N���v�g�ǉ�

/*============================================================================*/

using UnityEngine;

public class CWeightActionUI : MonoBehaviour
{
    [SerializeField] GameObject _gPickUpActionUI = null;            // �E���A�N�V����UI
    [SerializeField] GameObject _gPutSpaceActionUI = null;          // �V���ɒu���A�N�V����UI

    [SerializeField] CPlayerPickUpState _cPlayerPickUpState = null; // �\���󋵂��擾
    

    void Start()
    {
        _gPickUpActionUI.SetActive(false);
        _gPutSpaceActionUI.SetActive(false);
        _cPlayerPickUpState._ueChangeCanAction.AddListener(ChangeShowUI);
    }

    // ChangeShowUI UI�\���ؑ�
    void ChangeShowUI()
    {
        if(_cPlayerPickUpState.CanPutSpace())
        {// �X�y�[�X�ɂ�����\��
            _gPutSpaceActionUI.SetActive(true);
            _gPickUpActionUI.SetActive(false);
            return;
        }
        if(_cPlayerPickUpState.CanPickUp())
        {// �d����E����\��
            _gPickUpActionUI.SetActive(true);
            _gPutSpaceActionUI.SetActive(false);
            return;
        }

        // �\���Ȃ�
        _gPickUpActionUI.SetActive(false);
        _gPutSpaceActionUI.SetActive(false);
    }
    
}
