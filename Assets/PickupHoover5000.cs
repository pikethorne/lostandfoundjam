using UnityEngine;

class PickupHoover5000: MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Pickup")
		{
			PickupHolder pickup = col.gameObject.GetComponent<PickupHolder>();
			if(pickup.itemPickup)
			{
				//shold probably add some check so you don't waste pickups? is anyone going to pick up 100 bombs? I sure hope not
				pickup.statUpgrade.activateInstantEffects();
			}
			else
			{
				PlayerInfo.Instance.giveStatUpgrade(pickup.statUpgrade);
			}

			Destroy(col.gameObject);
		}
	}
}
