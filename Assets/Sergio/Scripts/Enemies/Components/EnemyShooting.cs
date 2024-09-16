
using UnityEngine;

public abstract class EnemyShooting : EnemyComponent
{
	protected float _lastShootTime;
	protected Transform shootPoint;
	protected Projectile projectilePrefab;

	public virtual void Shoot(Transform target = null)
	{
		if (Time.time >= _lastShootTime + Stats.AttackCooldown)
		{
			var projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
			projectile.SetTarget(target);

			_lastShootTime = Time.time;
		}
	}
}