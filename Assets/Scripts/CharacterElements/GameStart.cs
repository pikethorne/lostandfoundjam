using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class GameStart : MonoBehaviour
{
	List<Image> images;
	List<Button> buttons;
	public void Awake()
	{
		images = GetComponentsInChildren<Image>().ToList();
		buttons = GetComponentsInChildren<Button>().ToList();
		GameState.Instance.gameStateChanged += UpdateGameState;
	}

	public void UpdateGameState()
	{
		if(GameState.Instance.state != GameState.State.Start &&  GameState.Instance.state != GameState.State.Starting)
		{
			foreach(Image i in images)
			{
				i.enabled = false;
			}

			foreach (Button b in buttons)
			{
				b.enabled = false;
			}
		}
		else
		{
			foreach(Image i in images)
			{
				i.enabled = true;
			}

			foreach (Button b in buttons)
			{
				b.enabled = true;
			}
		}
	}

	public void StartGame()
	{
		GameState.Instance.setState(GameState.State.Starting);
	}

	public void EndGame()
	{
		Application.Quit();
	}
}
