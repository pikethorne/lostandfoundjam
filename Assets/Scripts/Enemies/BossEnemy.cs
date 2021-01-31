using UnityEngine;

class BossEnemy : Enemy
{
	public RoomExit bossExit;

	public override void enemyHit(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			bossExit.activateTile = true;
			bossExit.UpdateTile();
			Destroy(gameObject);
		}
	}
}