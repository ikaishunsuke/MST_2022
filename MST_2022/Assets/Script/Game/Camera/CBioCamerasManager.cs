/*==============================================================================
	[CBioCamerasManager.cs]
	・初期のバイオっぽいカメラ用 全体管理

    注：_cBioCamerasに登録するカメラは、最初のカメラ以外はCameraのenableをオフにしておくこと
    注：_cBioCameras本体のActiveは全てオンにしておくこと
--------------------------------------------------------------------------------
	2021.10.17 @Fujiwara Aiko
================================================================================
	History
		2021.10.17 Fujiwara Aiko
			スクリプト追加

/*============================================================================*/

using System.Collections.Generic;
using UnityEngine;

public class CBioCamerasManager : MonoBehaviour
{
    [SerializeField] private CBioCamera[] _cBioCameras = null;   // 全部のカメラを登録しておく

    [SerializeField] private CBioCamera _currentCamera = null;   // 現在のカメラ

    [SerializeField] private CBioPlayerMover _playerMover = null;   // プレイヤー（カメラ方向に向かせるため）
    
    void Start()
    {
        _playerMover.Set_tCamera(_currentCamera.Get_camera().transform);
        // イベント登録
        foreach (CBioCamera cBioCamera in _cBioCameras)
        {
            cBioCamera._ueInOutPlayer.AddListener(ChangeCamera);
        }
    }
    
    // カメラを切り替えるかどうか判断し、切り替える
    void ChangeCamera()
    {
        List<CBioCamera> activeCameras = new List<CBioCamera>(); 

        foreach (CBioCamera cBioCamera in _cBioCameras)
        {
            if (cBioCamera.Get_isEnterPlayer())
            {
                activeCameras.Add(cBioCamera);
            }
        }
        
        CBioCamera nextCamera;  // 次のカメラ候補
        int priority;   // 優先度チェック用

        // 現在のカメラ範囲からプレイヤーが出たかチェック
        if (!_currentCamera.Get_isEnterPlayer())
        {// 出ている場合は、他のアクティブなカメラに切り替える
            nextCamera = null;
            priority = 1000;
        }
        else
        {// 範囲内の場合は今のカメラを基準とする
            nextCamera = _currentCamera;
            priority = _currentCamera.Get_iPriority();
        }

        // 優先度が高いものがあればそれを次のカメラとする
        foreach (CBioCamera cBioCamera in activeCameras)
        {
            int p = cBioCamera.Get_iPriority();
            if (p < priority)
            {
                priority = p;
                nextCamera = cBioCamera;
            }
        }

        // エラーチェック
        if(nextCamera == null)
        {// プレイヤーがどこのカメラの範囲にも入らない場所にいる
            Debug.LogError("プレイヤーがすべてのカメラの範囲外です");
            return;
        }

        // 切り替え
        if (_currentCamera != nextCamera)
        {
            _currentCamera.SetActiveCamera(false);  // 前のカメラをオフに
            _currentCamera = nextCamera;
            _currentCamera.SetActiveCamera(true);   // 次のカメラをオンに
            _playerMover.Set_tCamera(_currentCamera.Get_camera().transform);
        }

    }
}
