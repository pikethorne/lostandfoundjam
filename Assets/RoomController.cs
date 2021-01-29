using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class RoomController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            foreach (Transform g in this.transform)
            {
                if (g.tag == "Tile")
                {
                    Vector3 pos = g.position;
                    pos.x = Mathf.Floor(pos.x);
                    pos.y = Mathf.Floor(pos.y);
                    pos.z = Mathf.Floor(pos.z);
                    g.position = pos;
                }
            }
        }
    }
}
