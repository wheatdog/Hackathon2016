using UnityEngine;
using System.Collections;

public class playerInputReader : MonoBehaviour {
	public GameObject player;
	public float speedInverse;
	public CharacterController characterController;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("w")) {
			Debug.Log ("w");
			Debug.Log (Vector3.up);
			characterController.SimpleMove (Vector3.up/speedInverse);
			//player.transform.Translate (Vector3.up/speedInverse);
		}
		else if (Input.GetKey("a")) {
			Debug.Log ("a");
			characterController.Move (Vector3.down/speedInverse);
			//characterController.SimpleMove (Vector3.down/speedInverse);

		}
		else if (Input.GetKey("d")) {
			Debug.Log ("d");
			characterController.SimpleMove (Vector3.left/speedInverse);
		}
		else if (Input.GetKey("s")) {
			Debug.Log ("s");
			characterController.SimpleMove(Vector3.right/speedInverse);
		}
	}
}
