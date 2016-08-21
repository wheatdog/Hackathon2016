using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public int health = 100;
	public string role = "None";
	public string item = "None";
	public GameObject itemObject;
	public int INT = 10;//intelligence
	public int moveSpeed = 10;
	public int damage = 10;
	public UnityEngine.UI.Image healthBar;
	public int score = 0;
	public SpriteRenderer roleUI;

	public void SetItem(GameObject go){
		itemObject = go;
	}
	public void UnsetItem(){
		itemObject = null;
	}

	public void changeHealth(int value){
		health += value;
	}

	public void updateUI(){
		if (itemObject != null)
			itemObject.transform.position = roleUI.transform.position;
	}
	public void updateHealth(){
		healthBar.transform.localScale = new Vector3((float)health / 100, 1, 1);
	}
	public void checkDead(){
		if (health <= 0) {
			Debug.Log ("I am dead");
		}
	}

}
