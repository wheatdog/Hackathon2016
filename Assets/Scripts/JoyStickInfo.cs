using UnityEngine;
using System.Collections;

public class JoyStickInfo : Photon.MonoBehaviour {

    public VirtualJoyStick joystick;

    void Start() {
		joystick = GameObject.Find("Canvas").GetComponentInChildren<VirtualJoyStick>();
    }

    void Update() {
        if (PhotonNetwork.connectionStateDetailed != ClientState.Joined) {
            return;
        }
        if (!PhotonNetwork.isMasterClient) {
            GetComponent<PhotonView>().RPC ("Move", PhotonTargets.MasterClient, joystick.input);
        }
    }
}
