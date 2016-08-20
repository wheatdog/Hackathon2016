using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class initMap : MonoBehaviour {

	PhotonPlayer[] players;
	bool isGameEnd;
	public int time = 100;
//	public List<MapEvent> mapEvents = new List<MapEvent>();

	// Use this for initialization
	void Start () {
		initGameSettings();//settings
		//initMapData();//game logics
		initPlayers();//healthbar
		startGame ();//TODO: time
	}

	void initGameSettings(){
		//
	}

	void initMapData(){
	//	mapEvents.Add(new MapEvent(gameObject, loseHealth));
	}
	/*class MapEvent{
		public GameObject gameObject;
		public Action<GameObject> reactMethod; 
		public void MapEvent(GameObject gameObject, Action<GameObject> reactMethod){
			this.gameObject = gameObject;
			this.reactMethod = reactMethod;
		}
		public void runEvent(){
			reactMethod (gameObject);
		}
	}

	void loseHealth(GameObject gameObject){
		PlayerManger playerManager = (PlayerManager)gameObject;
		playerManager.changeHealth (-10);
	}*/

	void initPlayers(){

	}

	void startGame(){

	}
	
	// Update is called once per frame
	void Update () {//handle game logics
		//if (timeUp ()) {
		//
		//}

		//move players
		//check interaction between map and player
		//update and show player stats

	}
}
