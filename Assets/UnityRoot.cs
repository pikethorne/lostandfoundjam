using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Root for dungeon spawning
/// </summary>
[ExecuteInEditMode()]
public class UnityRoot : MonoBehaviour
{
    /// <summary>
    /// builds the fucking dungeons you idiot
    /// is this should be a class?
    /// are we abstracting too much for the sake of object oriented programming?
    /// don't give a ffuff!
    /// </summary>
    public class DungeonBuilder
    {

        public enum NodeType { Normal, Treasure, Miniboss, Boss };

        /// <summary>
        /// oh now we're in too funkin deep bro
        /// nodes contain references to room exits
        /// </summary>
        public class Node
        {
            /// <summary>
            /// how deep in this sh-dungeon the node is
            /// </summary>
            public int Depth;

            /// <summary>
            /// connections to other nodes
            /// </summary>
            private Node[] Connections;

            NodeType nodeType;

            public Node(int d, NodeType nt)
            {
                Depth = d;

                nodeType = nt;

                // How many branches will this room have
                int ROLLTHEFUCKINGDICE = 1;

                // boss rooms and such shouldn't really branch unless eric wants them to i guess
                if (nt == NodeType.Normal)
                {
                    ROLLTHEFUCKINGDICE = Random.Range(1, 4);
                }
            }
        }

        /// <summary>
        /// how many nodes deep from the start the dungeon should go, don't go above like 4
        /// </summary>
        int DungeonDepth = 3;

        /// <summary>
        /// likelihood for branching at lower depth levels, go from 0-1.0
        /// </summary>
        float Complexity = 0.5f;

        /// <summary>
        /// limit the amount of rooms in a floor. If the max is reached, all unclosed connections will lead to a single exit room
        /// </summary>
        int nodeMax = 10;

        /// <summary>
        /// suffer
        /// </summary>
        public List<Node> NodeMap;

        public DungeonBuilder(int dd = 3, int nm = 10, float c = 0.5f)
        {
            DungeonDepth = dd;
            nodeMax = nm;
            Complexity = c;

            NodeMap = new List<Node>();

            // HAS THE BOSS ROOM SPAWNED
            bool BossSpawned = false;

            // TREASURE SPAWNEDED ???
            bool TreasureSpawned = false;


            Node StartNode = new Node(0);
        }
    }

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
