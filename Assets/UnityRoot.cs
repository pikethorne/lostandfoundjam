using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Root for dungeon spawning
/// </summary>
[ExecuteInEditMode()]
public class UnityRoot : MonoBehaviour
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
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("Tile"))
            {

                Vector3 pos = g.transform.position;
                pos.x = Mathf.Floor(pos.x);
                pos.y = Mathf.Floor(pos.y);
                pos.z = Mathf.Floor(pos.z);
                g.transform.position = pos;
            }
        }
    }
}
