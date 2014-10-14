using UnityEngine;
using System.Collections;

/// <summary>
/// Holds links to prefabs and another resources; It's much easier to assign such things in one place than on several different scripts and prefabs; 
/// </summary>
public class PrefabsHolder : MonoBehaviour 
{
	public static PrefabsHolder Instance
	{
		get { return SingletonUtils<PrefabsHolder>.Instance; }
	}

	public GameObject Asteroid;
	public GameObject DepthCleaner;
	public GameObject LaserShot;
	public GameObject AsteroidExplosion;
	public GameObject PlayerExplosion;

	public AudioClip LoosingSound;
}
