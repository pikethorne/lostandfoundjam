using UnityEngine;

class RoomExit : ToggleTile
{
	public int teleportID; 

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player" && activateTile)
		{
			col.transform.position = Vector3.right * teleportID * 50;
		}
	}
}