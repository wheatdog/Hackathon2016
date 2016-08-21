using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

class MapEvent{
	//public GameObject gameObject;
	//public MonoBehaviour monobehavior;
	//public Player player;
	public Action<Player> reactMethod;

	//public Func<bool> checkCriteria;
	public MapEvent(Action<Player> rm){
		//this.player = pr;
		this.reactMethod = rm;
		//this.checkCriteria = cc;
	}
	public void runEvent(Player player){
		reactMethod(player);
	}
}

public class initMap : MonoBehaviour {
	public PlayerManager playerManager;
	public int gameTime = 100;

	//trap
	public float trapDistance;
	public GameObject[] traps;
	public int[] inTrapNums;
	public int[] requireInTrapNum;
	public GameObject[] lockdoor;


	public GameObject boss, clear, chair;
	public GameObject[] items;

	List<MapEvent> mapEvents = new List<MapEvent>();
	int inTrapNum, inMonsterNum, inFinalNum;
	bool isGameEnd;

	// Use this for initialization
	void Start () {
		initGameSettings();//settings
		initMapData();//game logics
		initPlayers();//healthbar
		startGame ();//TODO: time
	}

	void initGameSettings(){
		
	}

	void initMapData(){
		mapEvents.Add(new MapEvent(checkloseHealth));
		mapEvents.Add(new MapEvent(checkMonster));
		mapEvents.Add(new MapEvent(checkTrap));
		//mapEvents.Add(new MapEvent(checkGameEnd));
		mapEvents.Add(new MapEvent(checkItem));
		mapEvents.Add(new MapEvent(checkLockedItem));

	}

	void checkLockedItem(Player player){
		foreach (GameObject item in items) {
			if (inCircle (player.gameObject, item, 1.5) && player.itemObject != null) {
				Debug.Log ("unlock item");
				player.UnsetItem ();
				item.SetActive (false);
			}
		}
	}

	void checkItem(Player player){
		foreach (GameObject item in items) {
			if (inCircle (player.gameObject, item, 1.5)) {
				Debug.Log ("pick item");
				player.SetItem (item);
			}
		}
	}

	void checkGameEnd(Player player){
		if (inCircle(player.gameObject, traps[0], 2.2)) {
			Debug.Log ("in final");
			inFinalNum++;
			if (inFinalNum >= 1) {
				Debug.Log ("in final2");
				isGameEnd = true;
			}
		}
	}

	void checkTrap(Player player){
		//Debug.Log (trap.transform.position);
		//Debug.Log (player.gameObject.transform.position);
		//Debug.Log (Vector3.Distance(player.gameObject.transform.position, trap.transform.position));
		int i = 0;
		foreach (GameObject trap in traps){
			if (inCircle (player.gameObject, trap, trapDistance)) {
				inTrapNums[i]++;
				Debug.Log ("in trap" + i.ToString ());
				if (inTrapNums[i] >= requireInTrapNum[i]) {
					Debug.Log ("unlock" + i.ToString ());
					lockdoor[i].SetActive (false);
				}
			}
			i++;
		}
	}

	void checkMonster(Player player){
		//Debug.Log (boss.GetComponent<BoxCollider2D>().gameObject.transform.position);
		if (inCircle (player.gameObject, boss.GetComponent<BoxCollider2D>().gameObject, 3.5)) {
			inMonsterNum += 1;
			Debug.Log ("In monster");
			if (inMonsterNum >= 2)
				Debug.Log ("In monster2");
		}
	}

	void checkloseHealth(Player player){
		if (inCircle (chair, player.gameObject, 1)) {
			player.changeHealth (-1);
			Debug.Log ("losehealth" + player.health);
			//Debug.Log ("incircle, dis = " +  Vector3.Distance (v1, v2).ToString());
		}
	}
	bool inCircle(GameObject go, GameObject go1, double radius){//in manhatton
		Vector3 v1 = go.transform.position;
		Vector3 v2 = go1.transform.position;
		return Vector3.Distance (v1, v2) < radius;
	}
	bool inManhatton(GameObject go, GameObject go1, double xysum){//in manhatton
		Vector3 v1 = go.transform.position;
		Vector3 v2 = go1.transform.position;
		return Math.Abs(v1.x-v2.x) + Math.Abs(v1.y-v2.y)< xysum;
	}

	void initPlayers(){

	}

	void startGame(){

	}
	

	void FixedUpdate () {//handle game logics
		//check game end criteria
		if (isGameEnd){
			Debug.Log ("Game End");
			return;
		}
		//check interaction between map and player
		for(int i = 0; i < 3; i++)
			inTrapNums[i] = 0;
		inMonsterNum = 0;
		inFinalNum = 0;
		foreach (MapEvent mapEvent in mapEvents) {
			foreach (Player player in playerManager.players) {
				mapEvent.runEvent (player);
			}
		}
		//move players
		//   set player move vector
		//update and show player stats
		foreach (Player player in playerManager.players) {
			player.updateHealth ();
			player.checkDead ();
			player.updateUI ();
		}
		//show game info
		//updateGameInfo();//score calculation data
	}

}


