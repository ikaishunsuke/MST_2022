/*==============================================================================
	[CBioCamerasManager.cs]
	�E�����̃o�C�I���ۂ��J�����p �S�̊Ǘ�

    ���F_cBioCameras�ɓo�^����J�����́A�ŏ��̃J�����ȊO��Camera��enable���I�t�ɂ��Ă�������
    ���F_cBioCameras�{�̂�Active�͑S�ăI���ɂ��Ă�������
--------------------------------------------------------------------------------
	2021.10.17 @Fujiwara Aiko
================================================================================
	History
		2021.10.17 Fujiwara Aiko
			�X�N���v�g�ǉ�

/*============================================================================*/

using System.Collections.Generic;
using UnityEngine;

public class CBioCamerasManager : MonoBehaviour
{
    [SerializeField] private CBioCamera[] _cBioCameras = null;   // �S���̃J������o�^���Ă���

    [SerializeField] private CBioCamera _currentCamera = null;   // ���݂̃J����

    [SerializeField] private CBioPlayerMover _playerMover = null;   // �v���C���[�i�J���������Ɍ������邽�߁j
    
    void Start()
    {
        _playerMover.Set_tCamera(_currentCamera.Get_camera().transform);
        // �C�x���g�o�^
        foreach (CBioCamera cBioCamera in _cBioCameras)
        {
            cBioCamera._ueInOutPlayer.AddListener(ChangeCamera);
        }
    }
    
    // �J������؂�ւ��邩�ǂ������f���A�؂�ւ���
    void ChangeCamera()
    {
        List<CBioCamera> activeCameras = new List<CBioCamera>(); 

        foreach (CBioCamera cBioCamera in _cBioCameras)
        {
            if (cBioCamera.Get_isEnterPlayer())
            {
                activeCameras.Add(cBioCamera);
            }
        }
        
        CBioCamera nextCamera;  // ���̃J�������
        int priority;   // �D��x�`�F�b�N�p

        // ���݂̃J�����͈͂���v���C���[���o�����`�F�b�N
        if (!_currentCamera.Get_isEnterPlayer())
        {// �o�Ă���ꍇ�́A���̃A�N�e�B�u�ȃJ�����ɐ؂�ւ���
            nextCamera = null;
            priority = 1000;
        }
        else
        {// �͈͓��̏ꍇ�͍��̃J��������Ƃ���
            nextCamera = _currentCamera;
            priority = _currentCamera.Get_iPriority();
        }

        // �D��x���������̂�����΂�������̃J�����Ƃ���
        foreach (CBioCamera cBioCamera in activeCameras)
        {
            int p = cBioCamera.Get_iPriority();
            if (p < priority)
            {
                priority = p;
                nextCamera = cBioCamera;
            }
        }

        // �G���[�`�F�b�N
        if(nextCamera == null)
        {// �v���C���[���ǂ��̃J�����͈̔͂ɂ�����Ȃ��ꏊ�ɂ���
            Debug.LogError("�v���C���[�����ׂẴJ�����͈̔͊O�ł�");
            return;
        }

        // �؂�ւ�
        if (_currentCamera != nextCamera)
        {
            _currentCamera.SetActiveCamera(false);  // �O�̃J�������I�t��
            _currentCamera = nextCamera;
            _currentCamera.SetActiveCamera(true);   // ���̃J�������I����
            _playerMover.Set_tCamera(_currentCamera.Get_camera().transform);
        }

    }
}
