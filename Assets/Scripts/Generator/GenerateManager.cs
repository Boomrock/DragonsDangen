using System;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Zenject;

namespace Generator
{
    public class GenerateManager: MonoBehaviour
    {
        [SerializeField] private Tilemap _floorTilemap;
        [SerializeField] private Tilemap _wallTileMap;
        [SerializeField] private GameObject _player;

        private RoomGenerator.RoomType[,] _markedRoomMap;
        private  GeneratorConfig _generatorConfig;
        private TerrainType[,] _map;

        [Inject]
        private void Inject(GeneratorConfig generatorConfig)
        {
            _generatorConfig = generatorConfig;
        }
        
        private void Awake()
        {
            var roomMap = RoomGenerator.GenerateRoomMap(
                _generatorConfig.SizeMap,
                _generatorConfig.RoomScale,
                _generatorConfig.Threshold,
                _generatorConfig.Seed);
            
            _markedRoomMap = RoomGenerator.MarkRoomMap(
                new() { { RoomGenerator.RoomType.Spawn, 1 } }, 
                roomMap);
            
            
            var str = new StringBuilder();
            for (int i = 0; i < _markedRoomMap.GetLength(0); i++)
            {
                for (int z = 0; z < _markedRoomMap.GetLength(1); z++)
                {
                    str.Append(_markedRoomMap[i,z ] == RoomGenerator.RoomType.Spawn ? "0": "1");
                }
                Debug.Log(str.ToString());
                str.Clear();
            }
            
            _map = FloorGenerator.Generate(
                _generatorConfig.SizeMap, 
                _generatorConfig.RoomScale,
                roomMap);
            
            WallGenerator.SetWall(_map);
        }


        private void Start()
        {
            PlayerSpawn();
            var position = new Vector3Int();
            for (int y = 0; y < _map.GetLength(0); y++)
            {
                for (int x = 0; x < _map.GetLength(1); x++)
                {
                    position.y = y -_map.GetLength(0)/2;
                    position.x = x -_map.GetLength(1)/2;

                    
                    var terrainType = _map[y, x];
                    switch (terrainType)
                    {
                        case TerrainType.Floor:
                            _floorTilemap.SetTile(position, _generatorConfig.Floor);
                            break;
                        case TerrainType.Wall:
                            _wallTileMap.SetTile(position, _generatorConfig.Wall);
                            break;
                    }
                }
            }
        }

        private void PlayerSpawn()
        {
            var spawnPoint = new Vector2();
            var ySize = _markedRoomMap.GetLength(0);
            var xSize = _markedRoomMap.GetLength(1);

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {

                    if (_markedRoomMap[y, x] == RoomGenerator.RoomType.Spawn)
                    {
                        spawnPoint.x = (x - xSize / 2f) * _generatorConfig.RoomScale;
                        spawnPoint.y = (y - ySize / 2f) * _generatorConfig.RoomScale;

                        _player.transform.position = spawnPoint;
                        return;
                    }
                }
            }
        }
    }
}