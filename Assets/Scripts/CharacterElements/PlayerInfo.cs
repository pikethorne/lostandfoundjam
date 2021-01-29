using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class for handling player info
/// </summary>
public class PlayerInfo
{
	public static PlayerInfo Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new PlayerInfo();
			}
			return instance;
		}
		private set
		{
			instance = value;
		}
	}

	private static PlayerInfo instance;

	public float health { get; private set; } = 0;
	public float maxHealth { get; private set; } = 0;
	public Action healthChanged;
	private int damage;
	private int shotSpeed;
	private int speed;
	private List<StatUpgrade> statUpgrades;

	PlayerInfo()
	{
		statUpgrades = new List<StatUpgrade>();
		maxHealth = 50;
		health = 50;
	}

	public void giveStatUpgrade(StatUpgrade statUpgrade)
	{
		statUpgrades.Add(statUpgrade);
		float pendingMaxHealth = statUpgrades.Sum(s => s.health);
		if(pendingMaxHealth>maxHealth)
			health += pendingMaxHealth - maxHealth;
		else if(health>pendingMaxHealth)
			health = pendingMaxHealth;
		maxHealth = pendingMaxHealth;
		healthChanged.Invoke();
		damage = statUpgrades.Sum(s => s.damage);
		shotSpeed = statUpgrades.Sum(s => s.fireRate);
		speed = statUpgrades.Sum(s => s.speed);
	}
}
