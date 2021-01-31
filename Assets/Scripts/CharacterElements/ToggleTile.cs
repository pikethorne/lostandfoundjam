using System.Collections.Generic;
using UnityEngine;

class ToggleTile : MonoBehaviour
{
	[SerializeField]
	public Sprite inactiveSprite;
	[SerializeField]
	public Sprite activeSprite;
	public bool activateTile;
	private List<SpriteRenderer> spriteRenderers;
	private void Start()
	{
		spriteRenderers = new List<SpriteRenderer>();
		spriteRenderers.Add(GetComponent<SpriteRenderer>());
		spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
		UpdateTile();
	}

	public virtual void UpdateTile()
	{
		foreach (SpriteRenderer spriteRenderer in spriteRenderers)
		{
			if (spriteRenderer != null)
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
}
