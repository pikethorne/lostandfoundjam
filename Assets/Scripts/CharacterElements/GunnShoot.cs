using UnityEngine;

class GunnShoot : MonoBehaviour
{
	private float elapsedTime = 0;
	public Sprite bulletModel;
		
	public GameObject bulletPrefab;
	private void FixedUpdate()
	{
		if(Input.GetButton("Fire1"))
		{
			elapsedTime += Time.deltaTime;
			if(elapsedTime>PlayerInfo.Instance.shotSpeed)
			{
				elapsedTime -= PlayerInfo.Instance.shotSpeed;
				GameObject g = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
				g.AddComponent<Bullet>().InitializeBullet(bulletModel, PlayerInfo.Instance.damage);
			}
		}
	}
}