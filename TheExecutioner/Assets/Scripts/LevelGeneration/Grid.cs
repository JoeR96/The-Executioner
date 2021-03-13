using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Grid : MonoBehaviour
{

	
	public Transform CubeParent;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public Node[,] grid;
	public GameObject stairs;
	float nodeDiameter;
	public int gridSizeX, gridSizeY;
	public List<GameObject> SpawnedObjects = new List<GameObject>();
	public List<List<Node>> pathMaster = new List<List<Node>>();
	public GameObject floorContainer;
	public GameObject testCube;
	
	void Awake()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid();
		SpawnPlatform();
	}
	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 -
		                          Vector3.forward * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
				                     Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = true;
				grid[x, y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}
	public Node NodeFromWorldPoint(Vector3 worldPosition)
	{

		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		
		return grid[x, y];
	}

	public void ClearPaths()
	{
		pathMaster.Clear();
		foreach (var go in SpawnedObjects)
		{
			Destroy(go);
		}
	}
	
	public void SpawnTest()
	{
		Vector3 spawnPosition;
		foreach (Node n in grid)
		{
			Debug.Log(grid.Length);
			Debug.Log(pathMaster.Count);
			for (int i = 0; i < GameManager.instance.EnvironmentManager.environmentSpawner.LevelPaths.Count; i++)
			{
	
				if (GameManager.instance.EnvironmentManager.environmentSpawner.LevelPaths[i] != null)
				{
					spawnPosition = GameManager.instance.EnvironmentManager.environmentSpawner.LevelPaths[i][0][0].worldPosition;
					Debug.Log(GameManager.instance.EnvironmentManager.environmentSpawner.LevelPaths[i][0]);
					var stair =Instantiate(stairs, spawnPosition, Quaternion.identity);
					SpawnedObjects.Add(stair);


					if (GameManager.instance.EnvironmentManager.environmentSpawner.LevelPaths[i][i].Contains(n))
					{
						spawnPosition = n.worldPosition;
						spawnPosition.y = 18f;
				
						var t = Instantiate(testCube, spawnPosition, Quaternion.identity);
							SpawnedObjects.Add(t);

					}
				}
			}
			
		}
	}
	void SpawnPlatform()
	{
		Vector3 spawnPosition;
		foreach (Node n in grid)
		{
			
			spawnPosition = n.worldPosition;
			spawnPosition.y = spawnPosition.y - 18f;
			var _ = Instantiate(floorContainer, spawnPosition, Quaternion.identity);
			_.transform.SetParent(CubeParent);
			_.GetComponent<PlatformManager>().PlatformStateManager.Setint(n.gridX, n.gridY);
			n.SetPlatformToNode(_.gameObject);
			_.GetComponent<PlatformManager>().PlatformStateManager.SetNode(n);
		}
	}
	public List<Node> GetNeighbour(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
	public void SetMap(Node[,] newLevel)
	{
		grid = newLevel;
	}
	}

public class Node
{


	public bool InUse = true;
	public bool walkable;
	public Vector3 worldPosition;
	public Vector2 GridPosition;
	public int gridX;
	public int gridY;
	public GameObject platform;
	public int gCost;
	public int hCost;
	public Node parent;
	public PlatformManager PlatformManager;
	
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
		GridPosition = new Vector2(_gridX, _gridY);
	}

	
	public int fCost => gCost + hCost;

	public void SetPlatformToNode(GameObject go)
	{
		platform = go;
		PlatformManager = go.GetComponent<PlatformManager>();
	}
}
