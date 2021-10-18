/*==============================================================================
    [CFire.cs]
    �E�΂𓔂���B
--------------------------------------------------------------------------------
    2021.10.11 @ MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            Set_Fire(����)��static�ɂ��Ă邩�����������
            �܂��΂̂ǂꓔ�����i�z�񂾂���0�X�^�[�g�ˁj���w�肵��
            �ʃX�N���v�g�ŌĂׂ΂����΂��ł��Ⴄ���Ă킯�B
            ���Ȃ݂Ɏb��ōő吔3�ɂ��Ă邾�������ǂ������₹���
            ���Ă��������p�����B
            
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFire : MonoBehaviour
{
    public GameObject[] _gFire;              // �ŏ�����z�u���Ă�������
    public static bool[] _bIsFire = new bool[3];    // �R�₷����true�������A�ő吔��_gFire�Ɠ����ɂ��邱��

    // Start is called before the first frame update
    void Start()
    {
        for (int index = 0; index < 3; index++)
        {
            _bIsFire[index] = false;
            _gFire[index].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int index = 0; index < 3; index++)
        {
            // �΂�����t���O�������Ă���
            if (_bIsFire[index])
            {
                // �΂��A�N�e�B�u�łȂ��ꍇ�̓A�N�e�B�u�ɂ���
                if (!_gFire[index].activeSelf)
                {
                    _gFire[index].SetActive(true);
                }
            }
            else if(!_bIsFire[index])
            {
                // �΂��A�N�e�B�u�ȏꍇ�͔�A�N�e�B�u�ɂ���
                if (_gFire[index].activeSelf)
                {
                    _gFire[index].SetActive(false);
                }
            }
        }
    }

    // �΂�����/�����Ƃ��Ɏg���A�z��Ɠ����Ȃ̂�0����ԍ��[�̃C���[�W�ŁB
    public static void Set_Fire(int index,bool flag)
    {
        _bIsFire[index] = flag;
    }
}
