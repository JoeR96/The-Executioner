public class CheckAdjacentNodes
{
    public Node[,] CheckAdjacentPositions(Node node,Node[,] grid)
    {
        
        Node[,] adjacent = new Node[5,5];

        var startX = node.gridX -2;
        var startZ = node.gridY -2;
        
        for (int x = 0; x < adjacent.GetLength(0) ; x++)
        {
            for (int z = 0; z < adjacent.GetLength(1) ; z++)
            {
      
                adjacent[x , z ] = grid[startX +x, startZ + z];

            }
        }
        return adjacent;
    }

    public Node[,] CheckAdjacentClosePositions(Node node,Node[,] grid)
    {
        
        Node[,] adjacent = new Node[3,3];

        var startX = node.gridX -1;
        var startZ = node.gridY -1;
        
        for (int x = 0; x < adjacent.GetLength(0) ; x++)
        {
            for (int z = 0; z < adjacent.GetLength(1) ; z++)
            {
      
                adjacent[x , z ] = grid[startX +x, startZ + z];

            }
        }
        return adjacent;
    }
}