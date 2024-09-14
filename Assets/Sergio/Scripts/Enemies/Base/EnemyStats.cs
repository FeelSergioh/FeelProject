using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy/Stats")]
public class EnemyStats : ScriptableObject
{
    public float Health;
    public float Damage;
    public float Speed;
}