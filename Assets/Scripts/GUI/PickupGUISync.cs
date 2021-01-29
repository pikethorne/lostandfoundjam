using TMPro;
using UnityEngine;

class PickupGUISync : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI bombLevel;
	[SerializeField]
	TextMeshProUGUI keyLevel;
	[SerializeField]
	TextMeshProUGUI coinLevel;

	public void Awake()
	{
		Pickups.Instance.BombsUpdated += UpdateBombLevel;
		Pickups.Instance.KeysUpdated += UpdateKeyLevel;
		Pickups.Instance.CoinsUpdated += UpdateCoinLevel;
	}

	public void Update()
	{
		
	}

	/// <summary>
	/// Update the bomb level on the gui
	/// </summary>
	public void UpdateBombLevel()
	{
		bombLevel.text = "x" + Pickups.Instance.bombs.ToString("00");
	}

	/// <summary>
	/// Update the bomb level on the gui
	/// </summary>
	public void UpdateKeyLevel()
	{
		keyLevel.text = "x" + Pickups.Instance.keys.ToString("00");
	}

	/// <summary>
	/// Update the bomb level on the gui
	/// </summary>
	public void UpdateCoinLevel()
	{
		coinLevel.text = "x" + Pickups.Instance.coins.ToString("00");
	}
}