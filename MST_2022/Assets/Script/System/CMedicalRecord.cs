/*==============================================================================
    [CMedicalRecord.cs]
    �E�J���e��ʂ̕\��
--------------------------------------------------------------------------------
    2021.10.11 @ MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            �Z�[�u���[�h�@�\�������B(PlayerPrefs�j
            ���A���Ȃ݂�for���̍ő�l��2�ɂ��Ă邯�ǂ���͍��X�e�[�W�P��
            �v���g������������ĈӖ���
            �܂��S�X�e�[�W���������łɂ�_iHint.Length�ɂ��܂��B
            ���Ă��������p�����c���Ƃ�
            
/*============================================================================*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMedicalRecord : MonoBehaviour
{
    private GameObject _gIcon;
    private GameObject _gPanel;
    public static GameObject _gNewMark; // �܂��J���e�ڍׂ��݂Ă��Ȃ��������ꍇ�͕K�����B
    public GameObject[] _gHint;
    private GameObject _gPausePanel;

    /* PlayerPrefs�̊֌W��bool�ɂ�������������int�ŉ䖝���܂��B
       int = 0�̎���false,int = 1�̎���true�Ƃ���B   */
    public static int[] _iHint = new int[8];

    private GameObject mainCamera;      // ���C���J�����i�[�p
    private GameObject subCamera;       // �T�u�J�����i�[�p 

    public GameObject _gCursor;         // �J�[�\���摜
    private Vector3[] _iCursorPosition = new Vector3[2];
    private int _iCursorCnt;

    // Start is called before the first frame update
    void Start()
    {
        // �f�[�^�̃��[�h
        Load_MedicalRecord();

        //for (int index = 0; index < _iHint.Length; index++)
        for (int index = 0; index < 2; index++)
        {
            // �J���e�f�[�^���J������Ă��Ȃ��ꍇ
            if (_iHint[index] == 0)
            {
                _gHint[index * 2].SetActive(true);
                _gHint[1 + index * 2].SetActive(false);
            }
            if (_iHint[index] == 1)
            {
                _gHint[index * 2].SetActive(false);
                _gHint[1 + index * 2].SetActive(true);
            }
        }

        _gIcon = GameObject.Find("MedicalRecord_Icon");

        _gNewMark = GameObject.Find("NewMark");
        _gNewMark.SetActive(false);
        _gCursor = GameObject.Find("Cursor");
        _gCursor.SetActive(false);

        _gPanel = GameObject.Find("Panel_MedicalRecord");
        _gPanel.SetActive(false);

        _gPausePanel = GameObject.Find("Panel_Pause");

        _iCursorPosition[0] = _gCursor.transform.position;

        for (int index = 1; index < _iCursorPosition.Length; index++)
        {
            Vector3 vec = _iCursorPosition[index - 1];
            vec.x += 20.0f;
            _iCursorPosition[index] = vec;
        }
        _iCursorCnt = 0;

        //���C���J�����ƃT�u�J���������ꂼ��擾
        mainCamera = GameObject.Find("MainCamera");
        subCamera = GameObject.Find("MedicalRecord_Camera");

        //�T�u�J�������A�N�e�B�u�ɂ���
        subCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gPausePanel.activeSelf)
        {
            if (CInputManager.GetButtonDown(INPUT_CODE.X))
            {
                if (!_gPanel.activeSelf)
                {
                    _gNewMark.SetActive(false);
                    _gIcon.SetActive(false);
                    _gPanel.SetActive(true);
                    
                    // �J���e�f�[�^�̍X�V
                    //for (int index = 0; index < _iHint.Length; index++)
                    for (int index = 0; index < 2; index++)
                    {
                        // �J���e�f�[�^���J������Ă��Ȃ��ꍇ
                        if (_iHint[index] == 0)
                        {
                            _gHint[index * 2].SetActive(true);
                            _gHint[1 + index * 2].SetActive(false);
                        }
                        // �J���e�f�[�^���J������Ă���ꍇ
                        if (_iHint[index] == 1)
                        {
                            _gHint[index * 2].SetActive(false);
                            _gHint[1 + index * 2].SetActive(true);

                            _gCursor.SetActive(true);
                            _gCursor.transform.position = _iCursorPosition[0];
                        }
                    }
                }
                else
                {
                    _gPanel.SetActive(false);
                    _gIcon.SetActive(true);
                }

                // �J���e���\������Ă���ꍇ
                if (_gPanel.activeSelf)
                {
                    //�T�u�J�������A�N�e�B�u�ɐݒ�
                    mainCamera.SetActive(false);
                    subCamera.SetActive(true);
                }
                else
                {
                    //���C���J�������A�N�e�B�u�ɐݒ�
                    subCamera.SetActive(false);
                    mainCamera.SetActive(true);
                }
            }
            if (_gPanel)   // �p�l�����\������Ă���ꍇ
            {
                if (CInputManager.GetButtonDown(INPUT_CODE.RIGHT))
                {
                    for (int index = 0; index < 2; index++)
                    {
                        // �J���e�f�[�^���J������Ă��Ȃ��ꍇ
                        if (_iHint[index] == 0)
                        {
                        }
                        // �J���e�f�[�^���J������Ă���ꍇ
                        if (_iHint[index] == 1)
                        {
                            _iCursorCnt = 1;
                            _gCursor.transform.position = _iCursorPosition[_iCursorCnt];
                        }
                    }
                }
                if (CInputManager.GetButtonDown(INPUT_CODE.LEFT))
                {
                    for (int index = 0; index < 2; index++)
                    {
                        // �J���e�f�[�^���J������Ă��Ȃ��ꍇ
                        if (_iHint[index] == 0)
                        {
                        }
                        // �J���e�f�[�^���J������Ă���ꍇ
                        if (_iHint[index] == 1)
                        {
                            _iCursorCnt = 0;
                            _gCursor.transform.position = _iCursorPosition[_iCursorCnt];
                        }
                    }
                }
            }
        }
    
    }

    // �Y������J���e�f�[�^�ΏۃI�u�W�F�N�g�ɂ������Ƃ����
    public static void OnHint_1()
    {
        if (_iHint[0] == 0)
        {
            _iHint[0] = 1;
            _gNewMark.SetActive(true);  // �J���e�f�[�^���ǉ����ꂽ��I�}�[�N���o��
        }
        // �J���e�ɓo�^����Ă���ꍇ�͉������Ȃ��B
    }
    public static void OnHint_2()
    {
        if (_iHint[1] == 0)
        {
            _iHint[1] = 1;
            _gNewMark.SetActive(true);  // �J���e�f�[�^���ǉ����ꂽ��I�}�[�N���o��
        }
        // �J���e�ɓo�^����Ă���ꍇ�͉������Ȃ��B
    }


    //====================================================== �Z�[�u&���[�h�i�J���e�f�[�^�i�q���g�j�擾�󋵁j
    public void Save_MedicalRecord()
    {
        // �J���e�f�[�^�̎擾�󋵂̃Z�[�u
        for (int index = 0; index < _iHint.Length; index++)
        {
            PlayerPrefs.SetInt($"HINT{index}", _iHint[index]);
        }

        Debug.Log("�J���e�f�[�^�̃Z�[�u����");

    }
    public void Load_MedicalRecord()
    {
        // �J���e�f�[�^�̎擾�󋵂̃��[�h
        for (int index = 0; index < _iHint.Length; index++)
        {
            int iHint = PlayerPrefs.GetInt($"HINT{index}", 0);
            _iHint[index] = iHint;
        }
        Debug.Log("�J���e�f�[�^�̃��[�h����");
    }
}
