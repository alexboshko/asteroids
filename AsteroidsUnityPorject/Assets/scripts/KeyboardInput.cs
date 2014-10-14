using UnityEngine;
using System.Collections;

/// <summary>
/// Script for implementing keyboard input; Could be used as a templete for implementing other kinds of input (mouse, touches, etc.)
/// </summary>
public class KeyboardInput : MonoBehaviour 
{
	public float MovementSensivity;
	public float RotationSensivity;

	[System.NonSerialized]
	public bool BlockControls = false;

	void Update () 
	{
		if (BlockControls)
			return;

		var spaceShip = GameController.Instance.PlayerShip;

		if (Input.GetKey(KeyCode.UpArrow))
		{
			spaceShip.Accelerate(Vector3.forward * MovementSensivity * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			spaceShip.Accelerate(Vector3.back * MovementSensivity * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			spaceShip.AccelerateAngular(-1.0f * RotationSensivity * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			spaceShip.AccelerateAngular(1.0f * RotationSensivity * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameController.Instance.FinishGame();
		}

		if (Input.GetKey(KeyCode.Space))
		{
			spaceShip.CurrentWeapon.Launch();
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			spaceShip.NextWeapon();
		}
	}
}
