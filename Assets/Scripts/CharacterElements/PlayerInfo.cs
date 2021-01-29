using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class for handling player info
/// </summary>
public class PlayerInfo
{
	private float baseAttack = 5;
	private float baseHealth = 50;
	private float baseShotSpeed = 0.5f;
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

	public float health { get; private set; }
	public float maxHealth { get; private set; }
	public Action healthChanged;
	public float damage { get; private set; }
	public float shotSpeed { get; private set; }
	public float speed { get; private set; }
	private List<StatUpgrade> statUpgrades;
	private List<CharacterPart> parts;
	

	PlayerInfo()
	{
		statUpgrades = new List<StatUpgrade>();
		parts = new List<CharacterPart>();
		InitializePlayerData();
	}

	public void InitializePlayerData()
	{

		maxHealth = statUpgrades.Sum(s => s.health) + baseHealth;
		health = statUpgrades.Sum(s => s.health) + baseHealth;
		damage = statUpgrades.Sum(s => s.damage) + baseAttack;
		shotSpeed = statUpgrades.Sum(s => s.fireRate) + baseShotSpeed;
		speed = statUpgrades.Sum(s => s.speed) + baseSpeed;
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

	}

	public void givePart(CharacterPart part)
	{
		parts.RemoveAll(p => p.partType == part.partType);
		statUpgrades.RemoveAll(s => (s as CharacterPart)?.partType == part.partType);
		part.EquipPart();
		parts.Add(part);
		giveStatUpgrade(part);
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
