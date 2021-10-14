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
    public static void SetFire(int index)
    {
        _bIsFire[index] = true;
    }
}
