using UnityEngine;

public class ShootAction : IEnemyAction
{
	public float shootInterval = 1f;
	public GameObject projectilePrefab;
	private float timeSinceLastShot;
	public EnemyController Controller { get; set; }

	public void StartAction(EnemyController controller)
	{
		Controller = controller;
		timeSinceLastShot = 0f;
	}

	public void UpdateAction()
	{
		timeSinceLastShot += Time.deltaTime;

		if (timeSinceLastShot >= shootInterval)
		{
			Shoot();
			timeSinceLastShot = 0f;
		}
	}

	private void Shoot()
	{
		// Crear el proyectil y configurarlo
		// Instantiate(projectilePrefab, transform.position, Quaternion.identity);
	}

	public void EndAction()
	{
		// Finalizar la acción de disparo
	}
}