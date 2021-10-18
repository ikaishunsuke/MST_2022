/*==============================================================================
	[CBioCamera.cs]
	・初期のバイオっぽいカメラ用 個別カメラスクリプト
--------------------------------------------------------------------------------
	2021.10.17 @Fujiwara Aiko
================================================================================
	History
		2021.10.17 Fujiwara Aiko
			スクリプト追加

/*============================================================================*/

using UnityEngine;
using UnityEngine.Events;

public class CBioCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private int _iPriority = 0;   // 優先順位　（数字が低い方が高い）

    private bool _isEnterPlayer = false;    // プレイヤーが範囲内にいるかどうか

    [HideInInspector] public UnityEvent _ueInOutPlayer = new UnityEvent();  // プレイヤーが出入りしたときに通知される

    // カメラのアクティブ状態を変更する
    public void SetActiveCamera(bool active)
    {
        _camera.enabled = active;
    }

    // プレイヤーが範囲内にいる時にTrue
    // _isEnterPlayerのゲッター
    public bool Get_isEnterPlayer()
    {
        return _isEnterPlayer;
    }

    // 優先順位を取得（数字が低い方が高い）
    // _iPriorityのゲッター
    public int Get_iPriority()
    {
        return _iPriority;
    }

    // カメラを取得
    // _cameraのゲッター
    public Camera Get_camera()
    {
        return _camera;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isEnterPlayer = true;
            _ueInOutPlayer.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isEnterPlayer = false;
            _ueInOutPlayer.Invoke();
        }
    }
}
