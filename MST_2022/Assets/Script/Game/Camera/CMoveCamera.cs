using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMoveCamera : MonoBehaviour
{
    private Transform _parent = null;
    [SerializeField] private float _fRotateSpeed = 60f;

    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.J) || CGamePadInputManager.GetButton(CGamePadInputManager.GAME_PAD_CODE.RSTICK_LEFT))
        {
            transform.RotateAround(_parent.position, Vector3.up, -Time.deltaTime * _fRotateSpeed);
        }
        if (Input.GetKey(KeyCode.L) || CGamePadInputManager.GetButton(CGamePadInputManager.GAME_PAD_CODE.RSTICK_RIGHT))
        {
            transform.RotateAround(_parent.position, Vector3.up, Time.deltaTime * _fRotateSpeed);
        }
        if (Input.GetKey(KeyCode.I))
        {

        }
        if (Input.GetKey(KeyCode.K))
        {

        }
    }
}
