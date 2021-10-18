/*==============================================================================
    [CSoundSetting.cs]
    ◆音量調整をする時に必要ななんやかんやを入れました。
--------------------------------------------------------------------------------
    2021.10.11 @Author MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            作りました。
        2021.10.14 MISAKI SASAKI
            スライダーでの音量調整（オプション画面で）に対応させました。
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;       //Sliderを使用するために必要

public class CSoundSetting : MonoBehaviour
{
    private Slider _Slider;                     // 音量調整用スライダー
    private float _fScroolSpeed = 1;    // 調整用スライダーを動かすスピード

    void Awake()
    {
        _Slider = GetComponent<Slider>();
        _Slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        _Slider.value = AudioListener.volume;
        //スライダーの値が変更されたら音量も変更する
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
