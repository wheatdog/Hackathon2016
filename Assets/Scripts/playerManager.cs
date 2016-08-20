using UnityEngine;
using System.Collections;

public class playerManager : MonoBehaviour {

	public int health = 100;
	public string role = "None";
	public string item = "None";
	public int INT = 10;
	public int moveSpeed = 10;
	public int damage = 10;
	public UnityEngine.UI.Image healthBar;

	public void PlayerManager(){

	}

	public void changeHealth(int value){
		health -= value;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//healthBar.
	}


}
