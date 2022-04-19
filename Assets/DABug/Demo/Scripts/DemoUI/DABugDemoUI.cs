using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DArts;

namespace DArts {


public class DABugDemoUI : MonoBehaviour {

	Slider slider;
	Transform pic_tf;
	Text results;

	void Start() {
		slider = GameObject.Find("Slider").GetComponent<Slider>();
		pic_tf = GameObject.Find ("ImgMain").transform;
		results = GameObject.Find("TxtResults").GetComponent<Text>();
		setPicSize();
	}


	public void setPicSize() {
		float new_val	= slider.value;
		int int_val		= (int) Mathf.Floor(new_val*100); 

		DABug.Log("image size: " + new_val.ToString());
		pic_tf.localScale = new Vector3(new_val, new_val, 1f);
		results.text = int_val.ToString() + " %";
	}




}
}