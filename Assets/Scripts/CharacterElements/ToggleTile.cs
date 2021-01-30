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
		UpdateTile();
	}

	public virtual void UpdateTile()
	{
		if(spriteRenderer!=null)
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
}
