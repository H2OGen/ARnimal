using UnityEngine;
using System.Collections;
using DArts;

namespace DArts {

public class DABugDemoSpinCube : MonoBehaviour {

	float speed_x = 1.0f;
	float speed_y = 1.3f;
	float speed_z = 1.7f;
	int count = 0;
	int precision = 10000;

	// Use this for initialization
	void Start () {
		this.GetComponent<MeshRenderer>().material.color = Utils.hex2Color("CC9933");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(speed_x, speed_y, speed_z);
	}
	
	
	void OnMouseOver() {
		float x = Mathf.Round(transform.rotation.x * (precision))/(precision);
		float y = Mathf.Round(transform.rotation.y * (precision))/(precision);
		float z = Mathf.Round(transform.rotation.z * (precision))/(precision);
			
		count = (count<100000)? count + 1: 1;
		string str_1 = count.ToString() + ": " + x.ToString() + ", " + y.ToString() + ", " + z.ToString();
		DABug.Log(str_1);
	}

}
}