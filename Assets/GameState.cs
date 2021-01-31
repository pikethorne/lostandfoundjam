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
		Start,
		Starting,
		InGame,
		Paused,
		Ending,
		Ended
	}

	private static GameState instance;
	public int level;
	public State state { get; private set; }


	public Action gameStateChanged;

	public void setState(State state)
	{
		this.state = state;
		gameStateChanged?.Invoke();
	}
}
