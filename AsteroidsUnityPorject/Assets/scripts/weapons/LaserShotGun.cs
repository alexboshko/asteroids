using UnityEngine;
using System.Collections;

/// <summary>
/// Fires a fan of shots
/// </summary>
public class LaserShotGun : Weapon 
{
	public override string DisplayName
	{
		get { return "Laser Shotgun"; }
	}

	public int ShotsPerShot;
	public float FanAngle;

	public override void LaunchingLogic()
	{
		Quaternion angleStep = Quaternion.AngleAxis(FanAngle / (ShotsPerShot - 1), Vector3.up);
		Vector3 direction = Quaternion.AngleAxis(-FanAngle * 0.5f, Vector3.up) * Owner.Model.transform.TransformDirection(Vector3.forward);

		for (int i = 0; i < ShotsPerShot; ++i)
		{
			LaserShot shot = InstantiationUtils<LaserShot>.Instantiate(PrefabsHolder.Instance.LaserShot);
			shot.Velocity = direction * shot.MaxVelocity;
			shot.transform.position = this.transform.position;
			shot.transform.rotation = Quaternion.LookRotation(shot.Velocity, Vector3.up);

			direction = angleStep * direction;
		}

		this.audio.Play();
	}
}
