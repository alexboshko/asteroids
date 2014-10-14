using UnityEngine;
using System.Collections;

/// <summary>
/// Laser gun weapon. Shoots simple shots :) Also treats itself like a pivot point for launching missiles from right place of the ship model;
/// </summary>
public class LaserGun : Weapon 
{
	public override string DisplayName
	{
		get { return "Laser Gun"; }
	}

	public override void LaunchingLogic()
	{
		LaserShot shot = InstantiationUtils<LaserShot>.Instantiate(PrefabsHolder.Instance.LaserShot);
		shot.Velocity = Owner.Model.transform.TransformDirection(Vector3.forward) * shot.MaxVelocity;
		shot.transform.position = this.transform.position;
		shot.transform.rotation = Owner.Model.transform.rotation;
		this.audio.Play();
	}
}
