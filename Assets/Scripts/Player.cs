using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Role{
	public string name = "無職";
	public int trapNum = 1;//... the number added in trap, as does others 
	//”警察”、”護士”、”學者”、”忍者”

	public void changeRole(){

	}
};

public class Player : MonoBehaviour {
	public int healthMax = 100;
	public int health = 100;
	public Role role = new Role();
	public Item playerItem;
	public int INT;//intelligence
	public float moveSpeed;//TODO: run by speed
	public int damage;
	public int score;
	public Image healthBar;
	public Image roleUI;
	public Sprite deadSprite;

	public void revive(){
		health = healthMax;
	}

	public void move(Vector3 direction){
		if (isAlive()) {
			direction.Normalize ();
			direction.Scale (new Vector3 (moveSpeed, moveSpeed, 0f));
			this.gameObject.transform.Translate (direction);
		}
	}

	public void SetItem(Item item){
		playerItem = item;
	}
	public void UnsetItem(){
		playerItem = new Item();//None

		Image itemgrid = (Image)roleUI.transform.Find("ItemGrid").gameObject.GetComponent<Image>();
		Color itemgridColor = itemgrid.color;
		itemgridColor.a = 0;
		itemgrid.color = itemgridColor;
	}

	public void changeHealth(int value){
		if(isAlive())
			health += value;
		if(isDead())
			health = 0;
	}

	public void updateUI(){
		if (playerItem.instance != null && roleUI.gameObject != null) {
			//playerItem.instance.transform.position = roleUI.gameObject.transform.position;//move item to ui
			//set item image to ui
			Image itemgrid = (Image)roleUI.transform.Find("ItemGrid").gameObject.GetComponent<Image>();
			if (itemgrid == null) {
				Debug.Log ("item grid is null");
			}
			if (playerItem.instance.GetComponent<SpriteRenderer> () == null) {
				Debug.Log ("cannot get sprite renderer");
			}
			if (playerItem.instance.GetComponent<SpriteRenderer> ().sprite == null) {
				Debug.Log ("cannot get sprite");
			}
			itemgrid.sprite = playerItem.instance.GetComponent<SpriteRenderer> ().sprite;
			Color itemgridColor = itemgrid.color;
			itemgridColor.a = 1;
			itemgrid.color = itemgridColor;
		}
	}
	public void updateHealth(){
		if (isAlive())
			healthBar.transform.localScale = new Vector3((float)health / 100, 1, 1);
	}
	public void checkDead(){
		if (isDead()) {
			Debug.Log ("I am dead");
			GetComponent<SpriteRenderer>().sprite = deadSprite;
		}
	}
	public bool isAlive(){
		return health > 0;
	}
	public bool isDead(){
		return health <= 0;
	}

}
