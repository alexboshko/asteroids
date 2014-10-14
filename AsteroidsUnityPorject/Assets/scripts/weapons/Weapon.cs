using UnityEngine;
using System.Collections;

/// <summary>
/// This is a main class for all in-game weapons. Weapon-scripts can be dropped on SpaceShipt to "arm" it;
/// </summary>
public class Weapon : MonoBehaviour 
{
	public float CooldownDuration;
	[System.NonSerialized]
	public SpaceShip Owner;

	private float currentCooldownTime;

	public virtual void Start()
	{
		Owner = this.GetComponentInParent<SpaceShip>();
	}
	
	public virtual bool IsReady
	{
		get { return currentCooldownTime <= 0.0f; }
	}

	public virtual string DisplayName
	{
		get { return null; }
	}

	public virtual void Launch()
	{
		if (this.IsReady)
		{
			this.LaunchingLogic();
			currentCooldownTime = CooldownDuration;
		}
	}

	public virtual void LaunchingLogic()
	{
	}

	public virtual void Update()
	{
		if (currentCooldownTime > 0.0f)
			currentCooldownTime -= Time.deltaTime;
	}
}
