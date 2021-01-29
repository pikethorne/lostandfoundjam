using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class for handling player info
/// </summary>
public class PlayerInfo
{
	private int baseAttack = 5;
	private int baseHealth = 50;
	private int baseShotSpeed = 5;
	private float baseSpeed = 3.5f;

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

	public PlayerControl playerControls;

	public float health { get; private set; } = 0;
	public float maxHealth { get; private set; } = 0;
	public Action healthChanged;
	private int damage;
	private int shotSpeed;
	private float speed;
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
		float pendingMaxHealth = statUpgrades.Sum(s => s.health) + baseHealth;
		if(pendingMaxHealth>maxHealth)
			health += pendingMaxHealth - maxHealth;
		else if(health>pendingMaxHealth)
			health = pendingMaxHealth;
		maxHealth = pendingMaxHealth;
		healthChanged.Invoke();
		damage = statUpgrades.Sum(s => s.damage)+ baseAttack;
		shotSpeed = statUpgrades.Sum(s => s.fireRate) + baseShotSpeed;
		speed = statUpgrades.Sum(s => s.speed) + baseSpeed;
		playerControls.speed = speed;

	}

	public void addHealth(int healthAmount)
	{
		if(health + healthAmount > maxHealth)
		{
			health = maxHealth;
		}
		else
		{
			health += healthAmount;
		}

		healthChanged?.Invoke();
	}


	public void removeHealth(float healthAmount)
	{
		if (health - healthAmount <0)
		{
			health = 0;
		}
		else
		{
			health -= healthAmount;
		}

		healthChanged?.Invoke();
	}
}
