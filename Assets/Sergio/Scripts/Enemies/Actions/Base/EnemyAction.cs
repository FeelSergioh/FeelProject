
public abstract class EnemyAction : IEnemyAction
{
    protected EnemyController _controller;
    public abstract void StartAction(EnemyController controller);
	public abstract void EndAction();
}
