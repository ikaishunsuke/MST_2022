/*==============================================================================
    [CSoundSetting.cs]
    �����ʒ��������鎞�ɕK�v�ȂȂ�₩�������܂����B
--------------------------------------------------------------------------------
    2021.10.11 @Author MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            ���܂����B
        2021.10.14 MISAKI SASAKI
            �X���C�_�[�ł̉��ʒ����i�I�v�V������ʂŁj�ɑΉ������܂����B
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;       //Slider���g�p���邽�߂ɕK�v

public class CSoundSetting : MonoBehaviour
{
    private Slider _Slider;                     // ���ʒ����p�X���C�_�[
    private float _fScroolSpeed = 1;    // �����p�X���C�_�[�𓮂����X�s�[�h

    void Awake()
    {
        _Slider = GetComponent<Slider>();
        _Slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        _Slider.value = AudioListener.volume;
        //�X���C�_�[�̒l���ύX���ꂽ�特�ʂ��ύX����
        _Slider.onValueChanged.AddListener((sliderValue) => AudioListener.volume = sliderValue);
    }

    private void OnDisable()
    {
        _Slider.onValueChanged.RemoveAllListeners();
    }

    void Update()
    {
        float v = _Slider.value;

        if (CInputManager.GetButton(INPUT_CODE.LEFT))
        {
            v -= _fScroolSpeed * Time.deltaTime;
        }
        if (CInputManager.GetButton(INPUT_CODE.RIGHT))
        {
            v += _fScroolSpeed * Time.deltaTime;
        }
        
        v = Mathf.Clamp(v, 0, 1);
        _Slider.value = v;
    }
}
