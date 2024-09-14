
using UnityEngine;

using Cysharp.Threading.Tasks;


public class WatchAction : IEnemyAction
{
    public EnemyController Controller { get; set; }

	public void StartAction(EnemyController controller)
	{
		Controller = controller;
		if (Controller.Vision.CanSeePlayer())
		{
			Controller.SetState<AttackingState>();
			return;
		}

		Debug.Log("Player not found, watching...");
		UniTask.Void(async () =>
		{
			// Look around for a few seconds
			await UniTask.Delay(200);
			await Controller.Movement.RotateTowards(Controller.transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0));
			await UniTask.Delay(200);
			await Controller.Movement.RotateTowards(Controller.transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0));
			await UniTask.Delay(500);

			// Move to a random position
			Controller.SetAction<MoveAction>();
		});
	}

	public void UpdateAction()
	{
		
	}

	public void EndAction()
	{
		// Finalizar la acción de movimiento
	}
}
