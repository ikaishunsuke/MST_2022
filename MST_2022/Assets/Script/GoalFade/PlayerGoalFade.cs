/*==============================================================================
    [GoalFade.cs]
    �E�v���C���[���S�[��(tagg)�ɐG����GoalFade.cs��NextScene()���Ăяo���B
--------------------------------------------------------------------------------
    2021.10.18 @ �g�c���l
================================================================================
    History
        2021.10.18 �g�c���l
            NextScene()���Ăяo����true�ɂ���B
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoalFade : MonoBehaviour
{
   
    GameObject PlayerGoalObject;

    void Start()
    {
        PlayerGoalObject = GameObject.Find("FadeManager");
    }

    void Update()
    {
       
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Goal")
        {
            PlayerGoalObject.GetComponent<GoalFade>().NextScene();
        }
    }
}