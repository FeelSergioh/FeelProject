using System;

using UnityEngine;


public class EnemyVision : EnemyComponent
{
	[SerializeField] private LayerMask obstaclesLayer;

	public Vector2 LastKnownPlayerPosition { get; private set; }
	public event Action OnPlayerSeen;

	public bool CanSeePlayer()
	{
		var player = GameObject.FindWithTag("Player");
		if (player == null)
		{
			LastKnownPlayerPosition = Vector2.zero;
			return false;
		}

		Vector2 directionToPlayer = player.transform.position - transform.position;
		float angleToPlayer = Vector2.Angle(transform.up, directionToPlayer);

		if (angleToPlayer < Stats.VisionAngle && directionToPlayer.magnitude <= Stats.VisionRange)
		{
			// Lanzar un raycast para detectar si hay obstáculos
			RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, Stats.VisionRange, obstaclesLayer);
			if (hit.collider == null || hit.collider.transform == player)
			{
				Debug.Log("Player in sight");
				LastKnownPlayerPosition = player.transform.position;
				OnPlayerSeen?.Invoke();
				return true; // El jugador está dentro de la visión
			}
		}

		LastKnownPlayerPosition = Vector2.zero;
		return false; // No puede ver al jugador
	}

	// Dibujar el cono de visión con Gizmos
	private void OnDrawGizmosSelected()
	{
		if (Application.isPlaying == false) return;

		Gizmos.color = Color.green;

		// Dibujar la línea central del cono de visión
		Vector3 forwardDirection = transform.up * Stats.VisionRange;
		Gizmos.DrawLine(transform.position, transform.position + forwardDirection);

		// Dibujar los límites del ángulo de visión
		Vector3 rightLimit = Quaternion.Euler(0, 0, Stats.VisionAngle) * forwardDirection;
		Vector3 leftLimit = Quaternion.Euler(0, 0, -Stats.VisionAngle) * forwardDirection;

		Gizmos.DrawLine(transform.position, transform.position + rightLimit);
		Gizmos.DrawLine(transform.position, transform.position + leftLimit);

		// Dibujar un arco entre los límites del ángulo de visión
		DrawVisionArc();
	}

	private void DrawVisionArc()
	{
		int segments = 30; // Cuantos segmentos para el arco
		float angleStep = Stats.VisionAngle * 2 / segments; // Tamaño de cada segmento en grados

		Vector3 previousPoint = transform.position + (Quaternion.Euler(0, 0, -Stats.VisionAngle) * (transform.up * Stats.VisionRange));

		for (int i = 1; i <= segments; i++)
		{
			float currentAngle = -Stats.VisionAngle + i * angleStep;
			Vector3 nextPoint = transform.position + (Quaternion.Euler(0, 0, currentAngle) * (transform.up * Stats.VisionRange));

			Gizmos.DrawLine(previousPoint, nextPoint);
			previousPoint = nextPoint;
		}
	}
}