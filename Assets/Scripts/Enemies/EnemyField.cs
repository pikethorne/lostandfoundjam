using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class EnemyField : MonoBehaviour
{
	public bool enemiesActive;
	List<Enemy> enemies;
	private void Awake()
	{
		enemies = GetComponentsInChildren<Enemy>().ToList();
	}

	private void FixedUpdate()
	{
		if (enemies.Count(e => e!= null && e.enabled) == 0)
		{
			GetComponent<RandomPickupDropper>()?.spawnAPickup();
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