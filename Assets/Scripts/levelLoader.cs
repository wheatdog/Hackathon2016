using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class levelLoader : MonoBehaviour {

	public Text text;
	// Use this for initialization
	void Start () {
		// if (SceneManager.GetActiveScene ().name == "menu")
			// iTween.MoveBy(text.gameObject,iTween.Hash("x",400,"time", 1.5f, "easeType","easeInOutQuad"));
	}

	// Update is called once per frame
	void Update () {

	}

	public void moveObject(GameObject gameObject){

	}

	public void loadLevel(string str){
		Debug.Log ("loadlevel " + str);
		SceneManager.LoadScene (str);
	}

}
