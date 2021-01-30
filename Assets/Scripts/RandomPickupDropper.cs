using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
class RandomPickupDropper : MonoBehaviour
{
	[Serializable]
	public class Pickups
	{
		public GameObject gameObject;
		public int amount;
	}

	[SerializeField]
	public List<Pickups> pickups;

	private void Awake()
	{
	}

	public GameObject spawnAPickup(bool instaSpawn = true)
	{
		int max = pickups.Sum(pick => pick.amount);
		int value = UnityEngine.Random.Range(1, max);
		foreach(Pickups pickup in pickups)
		{
			value -= pickup.amount;
			if(value<=0)
			{
				if (pickup.gameObject != null)
				{
					return Instantiate(pickup.gameObject,transform.position,transform.rotation);
				}
			}
		}
		return null;
	}
}
