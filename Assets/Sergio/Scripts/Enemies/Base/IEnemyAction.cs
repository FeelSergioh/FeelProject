public interface IEnemyAction
{
	EnemyController Controller { get; set; }
	void StartAction(EnemyController controller);
	void UpdateAction();
	void EndAction();
}