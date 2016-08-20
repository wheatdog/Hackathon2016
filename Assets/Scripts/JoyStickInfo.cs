using UnityEngine;
using System.Collections;

public class JoyStickInfo : Photon.MonoBehaviour {

    public VirtualJoyStick joystick;
	private PhotonView pv;

	[PunRPC]
    void Move(int id, Vector2 input)
	{
		Debug.Log (id.ToString() + ": " + input.ToString());
	}

    void Start() {
		joystick = GameObject.Find("Canvas").GetComponentInChildren<VirtualJoyStick>();
		pv = GetComponent<PhotonView> ();
    }

    void Update() {
        if (PhotonNetwork.connectionStateDetailed != ClientState.Joined) {
            return;
        }
        if (!PhotonNetwork.isMasterClient) {
			if (!joystick.input.Equals (Vector2.zero)) {
				pv.RPC ("Move", PhotonTargets.MasterClient, pv.owner.ID, joystick.input);
			}
        }
    }
}
