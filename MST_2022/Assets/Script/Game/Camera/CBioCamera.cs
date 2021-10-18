/*==============================================================================
	[CBioCamera.cs]
	�E�����̃o�C�I���ۂ��J�����p �ʃJ�����X�N���v�g
--------------------------------------------------------------------------------
	2021.10.17 @Fujiwara Aiko
================================================================================
	History
		2021.10.17 Fujiwara Aiko
			�X�N���v�g�ǉ�

/*============================================================================*/

using UnityEngine;
using UnityEngine.Events;

public class CBioCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private int _iPriority = 0;   // �D�揇�ʁ@�i�������Ⴂ���������j

    private bool _isEnterPlayer = false;    // �v���C���[���͈͓��ɂ��邩�ǂ���

    [HideInInspector] public UnityEvent _ueInOutPlayer = new UnityEvent();  // �v���C���[���o���肵���Ƃ��ɒʒm�����

    // �J�����̃A�N�e�B�u��Ԃ�ύX����
    public void SetActiveCamera(bool active)
    {
        _camera.enabled = active;
    }

    // �v���C���[���͈͓��ɂ��鎞��True
    // _isEnterPlayer�̃Q�b�^�[
    public bool Get_isEnterPlayer()
    {
        return _isEnterPlayer;
    }

    // �D�揇�ʂ��擾�i�������Ⴂ���������j
    // _iPriority�̃Q�b�^�[
    public int Get_iPriority()
    {
        return _iPriority;
    }

    // �J�������擾
    // _camera�̃Q�b�^�[
    public Camera Get_camera()
    {
        return _camera;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isEnterPlayer = true;
            _ueInOutPlayer.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isEnterPlayer = false;
            _ueInOutPlayer.Invoke();
        }
    }
}
