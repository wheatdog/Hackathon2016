using System;
using UnityEngine;
using System.Collections;

public class PhotonServer : Photon.MonoBehaviour {

	/// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
	public bool AutoConnect = true;
	public byte Version = 1;
	/// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
	private bool ConnectInUpdate = true;
	String classStr = "server:";
	int currentClientNum = 0;
	public ShowStatusWhenConnecting showStatusWhenConnecting;



	public virtual void Start()
	{
		PhotonNetwork.autoJoinLobby = true;    // we join randomly. always. no need to join a lobby to get the list of rooms.
		createRoom();
	}
	void OnReceivedRoomListUpdate(){
		RoomInfo[] roomInfos = PhotonNetwork.GetRoomList ();
		Debug.Log("to "  + roomInfos.Length);
		foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
		{
			Debug.Log(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers +" / "+roomInfo.customProperties.Count);
			if (roomInfo.customProperties.ContainsKey("levelName"))
			{
				Debug.Log("found levelName");
				Debug.Log(roomInfo.customProperties + "levelName".ToString());//.ToString());
			}
		}
	}

	/*
	PhotonPlayer PhotonNetwork.player [static], [get]
	The local PhotonPlayer. Always available and represents this player. CustomProperties can be set before entering
	a room and will be synced as well.
	8.21.4.33 PhotonPlayer [ ] PhotonNetwork.playerList [static], [get]
	The list of players in the current room, including the local player

	*/

	public void createRoom(){
		if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
		{
			Debug.Log(classStr + "connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();" + SceneManagerHelper.ActiveSceneBuildIndex);
			ConnectInUpdate = false;
			PhotonNetwork.ConnectUsingSettings(""+Version.ToString());
		}
	}


	void Update () {
		if (PhotonNetwork.connectionStateDetailed != ClientState.Joined) {
			return;
		}
		if (PhotonNetwork.isMasterClient) { // 若是Master則處理遊戲邏輯
			//m_Animator = GetComponent<Animator>();
			//m_Body = GetComponent<Rigidbody2D>();
			Debug.Log("I am master");
			foreach (PhotonPlayer player in PhotonNetwork.playerList) {
				Debug.Log (player.ToString ());
			}
			PhotonPlayer[] players = PhotonNetwork.playerList;
			if (players.Length > 1 && currentClientNum+1 < players.Length) {
				currentClientNum = players.Length-1;
				showStatusWhenConnecting.setClientName (currentClientNum, players [currentClientNum].ID.ToString());
			}
		}
		else { // 若是純客端則處理顯⽰
			Debug.Log("I am client");
			if (Input.GetKey("w")) {
				photonView.RPC ("rpctest", PhotonTargets.MasterClient);
			}
			else if (Input.GetKey("a")) {
				photonView.RPC ("rpctest", PhotonTargets.MasterClient);
			}
			else if (Input.GetKey("d")) {
				photonView.RPC ("rpctest", PhotonTargets.MasterClient);
			}
		}

	}


	[PunRPC]
	public void rpctest(){
		GUI.Button (new Rect(100, 100, 100, 100), "I am a button");
	}

	public void createFakeClient(){

	}


	// below, we implement some callbacks of PUN
	// you can find PUN's callbacks in the class PunBehaviour or in enum PhotonNetworkingMessage
	public virtual void OnConnectedToMaster()
	{
		Debug.Log(classStr + "OnConnectedToMaster");
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
		//PhotonNetwork.JoinRandomRoom();
	}

	public virtual void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
		TypedLobby typeLobby = PhotonNetwork.lobby;

		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, typeLobby);


		//PhotonNetwork.GetRoomList ();
	}

	public virtual void OnPhotonRandomJoinFailed()
	{
		Debug.Log(classStr + "OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
	}

	// the following methods are implemented to give you some context. re-implement them as needed.
	public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.LogError(classStr + "Connect Failed, Cause: " + cause);
	}

	public void OnJoinedRoom()
	{

		currentClientNum = 0;
		Debug.Log(classStr + "OnJoinedRoom");
	}
}
