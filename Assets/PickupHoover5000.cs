using UnityEngine;

class PickupHoover5000: MonoBehaviour
{
	[SerializeField]
	AudioClip pickupClip;
	[SerializeField]
	AudioClip upgradeClip;

	AudioSource soundPlayer;
	private void Awake()
	{
		soundPlayer = GetComponent<AudioSource>();
		soundPlayer.playOnAwake = false;
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Pickup")
		{
			
			PickupHolder pickup = col.gameObject.GetComponent<PickupHolder>();
			if(pickup.itemPickup)
			{
				//shold probably add some check so you don't waste pickups? is anyone going to pick up 100 bombs? I sure hope not
				pickup.statUpgrade.activateInstantEffects();
				soundPlayer.clip = pickupClip;
				soundPlayer.Play();
			}
			else
			{
				PlayerInfo.Instance.giveStatUpgrade(pickup.statUpgrade);
			}

			Destroy(col.gameObject);
		}
		else if(col.gameObject.tag == "Part")
		{
			CharacterPartHolder pickup = col.gameObject.GetComponent<CharacterPartHolder>();
			
			PlayerInfo.Instance.givePart(pickup.characterPart);

			Destroy(col.gameObject);
		}
	}
}
