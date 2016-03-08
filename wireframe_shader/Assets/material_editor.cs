using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class material_editor : MonoBehaviour
{

    public List<Renderer> wireframeONLY = new List<Renderer>();
    public List<Renderer> wireframeGrad = new List<Renderer>();

    public Material gradidentWire, wireOnly;

    float thickness;
    float clippingIO, clipping1, clipping2, clipping3;
    Color tint;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < wireframeONLY.Count; i++)
        {
            if (wireframeONLY[i].materials.Length > 1)
            {
                wireframeONLY[i].materials[1].SetFloat("_Thickness", thickness);
                wireframeONLY[i].materials[1].SetColor("_Color", tint);
                wireframeONLY[i].materials[1].SetFloat("_ClippingOnOff", clippingIO);
                wireframeONLY[i].materials[1].SetFloat("_Clipping", clipping1);
                wireframeONLY[i].materials[1].SetFloat("_Clipping2", clipping2);
                wireframeONLY[i].materials[1].SetFloat("_Clipping3", clipping3);
            }
            else
            {
                wireframeONLY[i].material.SetFloat("_Thickness", thickness);
                wireframeONLY[i].material.SetColor("_Color", tint);
                wireframeONLY[i].material.SetFloat("_ClippingOnOff", clippingIO);
                wireframeONLY[i].material.SetFloat("_Clipping", clipping1);
                wireframeONLY[i].material.SetFloat("_Clipping2", clipping2);
                wireframeONLY[i].material.SetFloat("_Clipping3", clipping3);
            }
        }

        for (int i = 0; i < wireframeGrad.Count; i++)
        {
            if (wireframeGrad[i].materials.Length > 1)
            {
                wireframeGrad[i].materials[1].SetFloat("_Thickness", thickness);
                wireframeGrad[i].materials[1].SetColor("_Color", tint);
                wireframeGrad[i].materials[1].SetFloat("_ClippingOnOff", clippingIO);
                wireframeGrad[i].materials[1].SetFloat("_Clipping", clipping1);
                wireframeGrad[i].materials[1].SetFloat("_Clipping2", clipping2);
                wireframeGrad[i].materials[1].SetFloat("_Clipping3", clipping3);
            }
            else
            {
                wireframeGrad[i].material.SetFloat("_Thickness", thickness);
                wireframeGrad[i].material.SetColor("_Color", tint);
                wireframeGrad[i].material.SetFloat("_ClippingOnOff", clippingIO);
                wireframeGrad[i].material.SetFloat("_Clipping", clipping1);
                wireframeGrad[i].material.SetFloat("_Clipping2", clipping2);
                wireframeGrad[i].material.SetFloat("_Clipping3", clipping3);
            }
        }
    }

    public void setThickness(float f)
    {
        thickness = f;
    }
    public void setTintR(float f)
    {
        tint.r = f;
    }
    public void setTintG(float f)
    {
        tint.g = f;
    }
    public void setTintB(float f)
    {
        tint.b = f;
    }
    public void setTintA(float f)
    {
        tint.a = f;
    }
    public void setClippingIO(bool b)
    {
        if (b)
            clippingIO = 1;
        else
            clippingIO = 0;
    }

    public void setClipping1(float f)
    {
        clipping1 = f;
    }

    public void setClipping2(float f)
    {
        clipping2 = f;
    }

    public void setClipping3(float f)
    {
        clipping3 = f;
    }
}