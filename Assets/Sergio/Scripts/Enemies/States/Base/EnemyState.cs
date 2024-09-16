
public abstract  class EnemyState : IEnemyState
{
	protected EnemyController _controller;
	public abstract void Enter(EnemyController controller);
	public abstract void Exit();
}