using UnityEngine;


public class EnemyMovement : EnemyComponent
{
	public bool IsMoving { get; private set; }

	private Vector2 _targetPosition;
	private float _finalSpeed;

	private void Update()
	{
		if (IsMoving) Move();
	}

	public void MoveTo(Vector3 target)
	{
		Debug.Log("Moving to " + target);
		IsMoving = true;
		_targetPosition = target;

		// Calcular la dirección hacia el objetivo ajustado
		var direction = target - transform.position;
		_finalSpeed = (2 - Mathf.Abs(direction.normalized.y)) / 2 * Stats.Speed;
	}

	private void Move()
	{
		// Mover al personaje en línea recta hacia el objetivo con velocidad constante
		transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _finalSpeed * Time.deltaTime);

		// Verificar si el personaje ha alcanzado el objetivo real
		if (Vector2.Distance(transform.position, _targetPosition) < .1f)
		{
			Debug.Log("Reached target");
			IsMoving = false;
		}
	}
}