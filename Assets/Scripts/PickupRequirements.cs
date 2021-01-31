using System;
using UnityEngine;

class PickupRequirements : MonoBehaviour
{
	[Serializable]
	public enum possibleRequirements
	{
		coins,
		keys,
		health
	}

	public possibleRequirements requirement;
	public int amount;

	public bool requirementsMet()
	{
		switch(requirement)
		{
			case possibleRequirements.coins:
				return Pickups.Instance.coins >= amount;
			case possibleRequirements.keys:
				return Pickups.Instance.keys >= amount;
			case possibleRequirements.health:
				return PlayerInfo.Instance.health > amount;
			default:
				return true;
		}
	}
	public void takeRequirements()
	{
		switch (requirement)
		{
			case possibleRequirements.coins:
				Pickups.Instance.removeCoin(amount);
				break;
			case possibleRequirements.keys:
				Pickups.Instance.removeKey(amount);
				break;
			case possibleRequirements.health:
				PlayerInfo.Instance.removeHealth(amount);
				break;
			default:
				break;
		}
	}
}
