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

        2021.10.18 AIKO FUJIWARA
            �d���u������V���������鏈����ǉ�
            ��������S�[���o�����ǉ�
            
/*============================================================================*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBalance : MonoBehaviour
{
    [SerializeField] private static GameObject[] _gPlate = new GameObject[4];    // �������̓V���̎M�@�ƕ��i���i�v���g�i�K��3���Ă��Ƃł���Ƃ��j
    [SerializeField] private GameObject _gComparisonPlate;  // �����Ȃ����̓V���̎M
    private static float _fDifference_Y;
    [SerializeField] private GameObject _gGoal = null;  // �S�[���i�V��������ɏo���j

    private static CBalance _instance = null;  // Singleton��
    public static CBalance Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CBalance>();
            }
            return _instance;
        }
    }

    
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

    // Dissapear ������X�^�[�g
    public void Disappear()
    {
        StartCoroutine(DisappearAnim());
    }

    // DisappearAnim ������A�j���[�V����
    private IEnumerator DisappearAnim()
    {

        // �q�I�u�W�F�N�g��Renderer������Ă��ē����x��������
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        for(float time = 0.0f; time < 3.0f; time += Time.deltaTime)
        {
            foreach(Renderer renderer in renderers)
            {
                Color color = renderer.material.color;
                color.a = ((3.0f - time) / 3.0f) * 2.0f;
                if (color.a > 1.0f) color.a = 1.0f;
                renderer.material.color = color;
                renderer.material.SetFloat("_Threshold", time / 3.0f);
            }
            yield return null;
        }
        

        // �S�[�����o��������
        _gGoal.SetActive(true);

        // �V��������
        Destroy(gameObject);
    }
}
