using System.Collections.Generic;
using UnityEngine;

class FinalBoss : BossEnemy
{
	[SerializeField]
	GameObject bulletPrefab;
	[SerializeField]
	Sprite bulletModel;
	[SerializeField]
	List<Transform> gunShootPoints;
	[SerializeField]
	List<SpriteRenderer> placementPoints;
	Animator animator;
	bool weaponsSpawned;
	private void Awake()
	{
		animator = GetComponent<Animator>();
		InvokeRepeating("FireBullets", 0, 5);
	}

	private void Update()
	{
		if(isActive && !weaponsSpawned)
		{
			for (int i =0;i<placementPoints.Count;i++)
			{
				placementPoints[i].sprite = PostBossItemTakey.spritesTaken[i];
			}
			weaponsSpawned = true;
		}
	}

	private void FireBullets()
	{
		if (isActive)
		{
			Vector3 playerPos = PlayerInfo.Instance.playerControls.gameObject.transform.position;
			Transform gunShootPoint = gunShootPoints[Random.Range(0, gunShootPoints.Count)];
			Vector3 vecDiff = (playerPos - gunShootPoint.position).normalized;
			

			GameObject g = Instantiate(bulletPrefab, gunShootPoint.position, gunShootPoint.rotation);
			g.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vecDiff.y, vecDiff.x) * Mathf.Rad2Deg - 3f);
			g.GetComponent<Bullet>().InitializeBullet(bulletModel, 4, false, 0.08f);

			g = Instantiate(bulletPrefab, gunShootPoint.position, gunShootPoint.rotation);
			g.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vecDiff.y, vecDiff.x) * Mathf.Rad2Deg + 3f);
			g.GetComponent<Bullet>().InitializeBullet(bulletModel, 4, false, 0.08f);
		}
	}

	public override void enemyHit(float damage)
	{
		animator.Play("Enemyhit");
		health -= damage;
		if (health <= 0)
		{
			GameState.Instance.setState(GameState.State.Ending);
			Destroy(gameObject);
		}
	}
}
