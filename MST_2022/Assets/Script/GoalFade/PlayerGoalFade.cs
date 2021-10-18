/*==============================================================================
    [GoalFade.cs]
    ・プレイヤーがゴール(tagg)に触れるとGoalFade.csのNextScene()を呼び出す。
--------------------------------------------------------------------------------
    2021.10.18 @ 吉田隼人
================================================================================
    History
        2021.10.18 吉田隼人
            NextScene()を呼び出してtrueにする。
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