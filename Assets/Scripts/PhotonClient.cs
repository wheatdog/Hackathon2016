using System;
using UnityEngine;
using System.Collections;

public class PhotonClient : Photon.MonoBehaviour {

	/// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
	public bool AutoConnect = true;
	public byte Version = 1;
	String classStr = "client:";
	/// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
	private bool ConnectInUpdate = true;
	public UnityEngine.UI.Text debugText;

	public virtual void Start()
	{
		PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms.
		joinRoom();
	}

		void OnReceivedRoomListUpdate(){
		Debug.Log("OnReceivedRoomListUpdate");

		}

	void Update () {
		if (PhotonNetwork.connectionStateDetailed != ClientState.Joined) {
			return;
		}
		if (PhotonNetwork.isMasterClient) { // 若是Master則處理遊戲邏輯
			//m_Animator = GetComponent<Animator>();
			//m_Body = GetComponent<Rigidbody2D>();
			Debug.Log("I am master");
			debugText.text = "I am master";
		}
		else { // 若是純客端則處理顯⽰
			foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
			{
				Debug.Log(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers +" / "+roomInfo.customProperties.Count);
				if (roomInfo.customProperties.ContainsKey("levelName"))
				{
					Debug.Log("found levelName");
					Debug.Log(roomInfo.customProperties + "levelName".ToString());//.ToString());
				}
			}
			debugText.text = "I am client";
			Debug.Log("I am client");
			if (Input.GetKey("w")) {
				photonView.RPC ("Move", PhotonTargets.MasterClient, 0);
			}
			else if (Input.GetKey("a")) {
				photonView.RPC ("Move", PhotonTargets.MasterClient, 1);
			}
			else if (Input.GetKey("d")) {
				photonView.RPC ("Move", PhotonTargets.MasterClient, 2);
			}
		}

	}


	public void joinRoom(){
		if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
		{
			debugText.text = classStr + "connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();";
			Debug.Log(classStr + "connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();" + SceneManagerHelper.ActiveSceneBuildIndex);
			ConnectInUpdate = false;
			PhotonNetwork.ConnectUsingSettings(""+Version.ToString());



		}
	}


	// below, we implement some callbacks of PUN
	// you can find PUN's callbacks in the class PunBehaviour or in enum PhotonNetworkingMessage
	public virtual void OnConnectedToMaster()
	{
		debugText.text = classStr + "OnConnectedToMaster";
		Debug.Log(classStr + "OnConnectedToMaster");


		PhotonNetwork.JoinRandomRoom();
	}

	public virtual void OnJoinedLobby()
	{
		debugText.text = classStr + "OnJoinedLobby()";
		Debug.Log(classStr + "OnJoinedLobby()");
		PhotonNetwork.JoinRandomRoom();
	}

	public virtual void OnPhotonRandomJoinFailed()
	{
		debugText.text = classStr + "OnPhotonRandomJoinFailed";
		Debug.Log(classStr + "OnPhotonRandomJoinFailed");
		PhotonNetwork.JoinRandomRoom();//rejoin TODO: find better way
	}

	// the following methods are implemented to give you some context. re-implement them as needed.
	public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		debugText.text = classStr + "Connect Failed, Cause: " + cause;
		Debug.LogError(classStr + "Connect Failed, Cause: " + cause);
	}

	public void OnJoinedRoom()
	{
		Debug.Log(classStr + "OnJoinedRoom");
		//UnityEngine.SceneManagement.SceneManager.LoadScene ("client");
		PhotonNetwork.LoadLevel("client");
	}
}
