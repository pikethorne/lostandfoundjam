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

	public void spawnAPopup()
	{
		int max = pickups.Sum(pick => pick.amount);
		int value = (int)UnityEngine.Random.Range(1, max);
		foreach(Pickups pickup in pickups)
		{
			value -= pickup.amount;
			if(value<=0)
			{
				if (pickup.gameObject != null)
					Instantiate(pickup.gameObject);
				break;
			}
		}
		
	}
}
