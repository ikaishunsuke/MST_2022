/*==============================================================================
    [CBalance.cs]
    ・天秤をいじるよ。でもここでは呼ばないよ。PutSpaceで呼んでるよ。
--------------------------------------------------------------------------------
    2021.10.14 @ MISAKI SASAKI
================================================================================
    History
        2021.10.14 MISAKI SASAKI
            Start()の中身なんだけどさ、後で変えるからひっどい書き方してるけど
            許されたいとか思ったりしましたですハイ。
            だってさ！！！！！プロトの段階なんだもん！！！
            部品分けとかさ！！！変わってくるじゃん！！！！
            っていう言い訳（ちゃんと直すもん。。）

            自分へ
            天秤の横棒の部分もあとでちゃんと動かすんだよ
            自分より
            
/*============================================================================*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBalance : MonoBehaviour
{
    [SerializeField] private static GameObject[] _gPlate = new GameObject[4];    // 動く側の天秤の皿　と部品も（プロト段階は3ってことでいれとく）
    [SerializeField] private GameObject _gComparisonPlate;  // 動かない側の天秤の皿
    private static float _fDifference_Y;
    
    // Start is called before the first frame update
    void Start()
    {
      
        //=================== ここあとで変える
        _gPlate[0] = GameObject.Find("cylinder2");
        _gPlate[1] = GameObject.Find("Cube (2)");
        _gPlate[2] = GameObject.Find("Cube (3)");
        _gPlate[3] = GameObject.Find("PutSpace");
        //==================== ここまで


        _fDifference_Y = _gComparisonPlate.transform.position.y - _gPlate[0].transform.position.y;
        _fDifference_Y = _fDifference_Y / 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void PlateUp()
    { 
        for(int index = 0; index < _gPlate.Length; index++)
        {
            Vector3 vec = _gPlate[index].transform.position;
            vec.y += _fDifference_Y;
            _gPlate[index].transform.position = vec;
        }
    }
    public static void PlateDown()
    {
        for (int index = 0; index < _gPlate.Length; index++)
        {
            Vector3 vec = _gPlate[index].transform.position;
            vec.y -= _fDifference_Y;
            _gPlate[index].transform.position = vec;
        }
    }
}
