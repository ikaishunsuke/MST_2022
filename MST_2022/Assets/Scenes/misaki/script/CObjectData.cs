/*==============================================================================
    [CObjectData.cs]
    ���J���e�ɓo�^�ł���I�u�W�F�N�g�̂Ȃ�₩���
--------------------------------------------------------------------------------
    2021.10.11 @Author MISAKI SASAKI
================================================================================
    History
        2021.10.11 MISAKI SASAKI
            �J���e�ɓo�^�Ƃ��A�܂��J���e��ʈȊO�ŃJ���e�Ɋւ��
            �Ȃ�₩���͂������ɏ����Ă܂��B�J���e�̓����I�ȃA����
            CMedicalRecord.cs�ɂ����
        
/*============================================================================*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CObjectData : MonoBehaviour
{
    public GameObject _gPanel;
    public GameObject _gIconCanActive;
    private GameObject _gObj;
    private bool _bIsRecord;  // �P�x�ł��m�F���Ă���ꍇ��true
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 vec = new Vector3(960.0f, 540.0f, 0.0f);

        _gObj = Instantiate(_gIconCanActive, this.transform.position, Quaternion.identity);
        _gObj.transform.SetParent(_gPanel.transform);
        _gObj.transform.position = vec;
        _gObj.SetActive(false);

        _bIsRecord = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        // �v���C���[�^�O�����Ă���I�u�W�F�N�g�̏ꍇ
        if (other.gameObject.tag == "Player")
        {
            if (!_bIsRecord)
            {
                // �����A�C�R�����o�Ă��Ȃ��ꍇ��true�ɂ��ĕ\��
                if (!_gObj.activeSelf)
                {
                    _gObj.SetActive(true);
                }

                if (CInputManager.GetButtonDown(INPUT_CODE.SELECT))
                {
                    switch (this.name)
                    {
                        case "MedicalRecord_1":
                            CMedicalRecord.OnHint_1();
                            _bIsRecord = true;
                            _gObj.SetActive(false);

                            break;

                        case "MedicalRecord_2":
                            CMedicalRecord.OnHint_2();
                            _bIsRecord = true;
                            _gObj.SetActive(false);
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }


    private void OnTriggerExit()
    {
        // �����A�C�R�����\������Ă���ꍇ�͕\�������Ȃ��悤�ɂ���
        if (_gObj.activeSelf)
        {
            _gObj.SetActive(false);
        }
    }
}
