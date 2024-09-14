using UnityEngine;

using Cysharp.Threading.Tasks;


public class EnemyMovement : EnemyComponent
{
	public bool IsMoving { get; private set; }
	public bool ReachedDestination { get; private set; }

	private Vector2 _targetPosition;
	private Quaternion targetRotation;
	private float _finalSpeed;

	private void Update()
	{
		if (IsMoving) Move();
	}

	public void MoveTo(Vector3 target)
	{
		Debug.Log("Moving to " + target + " at speed " + Stats.Speed);
		_targetPosition = target;
		ReachedDestination = false;

		// Calcular la dirección hacia el objetivo ajustado
		var direction = target - transform.position;
		_finalSpeed = (2 - Mathf.Abs(direction.normalized.y)) / 2 * Stats.Speed;

		UniTask.Void(async () =>
		{
			await RotateTowards(target);
			IsMoving = true;
		});
	}

	public async UniTask RotateTowards(Vector3 target)
	{
		Vector2 direction = target - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

		while (transform.rotation != targetRotation)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Stats.TurnSpeed * Time.deltaTime);
			await UniTask.Yield();
		}
	}

	private void Move()
	{
		// Mover al personaje en línea recta hacia el objetivo con velocidad constante
		transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _finalSpeed * Time.deltaTime);

		// Verificar si el personaje ha alcanzado el objetivo real
		if (Vector2.Distance(transform.position, _targetPosition) < .1f)
		{
			Debug.Log("Reached target");
			ReachedDestination = true;
			IsMoving = false;
		}
	}
}