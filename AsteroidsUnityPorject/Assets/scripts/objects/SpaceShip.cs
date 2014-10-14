using UnityEngine;
using System.Collections;

/// <summary>
/// Main class for player ship; Has additional kinematic values and functions to be called by controls;
/// </summary>
public class SpaceShip : SpaceObject 
{
	public float VelocityDrag;
	public float AngularDrag;

	private SpaceShipPropulsion shipPropulsionController;
	private Weapon mainWeapon;

	private Vector3? previousAccelerationInput;
	private float? previousAngularInput;

	public override void Awake()
	{
		base.Awake();

		shipPropulsionController = this.GetComponentInChildren<SpaceShipPropulsion>();
		mainWeapon = this.GetComponentInChildren<Weapon>();
		mainWeapon.Owner = this;
	}

	public void Accelerate(Vector3 localAcceleration)
	{
		Velocity += this.Model.transform.TransformDirection(localAcceleration);
		previousAccelerationInput = localAcceleration;
	}

	public void AccelerateAngular(float angularAcceleration)
	{
		AngularVelocity += angularAcceleration;
		previousAngularInput = angularAcceleration;
	}

	public void FireMainWeapon()
	{
		mainWeapon.Launch();
	}

	public override void Update()
	{
		velocity -= velocity * VelocityDrag * Time.deltaTime;
		angularVelocity -= angularVelocity * AngularDrag * Time.deltaTime;

		shipPropulsionController.UpdateWithInput(previousAccelerationInput, previousAngularInput);
		previousAccelerationInput = null;
		previousAngularInput = null;

		base.Update();
	}

	public override void Destroy()
	{
		var explosion = Object.Instantiate(PrefabsHolder.Instance.PlayerExplosion) as GameObject;
		explosion.transform.position = this.transform.position;
		AudioSource.PlayClipAtPoint(PrefabsHolder.Instance.LoosingSound, Vector3.zero);

		this.gameObject.SetActive(false);
		base.Destroy();
	}

	public override void Ressurect()
	{
		this.gameObject.SetActive(true);
		base.Ressurect();
	}
}
