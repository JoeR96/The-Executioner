using UnityEngine;
using System.Collections.Generic;



public class Pathfinding : MonoBehaviour
{

    public List<Transform> targets = new List<Transform>();
    public Transform seeker;
    public Transform target;
    Grid grid;

    void Awake() 
    {
        grid = GetComponent<Grid> ();
    }

    private void Start()
    {
        InitializePath();
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    }
    [SerializeField] private Material[] Colours;
    public void SpawnPaths()
    {
        var random = Random.Range(0, Colours.Length);
        var path = InitializePath();
        
        foreach (var node in path)
        {
            Debug.Log(path.Count);
            node.InUse = true;
        }
        ChangePathColor(Colours[random],path);
    }
    private void ChangePathColor(Material material,List<Node> list)
    {
        foreach (var g in list)
        {
            g.platform.GetComponent<MeshRenderer>().material = material;
        }
        
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    }
    private void SetSpawnPositions()
    {

        seeker = ReturnTransform();
        target = ReturnTransform();
    }
    public List<Node> InitializePath()
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        var t = FindPath (ReturnTransform().position, ReturnTransform().position);
        return t;
=======
        
        FindPath (ReturnTransform().position, ReturnTransform().position);
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
        
        FindPath (ReturnTransform().position, ReturnTransform().position);
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
        
        FindPath (ReturnTransform().position, ReturnTransform().position);
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
        
        FindPath (ReturnTransform().position, ReturnTransform().position);
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    }
    private Transform ReturnTransform()
    {
        var random = Random.Range(0, targets.Count - 1);
        return targets[random];
    }

    List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
     {
         List<Node> path = new List<Node>();
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
                RetracePath(startNode,targetNode);
            }

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            var t = grid.GetNeighbour(node);
            foreach (var neighbour in t) {
                
=======
            foreach (Node neighbour in grid.GetNeighbours(node)) {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
            foreach (Node neighbour in grid.GetNeighbours(node)) {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
            foreach (Node neighbour in grid.GetNeighbours(node)) {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
            foreach (Node neighbour in grid.GetNeighbours(node)) {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
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

        return null;
     }
    
    public List<Node> RetracePath(Node startNode, Node endNode) {
        
        Node currentNode = endNode;
        List<Node> path = new List<Node>();
        
        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

<<<<<<< HEAD

=======
    public List<Node> ReturnPath()
    {
        return path;
    }
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    int GetDistance(Node nodeA, Node nodeB) {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14*dstY + 10* (dstX-dstY);
        return 14*dstX + 10 * (dstY-dstX);
    }
}