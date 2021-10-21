using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==============================================================================
    [CCameraPostEffectMono.cs]
    ・ポストエフェクトでモノクロにするスクリプト
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

    // ポストエフェクトをかける
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, monoTone);
    }
}
