
using UnityEngine;


public class PatrollingState : EnemyState
{
	public override void Enter(EnemyController controller)
	{
		_controller = controller;
		_controller.Vision.OnPlayerSeen += OnSeenPlayer;
		_controller.SetAction<MoveAction>();
	}

	private void OnSeenPlayer()
	{
		Debug.Log("Player seen, switching to AttackingState...");
		_controller.SetState<AttackingState>();
	}

	public override void Exit()
	{
		// Limpiar al salir del estado de patrullaje
		_controller.Vision.OnPlayerSeen -= OnSeenPlayer;
	}
}