using UnityEngine;
using System.Collections;

public class Parameter_management : MonoBehaviour {
	
	//Link to the animator
	public Animator m_animator;
	public Renderer m_renderer;

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
		m_renderer = GetComponent<Renderer>();
		m_renderer.material.shader = Shader.Find ("Custom/wireframe");
	}

	public void SetThickness(float newThick)	{
		m_renderer.material.SetFloat("_Thickness", newThick);
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

	public void ToggleAnimator(bool newBool)
	{
		m_animator.enabled = newBool;
	}

	public void ChangeAnimState(int newState){
		m_animator.SetInteger ("state", newState);
	}
	public void ChangeAnimState(float newState){
		m_animator.SetFloat ("state", newState);
	}
}
