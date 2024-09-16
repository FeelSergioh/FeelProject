
public class ShootAction : EnemyAction
{
	public override void StartAction(EnemyController controller)
	{
		_controller = controller;

		if (_controller.Vision.CanSeePlayer())
		{
			_controller.Shooting.Shoot();
		}
	}

	public override void EndAction()
	{
		// Finalizar la acción de disparo
	}
}