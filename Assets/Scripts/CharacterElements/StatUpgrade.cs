using System;
using UnityEngine;

/// <summary>
/// Class for handling stat improvements and pickups
/// </summary>
[Serializable]
public class StatUpgrade
{
	public enum InstantEffect
	{
		None,
		Coins,
		Bombs,
		Keys,
		Health
	}

	public float damage;
	public float speed;
	public float health;
	public float fireRate;
	public int instantEffectValue;
	public InstantEffect instantEffect;

	public StatUpgrade(float damage = 0, float speed = 0, float health = 0, float fireRate = 0, int instantEffectValue = 0, InstantEffect instantEffect = InstantEffect.None)
	{
		this.damage = damage;
		this.speed = speed;
		this.health = health;
		this.fireRate = fireRate;
		this.instantEffectValue = instantEffectValue;
		this.instantEffect = instantEffect;

	}

	public void activateInstantEffects()
	{
		if (instantEffect == InstantEffect.Coins)
		{
			Pickups.Instance.addCoins(instantEffectValue);
		}
		else if (instantEffect == InstantEffect.Bombs)
		{
			Pickups.Instance.addBomb(instantEffectValue);
		}
		else if (instantEffect == InstantEffect.Keys)
		{
			Pickups.Instance.addKey(instantEffectValue);
		}
		else if (instantEffect == InstantEffect.Health)
		{
			PlayerInfo.Instance.addHealth(instantEffectValue);
		}
	}
}
