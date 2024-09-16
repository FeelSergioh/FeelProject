using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData", menuName = "Projectiles/Projectile Data", order = 1)]
public abstract class ProjectileData : ScriptableObject
{
	public float Speed;
	public float MaxDistance;
	public float BaseDamage;

	public float ExplosionRadius;
	public bool Homing;
}