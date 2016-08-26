using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	public Player[] players;
	public List<Player> getDeadPlayers(){
		List<Player> dp = new List<Player>();
		foreach (Player player in players) {
			if(player.isDead())
				dp.Add (player);
		}
		return dp;
	}
	public List<Player> getAlivePlayers(){
		List<Player> ap = new List<Player>();
		foreach (Player player in players) {
			if(!player.isDead())
				ap.Add (player);
		}
		return ap;
	}
}
