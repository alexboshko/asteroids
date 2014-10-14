using UnityEngine;
using System.Collections;

/// <summary>
/// This class represents object model. Used basically for collision detection and holding boundaries of object;
/// Improve this class if there is need of using complex models having more then from one Renderer
/// </summary>

public class Model : MonoBehaviour 
{
	[System.NonSerialized]
	public SpaceObject Owner;
	private Renderer cachedRenderer;
	public Renderer Renderer
	{
		get
		{
			if (cachedRenderer == null)
				cachedRenderer = this.GetComponentInChildren<Renderer>();
			return cachedRenderer;
		}
	}

	public int RenderQueue
	{
		set { Renderer.material.renderQueue = value; }
	}

	public Bounds Bounds
	{
		get { return cachedRenderer.bounds; }
	}

	void Awake() 
	{
		Owner = this.GetComponentInParent<SpaceObject>();
	}

	// Collision model is quite incosistent here; Sometimes objects are devided by the screen border and only one visible part of it would actually respond to this event
	// However, considering game dynamic it's not particulary noticable
	void OnTriggerEnter(Collider other)
	{
		Model collidedModel = other.gameObject.GetComponent<Model>();

		if (collidedModel == null)
		{
			Debug.LogError("All colliders should have Model script on it", other.gameObject);
			return;
		}

		GameController.Instance.ProcessCollision(this.Owner, collidedModel.Owner);
	}
}
