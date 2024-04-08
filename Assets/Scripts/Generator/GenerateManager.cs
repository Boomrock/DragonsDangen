using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Zenject;

namespace Generator
{
    public class GenerateManager: MonoBehaviour
    {
        [SerializeField] public Tilemap _floorTilemap;
        [SerializeField] public Tilemap _wallTileMap;

        public TerrainType[,]  Map => _map;
        
        private TerrainType[,] _map;
        private GeneratorConfig _generatorConfig;

        [Inject]
        private void Inject(GeneratorConfig generatorConfig)
        {
            _generatorConfig = generatorConfig;
        }
        private void Awake()
        {
            _map = FloorGenerator.Generate(
                _generatorConfig.SizeMap.x, 
                _generatorConfig.SizeMap.y, 
                _generatorConfig.RoomScale, 
                _generatorConfig.Threshold, 
                _generatorConfig.Seed);
            WallGenerator.SetWall(_map);
        }


        private void Start()
        {
            var position = new Vector3Int();
            for (int y = 0; y < _map.GetLength(0); y++)
            {
                for (int x = 0; x < _map.GetLength(1); x++)
                {
                    position.x = x -_map.GetLength(1)/2;
                    position.y = y -_map.GetLength(0)/2;
                    
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
    }
}