/*==============================================================================
    [GoalFade.cs]
    �E�S�[�����Ƀt�F�[�h���Ȃ���V�[���J�ڂ����B
--------------------------------------------------------------------------------
    2021.10.18 @ �g�c���l
================================================================================
    History
        2021.10.18 �g�c���l
            fademanager�Ăяo���đJ�ڂ���
        2021.10.18 �������q
            ���[�h�����݂̂ɐ���
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalFade : MonoBehaviour
{
    public bool isGoal;
    private bool _isLoaded;     // ���[�h�ς݂�

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