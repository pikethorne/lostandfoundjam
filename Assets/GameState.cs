using System;
using UnityEngine;

/// <summary>
/// Class for handling resources level
/// </summary>
public class GameState
{
	public static GameState Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameState();
			}
			return instance;
		}
		private set
		{
			instance = value;
		}
	}

	public enum State
	{
		Starting,
		InGame,
		Paused
	}

	private static GameState instance;
	public int level;
	public State state;
}
