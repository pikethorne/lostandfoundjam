using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class GameOverWinning : MonoBehaviour
{
	List<Image> images;
	List<Button> buttons;
	public void Awake()
	{
		images = GetComponentsInChildren<Image>().ToList();
		buttons = GetComponentsInChildren<Button>().ToList();
		GameState.Instance.gameStateChanged += UpdateGameDisplay;
	}
	

	public void UpdateGameDisplay()
	{
		if (GameState.Instance.state != GameState.State.Victory)
		{
			foreach (Image i in images)
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
			foreach (Image i in images)
			{
				i.enabled = true;
			}

			foreach (Button b in buttons)
			{
				b.enabled = true;
			}
		}
	}

	public void buttonUpdateState()
	{

		GameState.Instance.setState(GameState.State.Start);
	}
}
