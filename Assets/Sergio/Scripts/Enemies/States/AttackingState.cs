

public class AttackingState : IEnemyState
{
	public EnemyController Controller { get; set; }

	public void Enter(EnemyController controller)
	{
		Controller = controller;
		// Configurar el estado de ataque
	}

	public void Execute()
	{
		// Ejecutar el comportamiento de ataque
		// Si el enemigo pierde al jugador o cumple con alguna condición, cambia de estado
		// if (/* condición para patrullar */)
		// {
		// 	Controller.SetState(Controller.GetComponent<PatrollingState>());
		// }
	}

	public void Exit()
	{
		// Limpiar al salir del estado de ataque
	}
}