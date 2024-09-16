

public class AttackingState : EnemyState
{
	public override void Enter(EnemyController controller)
	{
		_controller = controller;
		_controller.SetAction<ShootAction>();
	}

	public override void Exit()
	{
		// Limpiar al salir del estado de ataque
	}
}