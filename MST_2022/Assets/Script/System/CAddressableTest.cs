/*==============================================================================
    [CAddressableTest.cs]
    ・AddressableとEZAddresserのテスト用スクリプト
    後で消す
--------------------------------------------------------------------------------
    2021.10.12 @Fujiwara Aiko
================================================================================
    History
        2021.10.12 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/

using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CAddressableTest : MonoBehaviour
{

    private AsyncOperationHandle<GameObject> _handle;
    private GameObject _prefab;     // ロードされたプレハブ
    

    void Start()
    {
        // アセット読み込み
        StartCoroutine(Load());
    }
    

    void Update()
    {
        // Qキーで生成
        if (CInputManager.GetButtonDown(INPUT_CODE.CANCEL))
        {
            if (_prefab != null)
            {
                Instantiate(_prefab, transform);
            }
        }
    }

    // アセットをロードする
    private IEnumerator Load()
    {
        var _handle = Addressables.LoadAssetAsync<GameObject>("SphereObj");
        yield return _handle;
        if (_handle.Status == AsyncOperationStatus.Succeeded)
        {
            _prefab = _handle.Result;
        }
    }

}
