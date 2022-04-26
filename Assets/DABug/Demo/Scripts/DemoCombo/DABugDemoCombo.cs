using UnityEngine;
using System.Collections;
using DArts;

namespace DArts {

public class DABugDemoCombo : MonoBehaviour {

	private int count = 0;




public void fireBrick() {

		count++;

		float px = Random.Range(2,5);
		float py = Random.Range(4,4);
		float pz = Random.Range(-1,1);

		float rx = Random.Range(0,360);
		float ry = Random.Range(0,360);
		float rz = Random.Range(0,360);

		float cr = Random.Range (0.5f,1f);
		float cg = Random.Range (0.5f,1f);
		float cb = Random.Range (0.5f,1f);

		DABug.Log(count + ": " + px + "," + py + "," + pz + " rot: " + rx + "," + ry + "," + rz); 

		GameObject brick = Instantiate(Resources.Load("Brick", typeof(GameObject))) as GameObject;
		brick.name = "brick_" + count.ToString();
		brick.transform.position = new Vector3(px,py,pz);
		brick.transform.Rotate(rx,ry,rz);
		brick.GetComponent<MeshRenderer>().material.color = new Color(cr, cg, cb, 1);

		Destroy(brick, 3.0f);
	}


}
}