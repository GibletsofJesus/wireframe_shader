using UnityEngine;
using System.Collections;

public class Parameter_management : MonoBehaviour {
	
	//Link to the animator
	//public Animator m_animator;
	public Renderer m_renderer;
	float r, g, b;

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
		m_renderer = GetComponent<Renderer>();
		m_renderer.material.shader = Shader.Find ("Custom/wireframe");
	}

	public void SetWFTint_R(float newVal)	{
		r = newVal;
		SetWFTint_RGB ();
	}

	public void SetWFTint_G(float newVal)	{
		g = newVal;
		SetWFTint_RGB ();
	}

	public void SetWFTint_B(float newVal)	{
		b = newVal;
		SetWFTint_RGB ();
	}

	void SetWFTint_RGB()	{
		Color newCol = new Color(r, g, b, 1);
		m_renderer.material.SetColor("_Color", newCol);
	}

	public void SetThickness(float newThick)	{
		m_renderer.material.SetFloat("_Thickness", newThick);
	}

	public void SetBaseTexAlpha( float newVal) {
		m_renderer.material.SetFloat("_Alpha", newVal);
	}

	public void ToggleClipping(bool newBool)	{
		if (newBool)
			m_renderer.material.SetFloat("_ClippingOnOff", 1.0f);
		else
			m_renderer.material.SetFloat("_ClippingOnOff", 0.0f);
	}

	public void SetClipping(float newVal)	{
		m_renderer.material.SetFloat("_Clipping", newVal);
	}

	public void SetClipping2(float newVal2)	{
		m_renderer.material.SetFloat("_Clipping2", newVal2);
	}

	public void SetClipping3(float newVal3)	{
		m_renderer.material.SetFloat("_Clipping3", newVal3);
	}
}
