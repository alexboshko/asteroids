using UnityEngine;
using System.Collections;

/// <summary>
/// Laser shot controller; Animates it and manages shot lifetime;
/// </summary>
public class LaserShot : SpaceObject 
{
	public AnimationCurve FadeOutCurve;
	public float Lifetime;

	private float currentLifetime = 0.0f;

	public override void Update()
	{
		if (currentLifetime < Lifetime)
		{
			currentLifetime += Time.deltaTime;
			Model.renderer.material.SetFloat("_Alpha", FadeOutCurve.Evaluate(currentLifetime / Lifetime));
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}
		base.Update();
	}

	public override void Destroy()
	{
		GameObject.Destroy(this.gameObject);
		base.Destroy();
	}

	public override void Awake()
	{
		Model = this.GetModel();
	}
}
