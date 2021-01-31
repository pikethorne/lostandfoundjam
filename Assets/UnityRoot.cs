using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Root for dungeon spawning
/// </summary>
[ExecuteInEditMode()]
public class UnityRoot : MonoBehaviour
{
    public string Tileset = "ProgrammerTileset";

    string RoomPath = "Rooms/";

	int distanceBetweenNodes = 21;
	/// <summary>
	/// builds the fucking dungeons you idiot
	/// is this should be a class?
	/// are we abstracting too much for the sake of object oriented programming?
	/// don't give a ffuff!
	/// </summary>
	public class DungeonBuilder
	{
		public Dictionary<int, Tuple<int, int>> nodePositions;

		public enum NodeType { Normal, Treasure, Miniboss, Boss, Entrance, Shop };

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

			public bool processed;

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
		
		// HAS CAPITALISM WON ?????
		public bool shopSpawned = false;

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

		public DungeonBuilder(int dd = 5, int nm = 20, float c = 0.3f)
		{
			DungeonDepth = dd;
			nodeMax = nm;
			Complexity = c;

			NodeMap = new List<Node>();
			nodePositions = new Dictionary<int, Tuple<int, int>>();

			Node StartNode = new Node(0, NodeType.Entrance);
			nodePositions.Add(0, new Tuple<int, int>(0, 0));

			NodeMap.Add(StartNode);
			StartNode.spawnIndex = 0;
			NodeSetup(StartNode);
		}
		/// <summary>
		/// 
		/// </summary>
		int getMaxValidConnections(int index)
		{
			Tuple<int, int> tuple = nodePositions[index];
			int maxCon = 0;
			if (!nodePositions.ContainsValue(new Tuple<int, int>(tuple.Item1 - 1, tuple.Item2)))
				maxCon++;
			if (!nodePositions.ContainsValue(new Tuple<int, int>(tuple.Item1 + 1, tuple.Item2)))
				maxCon++;
			if (!nodePositions.ContainsValue(new Tuple<int, int>(tuple.Item1, tuple.Item2 - 1)))
				maxCon++;
			if (!nodePositions.ContainsValue(new Tuple<int, int>(tuple.Item1, tuple.Item2 + 1)))
				maxCon++;
			return maxCon;
		}

		void addNodeToNearestPosition(int lastIndex, int thisIndex)
		{
			Tuple<int, int> lastPosition = nodePositions[lastIndex];
			Tuple<int, int> thisPosition;
			if (!nodePositions.ContainsValue(new Tuple<int, int>(lastPosition.Item1 - 1, lastPosition.Item2)))
				thisPosition = new Tuple<int, int>(lastPosition.Item1 - 1, lastPosition.Item2);
			else if (!nodePositions.ContainsValue(new Tuple<int, int>(lastPosition.Item1 + 1, lastPosition.Item2)))
				thisPosition = new Tuple<int, int>(lastPosition.Item1 + 1, lastPosition.Item2);
			else if (!nodePositions.ContainsValue(new Tuple<int, int>(lastPosition.Item1, lastPosition.Item2 - 1)))
				thisPosition = new Tuple<int, int>(lastPosition.Item1, lastPosition.Item2 - 1);
			else if (!nodePositions.ContainsValue(new Tuple<int, int>(lastPosition.Item1, lastPosition.Item2 + 1)))
				thisPosition = new Tuple<int, int>(lastPosition.Item1, lastPosition.Item2 + 1);
			else
			{
				Debug.LogError("Could not find a vaild position, curling up into a ball and dying");
				return;
			}

			nodePositions.Add(thisIndex, thisPosition);
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
			n.processed = true;
            // How many branches will this room have
            int rollthedice = 0;

            // boss rooms and such shouldn't really branch unless eric wants them to i guess (he doesn't)
            if (n.nodeType == NodeType.Normal || n.nodeType == NodeType.Entrance)
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
            int connectionCount = 1 + Mathf.Min(rollthedice, getMaxValidConnections(n.spawnIndex));

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
                        else if (specialRoll > 30f && !shopSpawned)
                        {
                            newType = NodeType.Shop;
							shopSpawned = true;
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
                        else if (!shopSpawned)
                        {
                            newType = NodeType.Shop;
							shopSpawned = true;
                        }
                    }

                    n.Connections[i] = new Node(n.Depth + 1, newType);
					NodeMap.Add(n.Connections[i]);
					n.Connections[i].spawnIndex = NodeMap.Count - 1;
					addNodeToNearestPosition(n.spawnIndex, n.Connections[i].spawnIndex);
				}
            }

            foreach (Node ext in n.Connections)
            {
                if (!ext.processed)
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
				GameObject[] RoomOptions= { };

				if (n.nodeType == DungeonBuilder.NodeType.Entrance)
                {
                    RoomOptions = Resources.LoadAll<GameObject>(RoomPath + Tileset + "/Entrance");

                    if (RoomOptions.Length <= 0)
                    {
                        Debug.LogError("No rooms found in the [" + RoomPath + Tileset + "/Entrance] Folder!");
                    }
                }
				else if (n.nodeType == DungeonBuilder.NodeType.Shop)
                {
                    RoomOptions = Resources.LoadAll<GameObject>(RoomPath + Tileset + "/Shop");

                    if (RoomOptions.Length <= 0)
                    {
                        Debug.LogError("No rooms found in the [" + RoomPath + Tileset + "/Shop] Folder!");
                    }
                }
				else if (n.nodeType == DungeonBuilder.NodeType.Normal)
                {
                    RoomOptions = Resources.LoadAll<GameObject>(RoomPath + Tileset + string.Format("/{0}Exits",4));//need to add logic for room direction-y stuff to get the <4 to work again... bluh

                    if (RoomOptions.Length <= 0)
                    {
                        Debug.LogError("No rooms found in the [" + RoomPath + Tileset + string.Format("/{0}Exits", n.Connections.Length) + "] Folder!");
                    }
                }
                else if(n.nodeType == DungeonBuilder.NodeType.Boss)
                {
                    RoomOptions = Resources.LoadAll<GameObject>(RoomPath + Tileset + "/Boss");

                    if (RoomOptions.Length <= 0)
                    {
                        Debug.LogError("No rooms found in the [" + RoomPath + Tileset + "Boss" + "] Folder!");
                    }
                }
                else if (n.nodeType == DungeonBuilder.NodeType.Treasure)
                {
                    RoomOptions = Resources.LoadAll<GameObject>(RoomPath + Tileset + "/Treasure");

                    if (RoomOptions.Length <= 0)
                    {
                        Debug.LogError("No rooms found in the [" + RoomPath + Tileset + "Treasure" + "] Folder!");
                    }
                }

				GameObject g = Instantiate(RoomOptions[Random.Range(0, RoomOptions.Length)]);
				Tuple<int, int> position = db.nodePositions[n.spawnIndex];
				g.transform.position = Vector3.zero + Vector3.right * position.Item1 * distanceBetweenNodes + Vector3.up * position.Item2 * distanceBetweenNodes;

				PopulateExits(n, g, db);
			}

			
		}
    }

	private void PopulateExits(DungeonBuilder.Node n, GameObject g, DungeonBuilder db)
	{
		GameObject[] verticalPathOptions = Resources.LoadAll<GameObject>(RoomPath + Tileset + "/PathVertical");
		GameObject[] horizontalPathOptions = Resources.LoadAll<GameObject>(RoomPath + Tileset + "/PathHorizontal");

		List<RoomExit> roomExitPoints = g.GetComponentsInChildren<RoomExit>().ToList() ;

		Tuple<int, int> positionOfNode = db.nodePositions[n.spawnIndex];

		if (db.nodePositions.Any(no => n.Connections.Any(k => k.spawnIndex == no.Key) 
			&& no.Value.Equals(new Tuple<int, int>(positionOfNode.Item1 - 1, positionOfNode.Item2))))
		{
			RoomExit exit = roomExitPoints.OrderBy(r => r.transform.localPosition.x).First();
			exit.activateTile = true;
			for (int i = (int)exit.transform.localPosition.x-1; i >= -distanceBetweenNodes / 2; i--)
			{
				GameObject p = Instantiate(horizontalPathOptions[Random.Range(0, horizontalPathOptions.Length)]);
				p.transform.parent = g.transform;
				p.transform.localPosition = Vector3.zero + Vector3.right * i + Vector3.up * exit.transform.localPosition.y;
			}
		}

		if (db.nodePositions.Any(no => n.Connections.Any(k => k.spawnIndex == no.Key)
			&& no.Value.Equals(new Tuple<int, int>(positionOfNode.Item1 + 1, positionOfNode.Item2))))
		{
			RoomExit exit = roomExitPoints.OrderBy(r => r.transform.localPosition.x).Last();
			exit.activateTile = true;
			for (int i = (int)exit.transform.localPosition.x+1; i <= distanceBetweenNodes / 2; i++)
			{
				GameObject p = Instantiate(horizontalPathOptions[Random.Range(0, horizontalPathOptions.Length)]);
				p.transform.parent = g.transform;
				p.transform.localPosition = Vector3.zero + Vector3.right * i + Vector3.up * exit.transform.localPosition.y;
			}
		}

		if (db.nodePositions.Any(no => n.Connections.Any(k => k.spawnIndex == no.Key)
			&& no.Value.Equals(new Tuple<int, int>(positionOfNode.Item1, positionOfNode.Item2-1))))
		{
			RoomExit exit = roomExitPoints.OrderBy(r => r.transform.localPosition.y).First();
			exit.activateTile = true;
			for (int i = (int)exit.transform.localPosition.y-1; i >= -distanceBetweenNodes / 2; i--)
			{
				GameObject p = Instantiate(verticalPathOptions[Random.Range(0, verticalPathOptions.Length)]);
				p.transform.parent = g.transform;
				p.transform.localPosition = Vector3.zero + Vector3.up * i + Vector3.right * exit.transform.localPosition.x;
			}
		}

		if (db.nodePositions.Any(no => n.Connections.Any(k => k.spawnIndex == no.Key)
			&& no.Value.Equals(new Tuple<int, int>(positionOfNode.Item1, positionOfNode.Item2+1))))
		{
			RoomExit exit = roomExitPoints.OrderBy(r => r.transform.localPosition.y).Last();
			exit.activateTile = true;
			for (int i = (int)exit.transform.localPosition.y+1; i <= distanceBetweenNodes/2; i++)
			{
				GameObject p = Instantiate(verticalPathOptions[Random.Range(0, verticalPathOptions.Length)]);
				p.transform.parent = g.transform;
				p.transform.localPosition = Vector3.zero + Vector3.up * i + Vector3.right * exit.transform.localPosition.x;
			}
		}
	}

}
