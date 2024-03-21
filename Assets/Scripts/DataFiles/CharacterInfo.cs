using UnityEngine;

[CreateAssetMenu(fileName = "characterModel", menuName = "Gameplay/New CharacterModel")]
public class CharacterInfo : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    
    [SerializeField] private float _speed;

    public int Heath => _health;
    public int Damage => _damage;

    public float Speed => _speed;
}
