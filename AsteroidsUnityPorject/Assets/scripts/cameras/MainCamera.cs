using UnityEngine;
using System.Collections;

/// <summary>
/// This class manages several cameras to make seamless rendering on screen borders
/// </summary>
public class MainCamera : SpaceCamera 
{
	public static MainCamera Instance
	{
		get { return SingletonUtils<MainCamera>.Instance; }
	}

	public GameObject CameraPrefab;
	SpaceCamera[] sideCameras;

	public void Start()
	{
		Vector2[] viewportPoints = new Vector2[] 
		{ 
			new Vector2(-0.5f, 1.5f), new Vector2(0.5f, 1.5f), new Vector2(1.5f, 1.5f),
			new Vector2(-0.5f, 0.5f), new Vector2(1.5f, 0.5f),
			new Vector2(-0.5f, -0.5f), new Vector2(0.5f, -0.5f), new Vector2(1.5f, -0.5f),
		};

		sideCameras = new SpaceCamera[viewportPoints.Length];

		for (int i = 0; i < sideCameras.Length; ++i)
		{
			sideCameras[i] = InstantiationUtils<SpaceCamera>.Instantiate(CameraPrefab);
			sideCameras[i].transform.parent = this.transform;

			// we can do that once, assuming the screen size won't change at runtime
			sideCameras[i].transform.position = this.camera.ViewportToWorldPoint(viewportPoints[i]);
		}
	}

	public void ResetObjectPosition(SpaceObject spaceObject)
	{
		if (!this.IsObjectVisible(spaceObject))
		{
			foreach (var sideCamera in sideCameras)
			{
				if (sideCamera.IsObjectVisible(spaceObject))
				{
					Vector3 newPosition = this.camera.ViewportToWorldPoint(sideCamera.camera.WorldToViewportPoint(spaceObject.transform.position));
					newPosition.y = spaceObject.transform.position.y;
					spaceObject.transform.position = newPosition;
				}
			}
		}
	}
}
