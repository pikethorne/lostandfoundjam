using System;
using UnityEngine;

/// <summary>
/// Class for handling player info
/// </summary>
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

	public CharacterPart(PartType type, Sprite part, int damage = 0, float speed = 0, float health = 0, int fireRate = 0, int otherFeature = 0)
		: base(damage,speed,health,fireRate,otherFeature, InstantEffect.None)
	{
		this.partType = type;
		this.sprite = part;
	}

	public void EquipPart()
	{
		
		switch (partType)
		{
			case PartType.head:
				SpriteRenderer headSprite = PlayerInfo.Instance.playerControls.HeadRef.GetComponentInChildren<SpriteRenderer>();
				headSprite.sprite = sprite;
				break;
			case PartType.body:
				SpriteRenderer bodySprite = PlayerInfo.Instance.playerControls.BodyRef.GetComponentInChildren<SpriteRenderer>();
				bodySprite.sprite = sprite;
				break;
			case PartType.arm:
				SpriteRenderer armSprite = PlayerInfo.Instance.playerControls.ArmRef.GetComponentInChildren<SpriteRenderer>();
				armSprite.sprite = sprite;
				break;case PartType.gun:
				SpriteRenderer gunSprite = PlayerInfo.Instance.playerControls.GunRef.GetComponentInChildren<SpriteRenderer>();
				gunSprite.sprite = sprite;
				break;
		}
	}
}
