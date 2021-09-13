using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer_ATODEKESUYO : MonoBehaviour
{
    private GameObject _Player;

    // Start is called before the first frame update
    void Start()
    {
        this._Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            _Player.transform.position = new Vector3(_Player.transform.position.x - 1.0f,
                                                     _Player.transform.position.y, 
                                                     _Player.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _Player.transform.position = new Vector3(_Player.transform.position.x + 1.0f,
                                                     _Player.transform.position.y,
                                                     _Player.transform.position.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _Player.transform.position = new Vector3(_Player.transform.position.x,
                                                     _Player.transform.position.y,
                                                     _Player.transform.position.z + 1.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _Player.transform.position = new Vector3(_Player.transform.position.x,
                                                     _Player.transform.position.y,
                                                     _Player.transform.position.z - 1.0f);
        }
    }
}
