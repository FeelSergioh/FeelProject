using System;
using System.Threading;

using UnityEngine;

using Cysharp.Threading.Tasks;


public class WatchAction : EnemyAction
{
	private CancellationTokenSource _cancellationTokenSource;

	public override void StartAction(EnemyController controller)
	{
		_controller = controller;
		_cancellationTokenSource = new CancellationTokenSource();

		if (_controller.Vision.CanSeePlayer())
		{
			_controller.SetState<AttackingState>();
			return;
		}

		Debug.Log("Player not found, watching...");
		PerformWatchAsync(_cancellationTokenSource.Token).Forget();
	}

	private async UniTaskVoid PerformWatchAsync(CancellationToken token)
	{
		try
		{
			// Realizar rotación condicional
			if (_controller.Vision.LastKnownPlayerPosition != Vector2.zero)
			{
				await _controller.Movement.RotateTowards(_controller.Vision.LastKnownPlayerPosition, token);
			}
			else
			{
				await _controller.Movement.LookAround(200, token);
				await _controller.Movement.LookAround(200, token);
			}

			// Retraso antes de moverse
			await UniTask.Delay(500, cancellationToken: token);
			_controller.SetAction<MoveAction>();
		}
		catch (OperationCanceledException)
		{
			Debug.Log("Watch action cancelled.");
		}
	}

	public override void EndAction()
	{
		// Finalizar la acción de movimiento
		_cancellationTokenSource?.Cancel();
	}
}