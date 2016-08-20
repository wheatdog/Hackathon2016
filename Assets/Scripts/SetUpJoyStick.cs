using UnityEngine;
using System.Collections;

public class SetUpJoyStick : Photon.MonoBehaviour {

    // Use this for initialization
    void Start () {
        PhotonNetwork.Instantiate("info", new Vector3( -4.5f, 5.5f, 0 ), Quaternion.identity, 0, null );
    }
}
