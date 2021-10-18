/*==============================================================================
    [GoalFade.cs]
    ・ゴール時にフェードしながらシーン遷移するよ。
--------------------------------------------------------------------------------
    2021.10.18 @ 吉田隼人
================================================================================
    History
        2021.10.18 吉田隼人
            fademanager呼び出して遷移する
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalFade : MonoBehaviour
{
    public bool isGoal;

    void Start()
    {
        isGoal = false;
    }

    public void NextScene()
    {
        isGoal = true;
    }

    void Update()
    {
        if (isGoal == true)
        {
            {
                FadeManager.Instance.LoadScene("AfterTheGoal", 2.0f);
            }

        }
    }
}