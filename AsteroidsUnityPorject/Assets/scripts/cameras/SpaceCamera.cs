using UnityEngine;
using System.Collections;

/// <summary>
/// Script for each camera in the game; Checks whether is object visible on the screen
/// </summary>
public class SpaceCamera : MonoBehaviour 
{
	public bool IsObjectVisible(SpaceObject spaceObject)
	{
		Vector2 viewportPosition = this.camera.WorldToViewportPoint(spaceObject.transform.position);
		return viewportPosition.x > 0.0f && viewportPosition.x < 1.0f && viewportPosition.y > 0.0f && viewportPosition.y < 1.0f;
	}
}
