using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class IpInput : MonoBehaviour {
	private NetworkClient myClient;

	private bool isAtStartup = true;
	private bool updateIp = false;

	public string ip = "127.0.0.1";
	public GameObject joystick;

	// Create a client and connect to the server port
	void Update()
	{
		if (isAtStartup) {
			SetupClient ();
		}

		if (updateIp) {
			myClient.Connect(ip, 8000);
			Debug.Log ("Try connect to new host!");
			updateIp = false;
		}
	}

	// client function
	public void OnConnected(NetworkMessage netMsg)
	{
		joystick.GetComponent<Image> ().color = Color.black;

	}

	public void SetupClient()
	{
		myClient = new NetworkClient();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);
		isAtStartup = false;
	}

	void OnGUI() {
		GUI.skin.textField.fontSize = 64;
		ip = GUI.TextField(new Rect(10, 10, 300, 80), ip, 30);
		updateIp = GUI.Button(new Rect (320, 10, 50, 80), "update");
	}
}
