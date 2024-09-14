using UnityEngine;


public class MoveAction : IEnemyAction
{
	public EnemyController Controller { get; set; }

	public void StartAction(EnemyController controller)
	{
		Controller = controller;
		// Get the target position
		Vector2 targetPosition = new (Random.Range(-5f, 5f), Random.Range(-5f, 5f));

		// Move to the target position
		Controller.Movement.MoveTo(targetPosition);
	}

	public void UpdateAction()
	{
		// Check for any movement updates
		if (Controller.Movement.ReachedDestination)
		{
			Controller.SetAction<WatchAction>();
		}
	}

	public void EndAction()
	{
		// Finalizar la acción de movimiento
	}
}