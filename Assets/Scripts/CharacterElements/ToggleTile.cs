using UnityEngine;

class ToggleTile : MonoBehaviour
{
	[SerializeField]
	public Sprite inactiveSprite;
	[SerializeField]
	public Sprite activeSprite;
	public bool activateTile;
	private SpriteRenderer spriteRenderer;
	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public virtual void UpdateTile()
	{
		if (activateTile)
		{
			spriteRenderer.sprite = activeSprite;
		}
		else
		{
			spriteRenderer.sprite = inactiveSprite;
		}
	}
}
