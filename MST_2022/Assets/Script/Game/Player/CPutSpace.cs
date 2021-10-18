/*==============================================================================
    [CPutSpace.cs]
    ・天秤の皿の上。つまり物を置く場所
--------------------------------------------------------------------------------
    2021.10.14 @Fujiwara Aiko
================================================================================
    History
        2021.10.14 Fujiwara Aiko
            スクリプト追加
        2021.10.14 MISAKi SASAKI
            火を灯す・消す&天秤の位置を動かす。そんなやつを追加
/*============================================================================*/

using UnityEngine;

public class CPutSpace : MonoBehaviour
{
    [SerializeField] private Transform[] _tPutSpaces = null;    // 置ける場所一覧
    private CPickedUpObject[] _cPickedUpObjectSpaces;   // スペースに置かれているもの一覧
    

    void Start()
    {
        // 置ける場所と置く場所の個数を一致させる
        _cPickedUpObjectSpaces = new CPickedUpObject[_tPutSpaces.Length];
    }

    // PlacedObject スペースにオブジェクトを配置する
    // 引数：置くオブジェクト
    // 戻り値：置かれる場所（置くのに失敗した場合null）
    public Transform PlacedObject(CPickedUpObject obj)
    {
        // 空いてるスペースを探す
        for(int i = 0; i < _cPickedUpObjectSpaces.Length; i++)
        {
            if(_cPickedUpObjectSpaces[i] == null)
            {
                // 空いてるスペースに物を置く
                _cPickedUpObjectSpaces[i] = obj;

                if (obj.Get_isCorrect())
                {
                    // 当たりオブジェクトを置いたときの処理

                    // トーチに火を付ける
                    CFire.Set_Fire(i, true);

                    // 天秤の位置調整
                    CBalance.PlateUp();
                }

                return _tPutSpaces[i];
            }
        }
        
        // 空いてるスペース無し
        return null;
    }


    // RemoveObject スペースに置かれているオブジェクトを取り除く
    // 引数：取り除くオブジェクト
    public void RemoveObject(CPickedUpObject obj)
    {

        // どのスペースに置かれているか
        for (int i = 0; i < _cPickedUpObjectSpaces.Length; i++)
        {
            if (_cPickedUpObjectSpaces[i] == obj)
            {
                // 物を取り除く
                _cPickedUpObjectSpaces[i] = null;

                if (obj.Get_isCorrect())
                {
                    // 当たりオブジェクトを取り除いた時の処理

                    // トーチの火を消す
                    CFire.Set_Fire(i, false);

                    // 天秤の位置調整
                    CBalance.PlateDown();

                }
            }
        }

    }
}
