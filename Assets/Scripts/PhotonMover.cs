using UnityEngine;
using System.Collections;

public class PhotonMover : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		 
	}
	public GameObject player;
	Animator m_Animator;
	Rigidbody2D m_Body;
	public int Speed;

	void Awake()
	{
		m_Animator = GetComponent<Animator>();
		m_Body = GetComponent<Rigidbody2D>();
	}


	[PunRPC]
	void Move(int dir){
		//player;
		Debug.Log("move " + dir.ToString());
		Vector2 movementVelocity = m_Body.velocity;

		if( dir == 2)
		{
			movementVelocity.x = Speed;

		}
		else if(dir == 1)
		{
			movementVelocity.x = -Speed;
		}
		else
		{
			movementVelocity.x = 0;
		}

		m_Body.velocity = movementVelocity;
		//m_Animator.SetBool( "IsRunning", true);
	}

	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.connectionStateDetailed != ClientState.Joined) {
			return;
		}
		if (PhotonNetwork.isMasterClient) { // 若是Master則處理遊戲邏輯
			m_Animator = GetComponent<Animator>();
			m_Body = GetComponent<Rigidbody2D>();
			Debug.Log("I am master");
		}
		else { // 若是純客端則處理顯⽰
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
}
