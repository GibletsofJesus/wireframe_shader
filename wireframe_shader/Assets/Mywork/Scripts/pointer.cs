using UnityEngine;
using System.Collections;

public class pointer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rot = Camera.main.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, -rot.y, transform.rotation.eulerAngles.z));
    }
}
