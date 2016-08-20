using UnityEngine;
using System.Collections;

public class JoyStickInfo : Photon.MonoBehaviour {

    public VirtualJoyStick joystick;
	private PhotonView pv;
	private int frame_per_update = 3;
	private int update_counter = 0;

	[PunRPC]
    void Move(int id, Vector2 input)
	{
		Debug.Log (id.ToString() + ": " + input.ToString());
	}

    void Start() {
		joystick = GameObject.Find("Canvas").GetComponentInChildren<VirtualJoyStick>();

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
