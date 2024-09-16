using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy/Stats")]
public class EnemyStats : ScriptableObject
{
    public float Speed;
    public float TurnSpeed;
    public float Health;
    public float Damage;
    public float AttackRange;
    public float AttackCooldown;
    public float VisionAngle;
    public float VisionRange;
}