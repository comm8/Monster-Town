using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ModelShaderHolder : MonoBehaviour
{
    public Renderer shader;

    public bool graymode;

    void Update()
    {
        if (graymode)
        {
            shader.material.SetFloat("_Employed", 0);
        }
        else
        {
            shader.material.SetFloat("_Employed", 1);
        }

    }
}
