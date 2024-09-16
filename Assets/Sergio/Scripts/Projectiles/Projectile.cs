using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
	public ProjectileData Data { get; set; }
	public Transform Target { get; set; }

	protected float _distanceTraveled;

	private void Start()
	{
		_distanceTraveled = 0f;
	}

	private void Update()
	{
		Move();
	}

	protected virtual void Move()
	{
		Vector3 previousPosition = transform.position;
		transform.position += Data.Speed * Time.deltaTime * transform.up;

		_distanceTraveled += Vector3.Distance(previousPosition, transform.position);

		if (_distanceTraveled >= Data.MaxDistance)
		{
			OnHit();
		}
	}

	protected virtual void OnHit()
	{
		Destroy(gameObject);
	}

	public virtual void SetTarget(Transform target)
	{
		Target = target;
	}
}