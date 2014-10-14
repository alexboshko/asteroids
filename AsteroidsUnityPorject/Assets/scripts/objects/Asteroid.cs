using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for Asteroid; Main purpose is to chose random model, set proper scale, etc.
/// </summary>
public class Asteroid : SpaceObject 
{
	public int ScorePrice;

	private int asteroidLevel;
	private Vector3 originalScale;

	public int AsteroidLevel
	{
		get { return asteroidLevel; }
		set
		{
			asteroidLevel = value;
			this.UpdateScale();
		}
	}

	protected void UpdateScale()
	{
		if (Model != null)
		{
			float scaleMultiplier = 1.0f - asteroidLevel * 0.15f;
			scaleMultiplier = Mathf.Clamp(scaleMultiplier, 0.3f, 1.0f);
			Model.transform.localScale = originalScale * scaleMultiplier;
		}
	}

	public override void Awake()
	{
		base.Awake();
		this.UpdateScale();
		rotationAxis = Random.insideUnitSphere;
	}

	protected override Model GetModel()
	{
		var models = this.GetComponentsInChildren<Model>();
		var result = models[Random.Range(0, models.Length)];
		foreach (var model in models)
		{
			model.gameObject.SetActive(model == result);
		}
		originalScale = result.transform.localScale;
		return result;
	}

	public override void Destroy()
	{
		base.Destroy();
		var explosion = Object.Instantiate(PrefabsHolder.Instance.AsteroidExplosion) as GameObject;
		explosion.transform.position = this.transform.position;
		GameObject.Destroy(this.gameObject);
	}
}
