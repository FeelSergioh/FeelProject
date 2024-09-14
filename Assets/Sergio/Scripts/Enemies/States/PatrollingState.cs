
using System;

public class PatrollingState : IEnemyState
{
	public EnemyController Controller { get; set; }

	public void Enter(EnemyController controller)
	{
		Controller = controller;
		Controller.SetAction<MoveAction>();
	}

	public void Execute()
	{
		// Ejecutar el comportamiento de patrullaje
		// Si detecta al jugador o cumple con alguna condición, cambia de estado
		// if (/* condición para atacar */)
		// {
		// 	Controller.SetState(Controller.GetComponent<AttackingState>());
		// }
	}

	public void Exit()
	{
		// Limpiar al salir del estado de patrullaje
	}
}