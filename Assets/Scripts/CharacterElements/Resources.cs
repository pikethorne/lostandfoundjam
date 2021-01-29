using System;
using UnityEngine;

/// <summary>
/// Class for handling resources level
/// </summary>
public class Resources
{
	public static Resources Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new Resources();
			}
			return instance;
		}
		private set
		{
			instance = value;
		}
	}

	private static Resources instance;

	public Action BombsUpdated;
	public Action KeysUpdated;
	public Action CoinsUpdated;

	public int coins { get; private set; } = 0;
	public int keys { get; private set; } = 0;
	public int bombs { get; private set; } = 0;
	
	/// <summary>
	/// Attempt to add a bomb
	/// </summary>
	/// <returns>If the bomb was added</returns>
	public bool addBomb(int valueAdded = 1)
	{
		if (bombs+valueAdded <= 99)
		{
			bombs+=valueAdded;
			BombsUpdated.Invoke();
			return true;
		}
		else
		{
			return false;
		}
	}

	
	/// <summary>
	/// Attempt to remove a bomb
	/// </summary>
	/// <returns>If the bomb was removed</returns>
	public bool removeBomb(int valueRemoved = 1)
	{
		if (bombs-valueRemoved >= 0)
		{
			bombs-=valueRemoved;
			BombsUpdated.Invoke();
			return true;
		}
		else
		{
			return false;
		}
	}
	   
	/// <summary>
	/// Attempt to add a key
	/// </summary>
	/// <returns>If the key was added</returns>
	public bool addKey(int valueAdded = 1)
	{
		if (keys + valueAdded <= 99)
		{
			keys+=valueAdded;
			KeysUpdated?.Invoke();
			return true;
		}
		else
		{
			return false;
		}
	}


	/// <summary>
	/// Attempt to remove a Key
	/// </summary>
	/// <returns>If the Key was removed</returns>
	public bool removeKey(int valueRemoved = 1)
	{
		if (keys - valueRemoved >= 0)
		{
			keys -= valueRemoved;
			KeysUpdated?.Invoke();
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Attempt to add a coin
	/// </summary>
	/// <returns>If the coin was added</returns>
	public bool addCoins(int valueAdded = 1)
	{
		if (coins + valueAdded <= 99)
		{
			coins+= valueAdded;
			CoinsUpdated.Invoke();
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Attempt to remove a Coin
	/// </summary>
	/// <returns>If the Coin was removed</returns>
	public bool removeCoin(int valueRemoved = 1)
	{
		if (coins - valueRemoved >= 0)
		{
			coins -= valueRemoved;
			CoinsUpdated.Invoke();
			return true;
		}
		else
		{
			return false;
		}
	}
}
