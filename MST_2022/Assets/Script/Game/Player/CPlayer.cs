/*==============================================================================
    [CPlayer.cs]
    ・プレイヤー
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/


using UnityEngine;

public class CPlayer : MonoBehaviour
{
    [SerializeField] private IPlayerState _state = null;

    

    // Start is called before the first frame update
    void Start()
    {
        
        _state = GetComponentInChildren<CPlayerPickUpState>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Vector2.zero;

        if (CInputManager.GetButton(INPUT_CODE.LEFT))
        {
            dir.x -= 1.0f;
        }

        if (CInputManager.GetButton(INPUT_CODE.RIGHT))
        {
            dir.x += 1.0f;
        }

        if (CInputManager.GetButton(INPUT_CODE.UP))
        {
            dir.y += 1.0f;
        }

        if (CInputManager.GetButton(INPUT_CODE.DOWN))
        {
            dir.y -= 1.0f;
        }

        _state.Move(dir);

        if(CInputManager.GetButtonDown(INPUT_CODE.SELECT))
        {
            _state.Action();
        }
        
    }
}
