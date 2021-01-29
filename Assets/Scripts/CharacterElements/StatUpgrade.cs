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
		Keys
	}

	public int damage;
	public float speed;
	public float health;
	public int fireRate;
	public int instantEffectValue;
	public InstantEffect instantEffect;

	public StatUpgrade(int damage = 0, float speed = 0, float health = 0, int fireRate = 0, int instantEffectValue = 0, InstantEffect instantEffect = InstantEffect.None)
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
			Resources.Instance.addCoins(instantEffectValue);
		}
		else if (instantEffect == InstantEffect.Bombs)
		{
			Resources.Instance.addBomb(instantEffectValue);
		}
		else if (instantEffect == InstantEffect.Keys)
		{
			Resources.Instance.addKey(instantEffectValue);
		}
	}
}
