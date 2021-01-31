using System.Collections.Generic;
using UnityEngine;

class PostBossItemTakey : MonoBehaviour
{
	public static List<Sprite> spritesTaken;
	private RoomExit roomExit;
	public bool canTakey = true;
	public Animator animateytakey;

	void Awake()
	{
		animateytakey = GameObject.Find("ItemTakey").GetComponent<Animator>();
		roomExit = GetComponent<RoomExit>();
		if(spritesTaken == null || spritesTaken.Count >3)
		{
			spritesTaken = new List<Sprite>();
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player" && roomExit.activateTile && canTakey)
		{
			canTakey = false;
			animateytakey.Play("TakeyAnimation");
			SpriteRenderer spriteRendy;
			if (GameState.Instance.level == 0)
				spriteRendy = col.transform.Find("HeadSlot/Head").GetComponent<SpriteRenderer>();
			else if (GameState.Instance.level == 2)
				spriteRendy = col.transform.Find("BodySlot/Body").GetComponent<SpriteRenderer>();
			else if (GameState.Instance.level == 1)
				spriteRendy = col.transform.Find("GunSlot/Gun").GetComponent<SpriteRenderer>();
			else if (GameState.Instance.level == 3)
				spriteRendy = col.transform.Find("ArmSlot/Arm").GetComponent<SpriteRenderer>();
			else
				return;//silently failing if theres an error - the true sdk way
			spritesTaken.Add(spriteRendy.sprite);
			spriteRendy.color = new Color(1, 1, 1, 0.5f);
			GameState.Instance.level++;
		}
	}
}