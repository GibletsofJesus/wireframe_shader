using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class material_editor : MonoBehaviour
{

    public List<Renderer> wireframeONLY = new List<Renderer>();
    public List<Renderer> wireframeGrad = new List<Renderer>();

    public List<GameObject> texturedObjects = new List<GameObject>();
    public List<GameObject> wireframeObjects = new List<GameObject>();

    public Material gradidentWire, wireOnly;
    public Texture gradientTex, solidTex;
    Texture baseTex,WFTex;

    float thickness = 1;
    float clippingIO, clipping1, clipping2, clipping3;
    Color tint = Color.black;
    Color baseTexCol = Color.white;
    bool wireframeModels;

    void Awake()
    {
        baseTex = gradientTex;
        setBaseTexAlpha(0);
        switchBaseTex(false);
        setTintR(.5f);
        setTintG(1);
        setTintB(1);
    }

    void Update()
    {
        if (wireframeModels)
        {
            foreach (GameObject go in texturedObjects)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in wireframeObjects)
            {
                go.SetActive(true);
                if (go.GetComponent<Renderer>())
                {
                    go.GetComponent<Renderer>().material.SetFloat("_Thickness", thickness);
                    go.GetComponent<Renderer>().material.SetColor("_Color", tint);
                    go.GetComponent<Renderer>().material.SetFloat("_ClippingOnOff", clippingIO);
                    go.GetComponent<Renderer>().material.SetFloat("_Clipping", clipping1);
                    go.GetComponent<Renderer>().material.SetFloat("_Clipping2", clipping2);
                    go.GetComponent<Renderer>().material.SetFloat("_Clipping3", clipping3);
                    go.GetComponent<Renderer>().material.SetTexture("_BaseTex", baseTex);
                    go.GetComponent<Renderer>().material.SetTexture("_MainTex", WFTex);
                    go.GetComponent<Renderer>().material.SetColor("_Tint", baseTexCol);
                }
                Renderer[] ren = go.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in ren)
                {
                    r.material.SetFloat("_Thickness", thickness);
                    r.material.SetColor("_Color", tint);
                    r.material.SetFloat("_ClippingOnOff", clippingIO);
                    r.material.SetFloat("_Clipping", clipping1);
                    r.material.SetFloat("_Clipping2", clipping2);
                    r.material.SetFloat("_Clipping3", clipping3);
                    r.material.SetTexture("_BaseTex", baseTex);
                    r.material.SetTexture("_MainTex", WFTex);
                    r.material.SetColor("_Tint", baseTexCol);
                }
            }

        }
        else
        {
            foreach (GameObject go in texturedObjects)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in wireframeObjects)
            {
                go.SetActive(false);
            }

            #region textured
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
                    wireframeONLY[i].materials[1].SetTexture("_BaseTex", baseTex);
                    wireframeONLY[i].materials[1].SetTexture("_MainTex", WFTex);
                    wireframeONLY[i].materials[1].SetColor("_Tint", baseTexCol);
                }
                else
                {
                    wireframeONLY[i].material.SetFloat("_Thickness", thickness);
                    wireframeONLY[i].material.SetColor("_Color", tint);
                    wireframeONLY[i].material.SetFloat("_ClippingOnOff", clippingIO);
                    wireframeONLY[i].material.SetFloat("_Clipping", clipping1);
                    wireframeONLY[i].material.SetFloat("_Clipping2", clipping2);
                    wireframeONLY[i].material.SetFloat("_Clipping3", clipping3);
                    wireframeONLY[i].material.SetTexture("_BaseTex", baseTex);
                    wireframeONLY[i].material.SetTexture("_MainTex", WFTex);
                    wireframeONLY[i].material.SetColor("_Tint", baseTexCol);
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
                    wireframeGrad[i].materials[1].SetTexture("_BaseTex", baseTex);
                    wireframeGrad[i].materials[1].SetTexture("_MainTex", WFTex);
                    wireframeGrad[i].materials[1].SetColor("_Tint", baseTexCol);
                }
                else
                {
                    wireframeGrad[i].material.SetFloat("_Thickness", thickness);
                    wireframeGrad[i].material.SetColor("_Color", tint);
                    wireframeGrad[i].material.SetFloat("_ClippingOnOff", clippingIO);
                    wireframeGrad[i].material.SetFloat("_Clipping", clipping1);
                    wireframeGrad[i].material.SetFloat("_Clipping2", clipping2);
                    wireframeGrad[i].material.SetFloat("_Clipping3", clipping3);
                    wireframeGrad[i].material.SetTexture("_BaseTex", baseTex);
                    wireframeGrad[i].material.SetTexture("_MainTex", WFTex);
                    wireframeGrad[i].material.SetColor("_Tint", baseTexCol);
                }
            }
            #endregion

        }
    }

    public void ObjectToggle(bool b)
    {
        wireframeModels = b;
    }

    #region wireframe options
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
#endregion

    #region clipping options
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
#endregion

    public void switchBaseTex(bool b)
    {
        if (!b)
        {
            baseTex = solidTex;
            WFTex = solidTex;
        }
        else
        {
            baseTex = gradientTex;
            WFTex = gradientTex;
        }
    }

    public void setBaseTexAlpha(float a)
    {
        Color newCol = baseTexCol;
        Debug.Log(newCol.a);
        newCol = Color.Lerp(Color.black, Color.white, a);
        newCol.a = a;
        Debug.Log(newCol.a);
        baseTexCol = newCol;
    }
}