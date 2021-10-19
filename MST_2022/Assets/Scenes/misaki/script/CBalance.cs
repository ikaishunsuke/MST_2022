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

        2021.10.18 AIKO FUJIWARA
            重りを置いた後天秤が消える処理を追加
            消えた後ゴール出現も追加
            
/*============================================================================*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBalance : MonoBehaviour
{
    [SerializeField] private static GameObject[] _gPlate = new GameObject[4];    // 動く側の天秤の皿　と部品も（プロト段階は3ってことでいれとく）
    [SerializeField] private GameObject _gComparisonPlate;  // 動かない側の天秤の皿
    private static float _fDifference_Y;
    [SerializeField] private GameObject _gGoal = null;  // ゴール（天秤消去後に出現）

    private static CBalance _instance = null;  // Singleton化
    public static CBalance Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CBalance>();
            }
            return _instance;
        }
    }

    
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

    // Dissapear 消えるスタート
    public void Disappear()
    {
        StartCoroutine(DisappearAnim());
    }

    // DisappearAnim 消えるアニメーション
    private IEnumerator DisappearAnim()
    {

        // 子オブジェクトのRendererを取ってきて透明度を下げる
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        for(float time = 0.0f; time < 3.0f; time += Time.deltaTime)
        {
            foreach(Renderer renderer in renderers)
            {
                Color color = renderer.material.color;
                color.a = ((3.0f - time) / 3.0f) * 2.0f;
                if (color.a > 1.0f) color.a = 1.0f;
                renderer.material.color = color;
                renderer.material.SetFloat("_Threshold", time / 3.0f);
            }
            yield return null;
        }
        

        // ゴールを出現させる
        _gGoal.SetActive(true);

        // 天秤を消去
        Destroy(gameObject);
    }
}
