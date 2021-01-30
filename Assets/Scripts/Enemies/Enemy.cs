using UnityEngine;

class Enemy : MonoBehaviour
{
	public bool isActive;
	public float health;
	public virtual void enemyHit(float damage)
	{
		health -= damage;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
	
}