using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class GameOver : MonoBehaviour
{
	List<Image> images;
	public void Awake()
	{
		images = GetComponentsInChildren<Image>().ToList();
		PlayerInfo.Instance.healthChanged += UpdateGameState;
	}

	public void UpdateGameState()
	{
		if(PlayerInfo.Instance.health<=0)
		{
			foreach(Image i in images)
			{
				i.enabled = true;
			}
		}
	}
}
