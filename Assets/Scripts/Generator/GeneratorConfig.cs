using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GeneratorConfig", menuName = "Gameplay/New GeneratorConfig")]
public class GeneratorConfig:ScriptableObject
{


    [SerializeField] public TileBase Floor;

    [SerializeField] public TileBase Wall;
        
    [SerializeField] public Vector2Int SizeMap;

    [Range(0f,1f)]
    [SerializeField] public float Threshold;
    [Range(1, 100)]
    [SerializeField] public int RoomScale;
    
    [SerializeField] public int Seed;
}
