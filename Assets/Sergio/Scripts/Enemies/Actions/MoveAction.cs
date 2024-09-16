using UnityEngine;


public class MoveAction : EnemyAction
{
	public override void StartAction(EnemyController controller)
	{
		_controller = controller;
		_controller.Movement.OnDestinationReached += OnReachedDestination;

		// Get the target position
		Vector2 targetPosition;
		if (_controller.Vision.LastKnownPlayerPosition != Vector2.zero)
		{
			targetPosition = _controller.Vision.LastKnownPlayerPosition;
		}
		else
		{
			targetPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
		}

		// Move to the target position
		_controller.Movement.MoveTo(targetPosition);
	}

	private void OnReachedDestination()
	{
		Debug.Log("Destination reached, switching to WatchAction...");
		_controller.SetAction<WatchAction>();
	}

	public override void EndAction()
	{
		// Finalizar la acción de movimiento
		_controller.Movement.OnDestinationReached -= OnReachedDestination;
		_controller.Movement.StopMovement();
	}
}