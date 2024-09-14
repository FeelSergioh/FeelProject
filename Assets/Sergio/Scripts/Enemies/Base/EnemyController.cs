using System;

using UnityEngine;


public class EnemyController : MonoBehaviour
{
	[SerializeField] private EnemyStats _stats;

	private IEnemyAction _currentAction;
	private IEnemyState _currentState;

	public EnemyMovement Movement { get; private set; }
	public EnemyVision Vision { get; private set; }

	private void Start()
	{
		if (_stats == null)
		{
			Debug.LogError("EnemyStats not found on " + gameObject.name);
			return;
		}

		InitializeEnemy();
	}

	private void Update()
	{
		_currentAction?.UpdateAction();
		_currentState?.Execute();
	}

	public EnemyStats GetStats() { return _stats; }

	public void SetState<T>() where T : IEnemyState
	{
		var incomingState = Type.GetType(typeof(T).Name);
		Debug.Log("Setting state: " + incomingState);

		var newState = (IEnemyState)Activator.CreateInstance(incomingState);
		_currentState?.Exit();

		_currentState = newState;
		_currentState?.Enter(this);
	}

	public void SetAction<T>() where T : IEnemyAction
	{
		var incomingAction = Type.GetType(typeof(T).Name);
		Debug.Log("Setting action: " + incomingAction);

		var newAction = (IEnemyAction)Activator.CreateInstance(incomingAction);
		_currentAction?.EndAction();

		_currentAction = newAction;
		_currentAction?.StartAction(this);
	}

	private void InitializeEnemy()
	{
		Movement = GetComponent<EnemyMovement>();
		Vision = GetComponent<EnemyVision>();

		SetState<PatrollingState>();
	}
}