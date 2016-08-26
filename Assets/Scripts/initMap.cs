using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

class MapEvent{
	public Action<Player> reactMethod;

	public MapEvent(Action<Player> rm){
		this.reactMethod = rm;
	}
	public void runEvent(Player player){
		reactMethod(player);
	}
}

[System.Serializable]
public class Item{
	public Item(){}
	public String name;
	public bool isLocked;
	public GameObject instance;
	public bool isUsed = false;
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

	//revive
	public float reviveDistance;
	public int canReviveNum;
	public List<int> reviveNum;

	//item
	public Item[] items;

	public GameObject boss, clear, chair;


	List<MapEvent> mapEvents = new List<MapEvent>();
	int inMonsterNum, inFinalNum;
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

		/*foreach (GameObject item in items) {
			Vector3 position = item.transform.position;
		}*/


	}

	void checkLockedItem(Player player){
		foreach (Item item in items) {
			if (item.isLocked && !item.isUsed && inCircle (player.gameObject, item.instance, 1.5) && player.playerItem != null) {
				Debug.Log ("unlock item" + item.name);
				player.UnsetItem ();
				item.instance.SetActive(false);
			}
		}
	}

	void checkItem(Player player){
		foreach (Item item in items) {
			if (!item.isLocked && !item.isUsed && inCircle (player.gameObject, item.instance, 1.5)) {
				Debug.Log ("pick up item" + item.name);
				player.SetItem (item);
				item.isUsed = true;
				item.instance.SetActive (false);

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

	void checkRevive(Player player){
		List<Player> deadPlayers = playerManager.getDeadPlayers ();
		int i = 0;
		foreach (Player deadPlayer in deadPlayers){
			if (inCircle (deadPlayer.gameObject, player.gameObject, reviveDistance)) {
				reviveNum[i]++;
				if (reviveNum[i] > canReviveNum) {
					Debug.Log ("revive" + deadPlayer.name);
					deadPlayer.revive ();
				}
			}
			i++;
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
		List<Player> deadPlayers = playerManager.getDeadPlayers ();
		reviveNum.Clear ();
		foreach (Player player in deadPlayers) {
			reviveNum.Add(0);
		}

		inMonsterNum = 0;
		inFinalNum = 0;
		foreach (MapEvent mapEvent in mapEvents) {
			foreach (Player player in playerManager.players) {
				if(!player.isDead())
					mapEvent.runEvent (player);
			}
		}
		//move players (by cellphone)
		//   set player move vector
		//update and show player stats
		foreach (Player player in playerManager.players) {
			player.updateHealth ();
			if (player.isDead ()) {
				mapEvents.Add (new MapEvent(checkRevive));
			}
			player.updateUI ();
		}


		//show game info
		//updateGameInfo();//score calculation data
	}



}
