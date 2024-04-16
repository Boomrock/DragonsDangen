using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generator
{
    public class RoomGenerator
    {
        public static bool[,] GenerateRoomMap(Vector2Int mapSize, int roomScale, float threshold, int seed)
        {
        
            var xSizeRoomMap = mapSize.x / roomScale;
            var ySizeRoomMap =  mapSize.y / roomScale;
            return RoomMap(threshold, seed, xSizeRoomMap, ySizeRoomMap);
            
        }

        public static RoomType[,] MarkRoomMap(Dictionary<RoomType, int> roomCounts, bool[,] roomMap)
        {
            var rooms = FindRooms(roomMap);
            var roomTypesMap = new RoomType[roomMap.GetLength(0), roomMap.GetLength(1)];
    
            foreach (var roomType in roomCounts.Keys)
            {
                var count = roomCounts[roomType];
        
                for (int i = 0; i < count; i++)
                {
                    var randIndex = Random.Range(0, rooms.Count);
                    var room = rooms[randIndex];
            
                    foreach (var point in room)
                    {
                        roomTypesMap[point.y, point.x] = roomType;
                    }
            
                    rooms.RemoveAt(randIndex);
                }
            }
    
            return roomTypesMap;
        }

        private static List<HashSet<Vector2Int>>  FindRooms(bool[,] roomMap)
        {
            List<HashSet<Vector2Int>> list = new();
            MatrixTools.SpiralArrayTraversal(
                roomMap,
                new Vector2Int(0, 0),
                point =>
                {
                    if (roomMap[point.y, point.x])
                    {
                        var hashSet = list.Find(i => i.Contains(point));

                        if (hashSet == null)
                        {
                            hashSet = new HashSet<Vector2Int>();
                            list.Add(hashSet);
                        }

                        hashSet.Add(point);
                        
                        MatrixTools.SpiralArrayTraversal(roomMap, 
                            point,
                            secondPoint =>
                            {
                                if (roomMap[secondPoint.y,secondPoint.x]) 
                                    hashSet.Add(secondPoint);
                                
                                return MatrixTools.Result.None;
                            },
                            1);
                    }


                    return MatrixTools.Result.None;
                });
            return list;
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

        public enum RoomType
        {
            None,
            Spawn,
        }
    }

    
    
}