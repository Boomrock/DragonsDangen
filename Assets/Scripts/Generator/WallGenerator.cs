using Generator;
using UnityEngine;

public class WallGenerator
{
    public static void SetWall(TerrainType[,] map)
    {
        var copyMap = map;

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                bool needWall = false;
                MatrixTools.SpiralArrayTraversal(map,new Vector2Int(x,y), i =>
                {
                    if(i.x == x && i.y == y && map[i.x, i.y] != TerrainType.None) return MatrixTools.Result.Break;
                    if(i.x == x && i.y == y) return MatrixTools.Result.None;

                    if (map[i.x, i.y] == TerrainType.Floor)
                    {
                        needWall = true;
                        return MatrixTools.Result.Break;
                    }
                    
                    return MatrixTools.Result.None;
                }, 1);
                if (needWall) map[x, y] = TerrainType.Wall;
            }
        }
    }
}