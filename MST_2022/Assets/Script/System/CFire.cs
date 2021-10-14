/*==============================================================================
    [CFire.cs]
    ・火を灯すよ。
--------------------------------------------------------------------------------
    2021.10.11 @ MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            Set_Fire(引数)はstaticにしてるから引数部分に
            まぁ火のどれ灯すか（配列だから0スタートね）を指定して
            別スクリプトで呼べばすぐ火がでちゃうってわけ。
            ちなみに暫定で最大数3にしてるだけだけどすぐ増やせるよ
            っていう自分用メモ。
            
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFire : MonoBehaviour
{
    public GameObject[] _gFire;              // 最初から配置しておくこと
    public static bool[] _bIsFire = new bool[3];    // 燃やす時はtrueをいれる、最大数は_gFireと同数にすること

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
            if (_bIsFire[index])
            {
                if (!_gFire[index].activeSelf)
                {
                    _gFire[index].SetActive(true);
                }
            }
        }
    }

    // 火をつけるときに使う、配列と同じなので0が一番左端のイメージで。
    public static void Set_Fire(int index)
    {
        _bIsFire[index] = true;
    }
}
