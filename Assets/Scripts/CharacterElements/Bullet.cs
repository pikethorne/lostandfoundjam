using UnityEngine;

class Bullet : MonoBehaviour
{
	private const float shotSpeed = 0.3f;
	private float damage = 0;
	public void InitializeBullet(Sprite sprite, float damage)
	{
		this.damage = damage;
		SpriteRenderer spriteComponent = GetComponent<SpriteRenderer>();
		if(spriteComponent != null)
		{
			spriteComponent.sprite = sprite;
		}

		Destroy(gameObject, 4);
	}

	private void FixedUpdate()
	{
		this.transform.position += this.transform.up * shotSpeed;
	}
}