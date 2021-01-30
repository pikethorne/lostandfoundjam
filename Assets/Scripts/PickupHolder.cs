using UnityEngine;

class PickupHolder : MonoBehaviour
{
	[SerializeField]
	public StatUpgrade statUpgrade;
	[SerializeField]
	public bool itemPickup;

	public float coolDownForPickup = 0.6f;
	private void Update()
	{
		if(coolDownForPickup>0)
		{
			coolDownForPickup -= Time.deltaTime;
		}
	}


}
