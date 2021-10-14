using UnityEngine;


public class CPutSpace : MonoBehaviour
{
    [SerializeField] private Transform[] _tPutSpaces = null;

    private CPickedUpObject[] _cPickedUpObjectSpaces;
    // private CPickedUpObject _cPutObject; // 置かれたオブジェクト
    

    void Start()
    {
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
                    // 当たりオブジェクトを取り除いたの処理

                }
            }
        }

    }
}
