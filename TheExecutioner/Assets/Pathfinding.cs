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
    }
    
    private void SetSpawnPositions()
    {

        seeker = ReturnTransform();
        target = ReturnTransform();
    }
    public void InitializePath()
    {
        
        FindPath (ReturnTransform().position, ReturnTransform().position);
    }
    private Transform ReturnTransform()
    {
        var random = Random.Range(0, targets.Count - 1);
        return targets[random];
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
     {
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
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(node)) {
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
        return path;
        //grid.Path = path;
    }

    public List<Node> ReturnPath()
    {
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