using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DArts;

namespace DArts {


public class DABugDemoSceneMenu : MonoBehaviour {

	private static bool loaded	= false;
	private Text txt_scene;

	// Wake up! New Object, Singleton check ==========
	private void Awake() {
		if (loaded) {
			Destroy(this.gameObject);
		} else {
			DontDestroyOnLoad(this.gameObject);
			loaded = true;
		}
	}

	private void Start() {
		txt_scene = transform.Find("Canvas/Panel/TxtSceneName").GetComponent<Text>();
		txt_scene.text = SceneManager.GetActiveScene().name;
		SceneManager.activeSceneChanged += mySceneChanged; // Scene Listener
		buildFly();
	}

	// Destroy
	private void OnDestroy() {
		SceneManager.activeSceneChanged -= mySceneChanged; // Scene Listener
	}

	// Scene changed, build a new Fly ==========
	private void mySceneChanged(UnityEngine.SceneManagement.Scene s1, UnityEngine.SceneManagement.Scene s2) {
		txt_scene.text = SceneManager.GetActiveScene().name;
		buildFly();
	}

	// Build new Fly and send it this gameobject as its parent ==========
	private void buildFly() {
		GameObject fly = Instantiate(Resources.Load("DAFly_Demo", typeof(GameObject))) as GameObject;
		fly.GetComponent<DAFly_Demo>().init(this.gameObject);
	}

	// Change Scene ==========
	public void switchScene(string scene_name) {
		SceneManager.LoadScene(scene_name);
	}

}
}