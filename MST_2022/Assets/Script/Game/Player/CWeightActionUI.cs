/*==============================================================================
	[CWeightActionUI.cs]
	・重りに関するアクションのUI表示を管理
--------------------------------------------------------------------------------
	2021.10.17 @Fujiwara Aiko
================================================================================
	History
		2021.10.17 Fujiwara Aiko
			スクリプト追加

/*============================================================================*/

using UnityEngine;

public class CWeightActionUI : MonoBehaviour
{
    [SerializeField] GameObject _gPickUpActionUI = null;            // 拾うアクションUI
    [SerializeField] GameObject _gPutSpaceActionUI = null;          // 天秤に置くアクションUI

    [SerializeField] CPlayerPickUpState _cPlayerPickUpState = null; // 表示状況を取得
    

    void Start()
    {
        _gPickUpActionUI.SetActive(false);
        _gPutSpaceActionUI.SetActive(false);
        _cPlayerPickUpState._ueChangeCanAction.AddListener(ChangeShowUI);
    }

    // ChangeShowUI UI表示切替
    void ChangeShowUI()
    {
        if(_cPlayerPickUpState.CanPutSpace())
        {// スペースにおける表示
            _gPutSpaceActionUI.SetActive(true);
            _gPickUpActionUI.SetActive(false);
            return;
        }
        if(_cPlayerPickUpState.CanPickUp())
        {// 重りを拾える表示
            _gPickUpActionUI.SetActive(true);
            _gPutSpaceActionUI.SetActive(false);
            return;
        }

        // 表示なし
        _gPickUpActionUI.SetActive(false);
        _gPutSpaceActionUI.SetActive(false);
    }
    
}
