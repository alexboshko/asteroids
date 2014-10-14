using UnityEngine;
using System.Collections;

/// <summary>
/// This is main class for all in-game weapons (only one so far). Weapon-scripts can be dropped on SpaceShipt to "arm" it;
/// </summary>
public class Weapon : MonoBehaviour 
{
	public float CooldownDuration;
	[System.NonSerialized]
	public SpaceShip Owner;

	private float currentCooldownTime;
	
	public virtual bool IsReady
	{
		get { return currentCooldownTime <= 0.0f; }
	}

	public virtual void Launch()
	{
		currentCooldownTime = CooldownDuration;
	}

	public virtual void Update()
	{
		if (currentCooldownTime > 0.0f)
			currentCooldownTime -= Time.deltaTime;
	}
}
