// DAFly_Demo: Object to turn on/off parent's UI EventSystem

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DArts;

namespace DArts {


public class DAFly_Demo : MonoBehaviour {

	private GameObject parent_ev_sys;

	void Start () {
		if (parent_ev_sys != null && EventSystem.current == null) parent_ev_sys.SetActive(true);
	}
	
	public void init (GameObject parent_go) {
		this.name = "DAFly_Demo_" + GetInstanceID();
		Vector3 pos = parent_go.transform.position;
		transform.position = new Vector3(pos.x, pos.y, pos.z + .001f);
		parent_ev_sys = parent_go.transform.Find("EventSystem").gameObject;
	}

	private void OnDestroy() {
		if (parent_ev_sys != null) parent_ev_sys.SetActive(false);
	}


}
}