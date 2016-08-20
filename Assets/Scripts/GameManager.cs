using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int player_count;
	public RemoteInput[] new_inputs, old_inputs;

	void Start () {
		new_inputs = new RemoteInput[player_count];
		old_inputs = new RemoteInput[player_count];
	}

	void Update () {
		// handle player movement
	
	}
}
