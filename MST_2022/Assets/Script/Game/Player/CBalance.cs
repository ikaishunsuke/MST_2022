/*==============================================================================
    [CBalance.cs]
    �E�V�����������B�ł������ł͌Ă΂Ȃ���BPutSpace�ŌĂ�ł��B
--------------------------------------------------------------------------------
    2021.10.14 @ MISAKI SASAKI
================================================================================
    History
        2021.10.14 MISAKI SASAKI
            Start()�̒��g�Ȃ񂾂��ǂ��A��ŕς��邩��Ђ��ǂ����������Ă邯��
            �����ꂽ���Ƃ��v�����肵�܂����ł��n�C�B
            �����Ă��I�I�I�I�I�v���g�̒i�K�Ȃ񂾂���I�I�I
            ���i�����Ƃ����I�I�I�ς���Ă��邶���I�I�I�I
            ���Ă���������i�����ƒ�������B�B�j

            ������
            �V���̉��_�̕��������Ƃł����Ɠ������񂾂�
            �������
            
/*============================================================================*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBalance : MonoBehaviour
{
    [SerializeField] private static GameObject[] _gPlate = new GameObject[4];    // �������̓V���̎M�@�ƕ��i���i�v���g�i�K��3���Ă��Ƃł���Ƃ��j
    [SerializeField] private GameObject _gComparisonPlate;  // �����Ȃ����̓V���̎M
    private static float _fDifference_Y;
    
    // Start is called before the first frame update
    void Start()
    {
      
        //=================== �������Ƃŕς���
        _gPlate[0] = GameObject.Find("cylinder2");
        _gPlate[1] = GameObject.Find("Cube (2)");
        _gPlate[2] = GameObject.Find("Cube (3)");
        _gPlate[3] = GameObject.Find("PutSpace");
        //==================== �����܂�


        _fDifference_Y = _gComparisonPlate.transform.position.y - _gPlate[0].transform.position.y;
        _fDifference_Y = _fDifference_Y / 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void PlateUp()
    { 
        for(int index = 0; index < _gPlate.Length; index++)
        {
            Vector3 vec = _gPlate[index].transform.position;
            vec.y += _fDifference_Y;
            _gPlate[index].transform.position = vec;
        }
    }
    public static void PlateDown()
    {
        for (int index = 0; index < _gPlate.Length; index++)
        {
            Vector3 vec = _gPlate[index].transform.position;
            vec.y -= _fDifference_Y;
            _gPlate[index].transform.position = vec;
        }
    }
}
