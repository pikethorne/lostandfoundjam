using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class MultipickupPickup : MonoBehaviour
{
	public int min;
	public int max;
	public RandomPickupDropper randomPickupDropper;
	public float heat = 5;
	private void Awake()
	{
		randomPickupDropper = GetComponent<RandomPickupDropper>();
	}

	public void spawnPickups()
	{
		int pickupsToSpawn = UnityEngine.Random.Range(min, max);
		for(int i = 0;i<pickupsToSpawn;i++)
		{
			GameObject g = randomPickupDropper.spawnAPickup();
			int randomAngle = UnityEngine.Random.Range(0, 360);
			g.GetComponent<Rigidbody2D>()?.AddForce(new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle))*heat);
		}
	}

}
