using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==============================================================================
    [CCameraPostEffectMono.cs]
    �E�|�X�g�G�t�F�N�g�Ń��m�N���ɂ���X�N���v�g
--------------------------------------------------------------------------------
    2021.10.19 @yanagisawa
================================================================================*/

public class CCameraPostEffectMono : MonoBehaviour
{
    public Material monoTone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �|�X�g�G�t�F�N�g��������
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, monoTone);
    }
}
