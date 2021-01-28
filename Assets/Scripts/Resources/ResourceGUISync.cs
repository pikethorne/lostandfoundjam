using TMPro;
using UnityEngine;

class ResourceGUISync : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI bombLevel;
	[SerializeField]
	TextMeshProUGUI keyLevel;
	[SerializeField]
	TextMeshProUGUI coinLevel;

	public void Awake()
	{
		Resources.Instance.BombsUpdated += UpdateBombLevel;
		Resources.Instance.KeysUpdated += UpdateKeyLevel;
		Resources.Instance.CoinsUpdated += UpdateCoinLevel;
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			Resources.Instance.addBomb();
		}

		if (Input.GetKeyDown(KeyCode.N))
		{
			Resources.Instance.addKey();
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			Resources.Instance.addCoins();
		}
	}

	/// <summary>
	/// Update the bomb level on the gui
	/// </summary>
	public void UpdateBombLevel()
	{
		bombLevel.text = "x" + Resources.Instance.bombs.ToString("00");
	}

	/// <summary>
	/// Update the bomb level on the gui
	/// </summary>
	public void UpdateKeyLevel()
	{
		keyLevel.text = "x" + Resources.Instance.keys.ToString("00");
	}

	/// <summary>
	/// Update the bomb level on the gui
	/// </summary>
	public void UpdateCoinLevel()
	{
		coinLevel.text = "x" + Resources.Instance.coins.ToString("00");
	}
}