using Generator;
using UnityEngine;

public class FloorGenerator
{
    static public TerrainType[,] Generate(int xSize, int ySize, int roomScale, double threshold, int seed = 845723864)
    {
        var map = new TerrainType[xSize, ySize];
        
        var xSizeRoomMap = xSize / roomScale;
        var ySizeRoomMap = ySize / roomScale;
        
        var roomMap = RoomMap(threshold, seed, xSizeRoomMap, ySizeRoomMap);

        PlaceRoomsOnMap(roomScale, map, xSizeRoomMap, ySizeRoomMap, roomMap);

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
                secondPoint);
        }
    }

    private static void PlaceRoomsOnMap(int roomScale, TerrainType[,] map, int xSizeRoomMap, int ySizeRoomMap,
        bool[,] boolRoomMap)
    {
        for (int y = 1; y < map.GetLength(1) - 1; y++)
        {
            for (int x = 1; x < map.GetLength(0) - 1; x++)
            {
                var xRoomMap = x / roomScale;
                var yRoomMap = y / roomScale;
                if(xRoomMap >= xSizeRoomMap || yRoomMap  >= ySizeRoomMap) continue;

                var mask = boolRoomMap[xRoomMap, yRoomMap];
                if (mask) map[x, y] = TerrainType.Floor;
                else map[x, y] = TerrainType.None;
            }
        }
    }

    private static bool[,] RoomMap(double threshold, int seed, int xSizeRoomMap, int ySizeRoomMap)
    {
        var noiseRoomMap = HashFunction.GenerateNoiseMap(xSizeRoomMap, ySizeRoomMap, 1d, seed );
        
        var boolRoomMap = new bool[xSizeRoomMap,ySizeRoomMap];
        
        for (int y = 0; y < noiseRoomMap.GetLength(1); y++)
        {
            for (int x = 0; x < noiseRoomMap.GetLength(0); x++)
            {
                if (noiseRoomMap[x, y] < threshold) boolRoomMap[x, y] = false;
                else boolRoomMap[x, y] = true;
            }
        }

        return boolRoomMap;
    }
}