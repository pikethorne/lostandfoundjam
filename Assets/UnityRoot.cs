using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Root for dungeon spawning
/// </summary>
[ExecuteInEditMode()]
public class UnityRoot : MonoBehaviour
{
    public string Tileset = "ProgrammerTileset";

    string RoomPath = "Rooms/";

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
            /// spawnIndex of a given node
            /// </summary>
            public int spawnIndex = -5;

            /// <summary>
            /// connections to other nodes
            /// </summary>
            public Node[] Connections;

            public NodeType nodeType;

            public Node(int d, NodeType nt)
            {
                Depth = d;

                nodeType = nt;
            }
        }

        // HAS THE BOSS ROOM SPAWNED
        public bool BossSpawned = false;

        // TREASURE SPAWNEDED ???
        public bool TreasureSpawned = false;

        /// <summary>
        /// how many nodes deep from the start the dungeon should go, don't go above like 4
        /// </summary>
        public int DungeonDepth = 2;

        /// <summary>
        /// likelihood for branching at lower depth levels, go from 0-1.0
        /// </summary>
        public float Complexity = 0.25f;

        /// <summary>
        /// limit the amount of rooms in a floor. If the max is reached, all unclosed connections will lead to a single exit room
        /// </summary>
        public int nodeMax = 5;

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


            Node StartNode = new Node(0, NodeType.Normal);
            NodeSetup(StartNode);
        }

        /// <summary>
        /// Initialize a node and its connections
        /// 
        /// My brain is worms right now so i'll explain it best I can
        /// 
        /// We assume a node always has 1 connection to its previous node, the one that spawned it
        /// A node can have up to 3 other connections
        /// 
        /// Once we have created these potentially 4 connections, one of them has to be to the previous
        /// node that spawned this one. So if A spawns B, then B MUST have a connection to A and A MUST have a connection to B
        /// 
        /// </summary>
        /// <param name="n"></param>
        void NodeSetup(Node n, Node prevNode = null)
        {
            NodeMap.Add(n);
            n.spawnIndex = NodeMap.Count - 1;

            // How many branches will this room have
            int rollthedice = 0;

            // boss rooms and such shouldn't really branch unless eric wants them to i guess (he doesn't)
            if (n.nodeType == NodeType.Normal)
            {
                // if we exceeded the max, this room only has one connection
                if (NodeMap.Count < nodeMax && n.Depth <= DungeonDepth)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        float limit = 100f * (n.Depth / DungeonDepth) + 10f * i * Mathf.Max((Random.value - Complexity), 0);
                        float check = Random.value * 100f;

                        rollthedice += 1;
                    }

                    if (NodeMap.Count < nodeMax - 1 && rollthedice <= 1)
                    {
                        rollthedice += Random.Range(1, 4);
                    }
                }
                else
                {
                    rollthedice = 0;
                }
            }

            // ehhhhh just in case
            int connectionCount = 1 + Mathf.Min(rollthedice, 3);

            // establish the nodes new node connections
            n.Connections = new Node[connectionCount];


            if (prevNode != null)
            {
                // establish the index of by which a node connects back to the node that spawned it
                int rerun = Mathf.RoundToInt(Random.value * (n.Connections.Length - 1));
                
                // the connection index that returns to the previous node
                n.Connections[rerun] = prevNode;
            }


            for (int i = 0; i < n.Connections.Length; i++)
            {
                if (n.Connections[i] == null)
                {
                    NodeType newType = NodeType.Normal;

                    if (n.Depth >= 1 && n.Connections.Length > 1)
                    {
                        float specialRoll = Random.value * 100f;

                        if (specialRoll > 80f && !BossSpawned)
                        {
                            newType = NodeType.Boss;
                            BossSpawned = true;
                        }
                        else if (specialRoll > 50f && !TreasureSpawned)
                        {
                            newType = NodeType.Treasure;
                            TreasureSpawned = true;
                        }
                    }

                    if (NodeMap.Count >= nodeMax - 4)
                    {
                        if (!BossSpawned)
                        {
                            newType = NodeType.Boss;
                            BossSpawned = true;
                        }
                        else if (!TreasureSpawned)
                        {
                            newType = NodeType.Treasure;
                            TreasureSpawned = true;
                        }
                    }

                    n.Connections[i] = new Node(n.Depth + 1, newType);
                }
            }

            foreach (Node ext in n.Connections)
            {
                if (ext.spawnIndex < 0)
                {
                    NodeSetup(ext, n);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isPlaying)
        {
            DungeonBuilder db = new DungeonBuilder();

            Debug.Log(string.Format("Created node map with {0} nodes!", db.NodeMap.Count));

            for(int i = 0; i < db.NodeMap.Count; i++)
            {
                DungeonBuilder.Node n = db.NodeMap[i];

                if (n.nodeType == DungeonBuilder.NodeType.Normal)
                {
                    GameObject[] RoomOptions = UnityEngine.Resources.LoadAll<GameObject>(RoomPath + Tileset + string.Format("/{0}Exits",n.Connections.Length));

                    if (RoomOptions.Length <= 0)
                    {
                        Debug.LogError("No rooms found in the [" + RoomPath + Tileset + string.Format("/{0}Exits", n.Connections.Length) + "] Folder!");
                    }
                    else
                    {
                        GameObject g = GameObject.Instantiate(RoomOptions[Random.Range(0, RoomOptions.Length)]);

                        g.transform.position = Vector3.zero + Vector3.right * n.spawnIndex * 50;

						PopulateExits(n, g);
					}
                }
                else if(n.nodeType == DungeonBuilder.NodeType.Boss)
                {
                    GameObject[] RoomOptions = UnityEngine.Resources.LoadAll<GameObject>(RoomPath + Tileset + "/Boss");

                    if (RoomOptions.Length <= 0)
                    {
                        Debug.LogError("No rooms found in the [" + RoomPath + Tileset + "Boss" + "] Folder!");
                    }
                    else
                    {
                        GameObject g = GameObject.Instantiate(RoomOptions[Random.Range(0, RoomOptions.Length)]);

                        g.transform.position = Vector3.zero + Vector3.right * n.spawnIndex * 50;

						PopulateExits(n, g);
					}
                }
                else if (n.nodeType == DungeonBuilder.NodeType.Treasure)
                {
                    GameObject[] RoomOptions = UnityEngine.Resources.LoadAll<GameObject>(RoomPath + Tileset + "/Treasure");

                    if (RoomOptions.Length <= 0)
                    {
                        Debug.LogError("No rooms found in the [" + RoomPath + Tileset + "Treasure" + "] Folder!");
                    }
                    else
                    {
                        GameObject g = GameObject.Instantiate(RoomOptions[Random.Range(0, RoomOptions.Length)]);

                        g.transform.position = Vector3.zero + Vector3.right * n.spawnIndex * 50;

						PopulateExits(n, g);
                    }
                }
            }
        }
    }

	private void PopulateExits(DungeonBuilder.Node n, GameObject g)
	{
		RoomExit[] roomExitPoints = g.GetComponentsInChildren<RoomExit>();
		if(n.Connections.Length != roomExitPoints.Length)
		{
			Debug.LogError("somehow the number of exits from the prefab doesn't match the number of node exits. Prefab is probably called " + g.name + " or somthing");
		}
		else
		{
			for(int i =0;i<roomExitPoints.Length; i++)
			{
				roomExitPoints[i].teleportID = n.Connections[i].spawnIndex;
			}
		}
	}

}
