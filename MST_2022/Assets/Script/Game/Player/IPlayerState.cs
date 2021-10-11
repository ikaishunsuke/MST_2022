/*==============================================================================
    [IPlayerState.cs]
    ・プレイヤー状態インターフェース
--------------------------------------------------------------------------------
    2021.10.11 @Fujiwara Aiko
================================================================================
    History
        2021.10.11 Fujiwara Aiko
            スクリプト追加
            
/*============================================================================*/

using UnityEngine;

public interface IPlayerState
{
    void Move(Vector2 direction);
    void Action();
}
