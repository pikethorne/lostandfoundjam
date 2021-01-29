using System;
using UnityEngine;

/// <summary>
/// Class for handling player info
/// </summary>
public class StatUpgrade
{
	public int damage;
	public int speed;
	public float health;
	public int fireRate;

	public StatUpgrade(int damage = 0, int speed = 0, float health = 0, int fireRate = 0, int otherFeature = 0, string featureBonus = "")
	{
		this.damage = damage;
		this.speed = speed;
		this.health = health;
		this.fireRate = fireRate;

		if(featureBonus=="givesMoney")
		{
			Resources.Instance.addCoins(otherFeature);
		}
	}
}
