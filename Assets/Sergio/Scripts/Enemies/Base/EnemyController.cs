using System;
using System.Collections.Generic;

using UnityEngine;


public class EnemyController : MonoBehaviour, IDamageable
{
	[SerializeField] private EnemyStats _stats;

	public IEnemyAction CurrentAction { get; private set; }
	public IEnemyState CurrentState { get; private set; }

	public EnemyMovement Movement { get; private set; }
	public EnemyShooting Shooting { get; private set; }
	public EnemyVision Vision { get; private set; }

	private readonly Dictionary<Type, IEnemyAction> _actions = new ();
	private readonly Dictionary<Type, IEnemyState> _states = new ();

	private void Start()
	{
		if (_stats == null)
		{
			Debug.LogError("EnemyStats not found on " + gameObject.name);
			return;
		}

		InitializeEnemy();
	}

	public EnemyStats GetStats() { return _stats; }

	public void SetState<T>() where T : IEnemyState, new()
	{
		if (!_states.ContainsKey(typeof(T)))
		{
			_states[typeof(T)] = new T();
		}

		var incomingState = _states[typeof(T)];
		Debug.Log("Setting state: " + incomingState);

		CurrentState?.Exit();
		CurrentState = incomingState;
		CurrentState?.Enter(this);
	}

	public void SetAction<T>() where T : IEnemyAction, new()
	{
		if (!_actions.ContainsKey(typeof(T)))
		{
			_actions[typeof(T)] = new T();
		}

		var incomingAction = _actions[typeof(T)];
		Debug.Log("Setting action: " + incomingAction);

		CurrentAction?.EndAction();
		CurrentAction = incomingAction;
		CurrentAction?.StartAction(this);
	}

	private void InitializeEnemy()
	{
		Movement = GetComponent<EnemyMovement>();
		Shooting = GetComponent<EnemyShooting>();
		Vision = GetComponent<EnemyVision>();

		SetState<PatrollingState>();
	}

	public void TakeDamage(float amount)
	{
		_stats.Health -= amount;
		if (_stats.Health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		// Lógica para la muerte del enemigo
		Debug.Log("Enemy died", gameObject);
		Destroy(gameObject);
	}
}