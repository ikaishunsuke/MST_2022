/*==============================================================================
    [GoalFade.cs]
    �E�S�[�����Ƀt�F�[�h���Ȃ���V�[���J�ڂ����B
--------------------------------------------------------------------------------
    2021.10.18 @ �g�c���l
================================================================================
    History
        2021.10.18 �g�c���l
            fademanager�Ăяo���đJ�ڂ���
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