using UnityEngine;
using System.Collections;

public class playerInputReader : MonoBehaviour {

	public Player player;
	public float speedInverse;
	public char[] inputKey;
	// Use this for initialization
	void Start () {
	}

	void OnCollision(){
		Debug.Log ("collision");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(inputKey[0]+"")) {
			player.move (Vector3.up);
		}
		else if (Input.GetKey(inputKey[1]+"")) {
			player.move (Vector3.left);

		}
		else if (Input.GetKey(inputKey[2]+"")) {
			player.move  (Vector3.down);
		}
		else if (Input.GetKey(inputKey[3]+"")) {
			player.move  (Vector3.right);
		}
	}
}
