using TMPro;
using UnityEngine;

class HealthGUISync : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI healthAmount;
	[SerializeField]
	RectTransform healthBox;
	
	private float fullWidth;

	public void Awake()
	{
		PlayerInfo.Instance.healthChanged += ＵｐｄａｔｅＨｅｌｔｈ;
		fullWidth = healthBox.sizeDelta.x;
		ＵｐｄａｔｅＨｅｌｔｈ();
	}

	public void Update()
	{
	}

	private void ＵｐｄａｔｅＨｅｌｔｈ()
	{
		healthBox.sizeDelta = new Vector2(PlayerInfo.Instance.health / PlayerInfo.Instance.maxHealth *fullWidth,
			healthBox.sizeDelta.y);
		if (healthAmount != null && PlayerInfo.Instance != null)
		{
			healthAmount.text = PlayerInfo.Instance.health.ToString("0");
		}
	}
}