/*==============================================================================
    [GoalFade.cs]
    ・ゴール時にフェードしながらシーン遷移するよ。
--------------------------------------------------------------------------------
    2021.10.18 @ 吉田隼人
================================================================================
    History
        2021.10.18 吉田隼人
            fademanager呼び出して遷移する
        2021.10.18 藤原愛子
            ロードを一回のみに制限
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalFade : MonoBehaviour
{
    public bool isGoal;
    private bool _isLoaded;     // ロード済みか

    void Start()
    {
        isGoal = false;
        _isLoaded = false;
    }

    public void NextScene()
    {
        isGoal = true;
    }

    void Update()
    {
        if (isGoal == true)
        {
            if (_isLoaded == false)
            {
                FadeManager.Instance.LoadScene("AfterTheGoal", 2.0f);
                _isLoaded = true;
            }

        }
    }
}