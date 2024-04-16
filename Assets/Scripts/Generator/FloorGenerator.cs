using Generator;
using UnityEngine;

public class FloorGenerator
{
    static public TerrainType[,] Generate(Vector2Int mapSize, int roomScale, bool [,] roomMap)
    {
        var map = new TerrainType[mapSize.x, mapSize.y];
        PlaceRoomsOnMap(roomScale, map, roomMap);
        PlaceСorridorOnMap(roomScale, roomMap, map);

        return map;
    }

    private static void PlaceСorridorOnMap(int roomScale, bool[,] boolRoomMap, TerrainType[,] map)
    {
        var neightbors = MatrixTools.NearestNeighbor(boolRoomMap);
        foreach (var (start, end) in neightbors)
        {
            var firstPoint = start * roomScale + new Vector2Int(roomScale/2, roomScale /2);
            var secondPoint = end * roomScale + new Vector2Int(roomScale/2, roomScale / 2);

            MatrixWalker.TakeStep(
                map, 
                TerrainType.Floor, 
                firstPoint, 
                secondPoint,
                1);
        }
    }

    private static void PlaceRoomsOnMap(int roomScale, TerrainType[,] map, bool[,] boolRoomMap)
    {
        int ySizeRoomMap = boolRoomMap.GetLength(0);
        int xSizeRoomMap = boolRoomMap.GetLength(1);

        for (int y = 1; y < map.GetLength(0) - 1; y++)
        {
            for (int x = 1; x < map.GetLength(1) - 1; x++)
            {
                var yRoomMap = y / roomScale;
                var xRoomMap = x / roomScale;

   
                if(xRoomMap >= xSizeRoomMap || yRoomMap  >= ySizeRoomMap) continue;

                var mask = boolRoomMap[yRoomMap, xRoomMap];
                if (mask) map[y, x] = TerrainType.Floor;
                else map[y, x] = TerrainType.None;
            }
        }
    }


}