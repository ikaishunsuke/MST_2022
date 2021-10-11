/*==============================================================================
    [CSoundSetting.cs]
    ◆音量調整をする時に必要ななんやかんやを入れました。
--------------------------------------------------------------------------------
    2021.10.11 @Author MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            作りました。
        
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CSoundSetting : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer = default;
    public static int _iMasterVol = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 音量UP（ボタンとかどっかのスクリプトで呼ぶ用）
    public void OnVolUp()
    {
        if (_iMasterVol == 1)
        {
            Debug.Log("音量２に");
            _audioMixer.SetFloat("SE", 0.0f);
            _audioMixer.SetFloat("BGM", 0.0f);

            //vol1.SetActive(true);
            //vol2.SetActive(true);
            //vol3.SetActive(false);

            _iMasterVol++;
        }
        else if (_iMasterVol == 2)
        {
            Debug.Log("音量３に");
            _audioMixer.SetFloat("SE", 15.0f);
            _audioMixer.SetFloat("BGM", 15.0f);

            //vol1.SetActive(true);
            //vol2.SetActive(true);
            //vol3.SetActive(true);

            _iMasterVol++;
        }
    }
    // 音量DOWN（ボタンとかどっかのスクリプトで呼ぶ用）
    public void OnVolDown()
    {
        if (_iMasterVol == 2)
        {
            Debug.Log("音量１に");
            _audioMixer.SetFloat("SE", -15.0f);
            _audioMixer.SetFloat("BGM", -15.0f);

            //vol1.SetActive(true);
            //vol2.SetActive(false);
            //vol3.SetActive(false);

            _iMasterVol--;
        }
        else if (_iMasterVol == 3)
        {
            Debug.Log("音量２に");
            _audioMixer.SetFloat("SE", 0.0f);
            _audioMixer.SetFloat("BGM", 0.0f);

            //vol1.SetActive(true);
            //vol2.SetActive(true);
            //vol3.SetActive(false);

            _iMasterVol--;
        }
    }
}
