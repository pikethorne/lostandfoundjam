using UnityEngine;

class Bullet : MonoBehaviour
{
	private float shotSpeed = 0.3f;
	[SerializeField]
	private AudioClip hitSound;
	private float damage = 0;
	private bool isFromPlayer;
	public void InitializeBullet(Sprite sprite, float damage, bool isFromPlayer = true, float shotSpeed = 0.3f)
	{
		this.damage = damage;
		this.isFromPlayer = isFromPlayer;
		this.shotSpeed = shotSpeed;
		SpriteRenderer spriteComponent = GetComponentInChildren<SpriteRenderer>();
		if(spriteComponent != null)
		{
			spriteComponent.sprite = sprite;
		}

		Destroy(gameObject, 4);
	}

	private void FixedUpdate()
	{
		this.transform.position += this.transform.right * shotSpeed;
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (isFromPlayer)
			col.gameObject.GetComponent<Enemy>()?.enemyHit(damage);
		else if (col.gameObject.tag == "Player")
		{
			PlayerInfo.Instance.removeHealth(damage);
		}

		PlayerInfo.Instance.playerControls.audioPlayer.clip = hitSound;
		PlayerInfo.Instance.playerControls.audioPlayer.Play();

		Destroy(gameObject);
	}
}