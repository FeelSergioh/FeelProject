public interface IEnemyState
{
	EnemyController Controller { get; set; }
	void Enter(EnemyController controller);
	void Execute();
	void Exit();
}