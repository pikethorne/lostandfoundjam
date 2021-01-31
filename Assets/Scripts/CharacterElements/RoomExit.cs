using UnityEngine;

class RoomExit : ToggleTile
{
	public override void UpdateTile()
	{
		GetComponent<Collider2D>().isTrigger = activateTile;
		base.UpdateTile();
	}
}