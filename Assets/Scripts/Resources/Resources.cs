using System;
using UnityEngine;


public class Resources : MonoBehaviour
{
	public int coins { get; private set; } = 0;
	public int keys { get; private set; } = 0;
	public int bombs { get; private set; } = 0;
	
	public bool addBomb()
	{
		if (bombs < 99)
		{
			bombs++;
			return true;
		}
		else
		{
			return false;
		}
	}
}
