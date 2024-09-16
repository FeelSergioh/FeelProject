
using UnityEngine;


public class BasicShooting : EnemyShooting
{
	public override void Shoot(Transform target = null)
	{
		base.Shoot(target);
		Debug.Log("Basic shooting");
	}
}