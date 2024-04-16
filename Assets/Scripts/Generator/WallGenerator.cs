using Generator;
using UnityEngine;

public class WallGenerator
{
    public static void SetWall(TerrainType[,] map)
    {
        var copyMap = map;
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
 
                bool needWall = false;
                MatrixTools.SpiralArrayTraversal(map,new Vector2Int(x,y), i =>
                {
                    if(i.x == x && i.y == y && map[i.y, i.x] != TerrainType.None) return MatrixTools.Result.Break;
                    if(i.x == x && i.y == y) return MatrixTools.Result.None;

                    if (map[i.y, i.x] == TerrainType.Floor)
                    {
                        needWall = true;
                        return MatrixTools.Result.Break;
                    }
                    
                    return MatrixTools.Result.None;
                }, 1);
                if (needWall) map[y, x] = TerrainType.Wall;
            }
        }
    }
}