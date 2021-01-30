using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class EnemyField : MonoBehaviour
{
	public bool enemiesActive;
	List<RoomExit> roomExits;
	List<Enemy> enemies;
	private void Awake()
	{
		roomExits = GetComponentsInChildren<RoomExit>().ToList();
		enemies = GetComponentsInChildren<Enemy>().ToList();
	}

	private void FixedUpdate()
	{
		if (enemies.Count(e => e!= null && e.enabled) == 0)
		{
			roomExits.ForEach(a =>{ a.activateTile = true; a.UpdateTile(); });
			GetComponent<RandomPickupDropper>()?.spawnAPopup();
			Destroy(this);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			enemiesActive = true;
			enemies.ForEach(e => e.isActive = true);
		}
	}

	private void OnTriggerLeave2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			enemiesActive = false;
			enemies.ForEach(e => e.isActive = false);
		}
	}
}