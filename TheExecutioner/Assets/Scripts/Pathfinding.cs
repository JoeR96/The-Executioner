using UnityEngine;
using System.Collections.Generic;



public class Pathfinding : MonoBehaviour
{
    EnvironmentManager environmentManager;
    public Transform seeker;
    public Transform target;
    Grid grid;
    public List<Transform> targets = new List<Transform>();
    void Awake()
    {
        environmentManager = GetComponent<EnvironmentManager>();
        grid = GameManager.instance.gameObject.GetComponent<Grid>();
    }
    private PlatformManager _platformManager;
    private void Start()
    {
        _platformManager = GetComponent<PlatformManager>();
        Debug.Log(targets.Count);
        // for (int i = 0; i < targets.Count; i++)
        // {
        //     FindPath(targets[targets.Count -1].position,targets[i].position);
        //     
        // }
        
        //_platformManager.RaisePath(path);
    }
    


    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public void InitializePath()
    {
        path.Clear();
        
         StartPosition = new Vector3(Random.Range(-35,35),0,Random.Range(-35,35));
         EndPosition = new Vector3(Random.Range(-35,35),0,Random.Range(-35,35));
        
         FindPath (StartPosition, EndPosition);
         
    }

    
    public void InitializeConnectingPath(Node node, Node connectingPath)
    {
        path.Clear();

        StartPosition = node.worldPosition;
        EndPosition = connectingPath.worldPosition;
    
        FindPath (StartPosition, EndPosition);


    }


    public void FindPath(Vector3 startPos, Vector3 targetPos)
     {
         Debug.Log(grid);
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i ++) {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode) {
                foreach (var tile in path)
                {
                    
                }
                RetracePath(startNode,targetNode);
                return;
            }

            var t = grid.GetNeighbour(node);
            foreach (var go in t)
            {
            
            }
            foreach (var neighbour in t) {
                
                if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }
    List<Node> path = new List<Node>();
    public List<Node> RetracePath(Node startNode, Node endNode) {
        
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        Debug.Log(path.Count);
        var temp = new List<Node>();
        foreach (var go in path)
        {
            var t = GameManager.instance.EnvironmentManager.environmentSpawner.CheckAdjacentPositions(go);
            temp.Add(t[1,2]);
        }

        for (int i = 0; i < temp.Count; i++)
        {
            path.Add(temp[i]);
        }
        Debug.Log(path.Count);
        return path;
        //grid.Path = path;
    }

    public List<Node> ReturnPath()
    {
        foreach (var node in path)
        {
            node.InUse = true;
        }
        return path;
    }
    int GetDistance(Node nodeA, Node nodeB) {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14*dstY + 10* (dstX-dstY);
        return 14*dstX + 10 * (dstY-dstX);
    }

    
}