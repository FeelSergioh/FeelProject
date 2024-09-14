using UnityEngine;

public abstract class EnemyComponent : MonoBehaviour
{
	protected EnemyController Controller;
	protected EnemyStats Stats;

	protected virtual void Awake()
	{
		Controller = GetComponent<EnemyController>();
		if (Controller == null)
		{
			Debug.LogError("EnemyController not found on " + gameObject.name);
		}

		Stats = Controller.GetStats();
	}
}