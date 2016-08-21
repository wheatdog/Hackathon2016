using UnityEngine;
using System.Collections;

public class playerInputReader : MonoBehaviour {

	public Player player;
	public float speedInverse;
	// Use this for initialization
	void Start () {
	}

	void OnCollision(){
		Debug.Log ("collision");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey("w")) {
			player.move (Vector3.up / speedInverse);
		}
		else if (Input.GetKey("a")) {
			player.move (Vector3.left/speedInverse);

		}
		else if (Input.GetKey("d")) {
			player.move  (Vector3.right/speedInverse);
		}
		else if (Input.GetKey("s")) {
			player.move  (Vector3.down/speedInverse);
		}
	}
}
