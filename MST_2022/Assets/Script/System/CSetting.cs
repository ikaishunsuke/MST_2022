using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSetting : MonoBehaviour
{
    GameObject _gPanel;

    // Start is called before the first frame update
    void Start()
    {
        _gPanel = GameObject.Find("Panel_Pause");
        _gPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CInputManager.GetButtonDown(INPUT_CODE.PAUSE))
        {
            if(!_gPanel.activeSelf)
            {
                _gPanel.SetActive(true);
            }
            else
            {
                _gPanel.SetActive(false);
            }
        }
    }
}
