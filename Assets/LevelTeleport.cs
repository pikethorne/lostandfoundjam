using System.Collections.Generic;
using UnityEngine;

class LevelTeleport : ToggleTile
{
	public static List<Sprite> spritesTaken;
	public BossItemDecideyTime bossItemTaken;
	public int level;
	void Awake()
	{
		
	}

	void Update()
	{
		if(bossItemTaken == null)
		{
			activateTile = true;
			UpdateTile();
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player" && activateTile)
		{
			col.transform.position = Vector3.zero + Vector3.right * 500 * (level+1);
		}
	}
}