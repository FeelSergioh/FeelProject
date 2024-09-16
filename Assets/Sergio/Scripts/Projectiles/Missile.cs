using UnityEngine;


public class Missile : Projectile
{
	protected override void Move()
	{
		if (Target != null && Data.Homing)
		{
			// Rotar misil hacia el objetivo
			Vector2 direction = (Target.position - transform.position).normalized;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
		}

		base.Move();
	}

	protected override void OnHit()
	{
		// Encontrar todos los objetos en el radio de explosión
		var objectsInRange = Physics2D.OverlapCircleAll(transform.position, Data.ExplosionRadius);
		
		foreach (Collider2D obj in objectsInRange)
		{
			if (obj.TryGetComponent(out IDamageable damageable))
			{
				var distance = Vector2.Distance(transform.position, obj.transform.position);
				damageable.TakeDamage(Data.BaseDamage * (1 - distance / Data.ExplosionRadius));
			}
		}

		Debug.Log("Missile exploded!");
		base.OnHit();
	}
}