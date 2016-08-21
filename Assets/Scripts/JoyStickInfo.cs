using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class JoyStickInfo : Photon.MonoBehaviour {

    public VirtualJoyStick joystick;
	public initMap gm;
	private PhotonView pv;
	private int frame_per_update = 3;
	private int update_counter = 0;

	[PunRPC]
    void Move(int id, Vector2 input)
	{
		Debug.Assert (gm != null);
		gm.inputs.stick = input;
		Debug.Log (id.ToString() + ": " + input.ToString());

	}

    void Start() {
		joystick = GameObject.Find("Canvas").GetComponentInChildren<VirtualJoyStick>();
		HashSet<GameObject> r = PhotonNetwork.FindGameObjectsWithComponent	(typeof(initMap));	
		Debug.Assert (r.Count > 0);
		Debug.Log ("I found initMap");
		foreach (GameObject i in r) {
			Debug.Log ("I found initMap");
			gm = i.GetComponent<initMap>();
		}
		Debug.Assert (gm);


		if (joystick) {
			if (joystick.isReference) {
				joystick = null;
			} 
			else {
				joystick.isReference = true;
			}
		}

		pv = GetComponent<PhotonView> ();
    }

    void Update() {
        if (PhotonNetwork.connectionStateDetailed != ClientState.Joined) {
            return;
        }
		update_counter = (update_counter + 1) % frame_per_update;
        if (!PhotonNetwork.isMasterClient) {
			if (joystick && !joystick.input.Equals (Vector2.zero)) {
				if (update_counter == 0) {
					pv.RPC ("Move", PhotonTargets.MasterClient, pv.owner.ID, joystick.input);
				}
			}
        }
    }
}
