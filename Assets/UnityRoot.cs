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

                Vector3 axk = g.transform.position;
                axk.x = Mathf.Floor(axk.x);
                axk.y = Mathf.Floor(axk.y);
                axk.z = Mathf.Floor(axk.z);
                g.transform.position = axk;
            }
        }
    }
}
