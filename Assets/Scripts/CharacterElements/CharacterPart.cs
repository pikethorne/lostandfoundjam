using System;
using UnityEngine;

/// <summary>
/// Class for handling player info
/// </summary>
[Serializable]
public class CharacterPart : StatUpgrade
{
	public enum PartType
	{
		head,
		body,
		arm,
		gun
	}

	public PartType partType;
	public Sprite sprite;
	public Sprite additionalModel;
	public float rotation;
	public Vector3 offset;
	public float scale = 1;

	public CharacterPart(PartType type, Sprite part, Sprite additionalModel,float rotation = 0, float scale = 1, 
		float damage = 0, float speed = 0, float health = 0, float fireRate = 0, int otherFeature = 0)
		: base(damage,speed,health,fireRate,otherFeature, InstantEffect.None)
	{
		this.partType = type;
		this.sprite = part;
		this.additionalModel = additionalModel;
	}

	public void EquipPart()
	{
		switch (partType)
		{
			case PartType.head:
				SpriteRenderer headSprite = PlayerInfo.Instance.playerControls.HeadRef.GetComponentInChildren<SpriteRenderer>();
				headSprite.sprite = sprite;
				headSprite.color = new Color(1, 1, 1);
				headSprite.transform.localPosition = offset;
				headSprite.transform.localScale = new Vector3(scale, scale, scale);
				headSprite.transform.localRotation = Quaternion.Euler(0, 0, rotation);
				break;
			case PartType.body:
				SpriteRenderer bodySprite = PlayerInfo.Instance.playerControls.BodyRef.GetComponentInChildren<SpriteRenderer>();
				bodySprite.sprite = sprite;
				bodySprite.color = new Color(1, 1, 1);

				bodySprite.transform.localPosition = offset;
				bodySprite.transform.localScale = new Vector3(scale, scale, scale);
				bodySprite.transform.localRotation = Quaternion.Euler(0, 0, rotation);
				break;
			case PartType.arm:
				SpriteRenderer armSprite = PlayerInfo.Instance.playerControls.ArmRef.GetComponentInChildren<SpriteRenderer>();
				armSprite.sprite = sprite;
				armSprite.color = new Color(1, 1, 1);
				armSprite.transform.localPosition = offset;
				armSprite.transform.localScale = new Vector3(scale, scale, scale);
				armSprite.transform.localRotation = Quaternion.Euler(0, 0, rotation);
				break;case PartType.gun:
				if (additionalModel != null)
				{
					PlayerInfo.Instance.playerControls.GunRef.GetComponentInChildren<GunnShoot>().bulletModel = additionalModel;
				}
				SpriteRenderer gunSprite = PlayerInfo.Instance.playerControls.GunRef.GetComponentInChildren<SpriteRenderer>();
				gunSprite.sprite = sprite;
				gunSprite.color = new Color(1, 1, 1);
				gunSprite.transform.localPosition = offset;
				gunSprite.transform.localScale = new Vector3(scale, scale, scale);
				gunSprite.transform.localRotation = Quaternion.Euler(0, 0, rotation);
				break;
		}
	}
}
