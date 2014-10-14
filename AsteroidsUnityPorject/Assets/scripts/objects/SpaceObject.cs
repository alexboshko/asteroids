using UnityEngine;
using System.Collections;

/// <summary>
/// Main class for all gameplay-related objects; Resets own coordinates to keep itself in gamespace, holds kinematic data
/// like velocity, etc. Also has simple mechanism to prevent objects rendering inside each other (that's a 2D game, right? :)
/// </summary>
public class SpaceObject : MonoBehaviour 
{
	public float MaxVelocity;
	public float MaxAngularVelocity;

	[System.NonSerialized]
	public Model Model;

	protected bool destroyed;

	protected Vector3 velocity;
	protected float angularVelocity;

	protected Vector3 rotationAxis = Vector3.up;
	protected static int renderQueue = 2000; // default geometry queue

	public Vector3 Velocity
	{
		set
		{
			velocity = value;

			if (velocity.magnitude > MaxVelocity)
				velocity = velocity.normalized * MaxVelocity;
		}
		get { return velocity; }
	}

	public float AngularVelocity
	{
		set { angularVelocity = Mathf.Clamp(value, -MaxAngularVelocity, MaxAngularVelocity); }
		get { return angularVelocity; }
	}

	public bool Destroyed
	{
		get { return destroyed; }
	}

	protected virtual Model GetModel()
	{
		return this.GetComponentInChildren<Model>();
	}

	public virtual void Awake()
	{
		// this is used to prevent objects penetrating each other visually (asteroids mainly)
		var depthCleaner = Object.Instantiate(PrefabsHolder.Instance.DepthCleaner) as GameObject;
		depthCleaner.transform.parent = this.transform;
		depthCleaner.transform.localPosition = Vector3.down * 10.0f;

		Model = this.GetModel();
		Model.RenderQueue = renderQueue++;
		depthCleaner.renderer.material.renderQueue = renderQueue++;

		if (renderQueue >= 2500)
			renderQueue = 2000;
	}

	public virtual void Update() 
	{
		this.transform.position += velocity * Time.deltaTime;
		Model.transform.Rotate(rotationAxis, angularVelocity * Time.deltaTime);

		MainCamera.Instance.ResetObjectPosition(this);
	}

	public virtual void Destroy()
	{
		destroyed = true;
		Model.collider.enabled = false;
	}

	public virtual void Ressurect()
	{
		destroyed = false;
		Model.collider.enabled = true;
	}
}
