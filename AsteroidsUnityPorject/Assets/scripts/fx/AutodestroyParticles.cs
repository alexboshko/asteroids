using UnityEngine;
using System.Collections;

/// <summary>
/// Utility class to destroy inactive particles emitters
/// </summary>
public class AutodestroyParticles : MonoBehaviour
{
	void OnEnable()
	{
		StartCoroutine(CheckEmitters());
	}
	
	IEnumerator CheckEmitters()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			if(!this.GetComponent<ParticleSystem>().IsAlive(true))
			{
				GameObject.Destroy(this.gameObject);
			}
		}
	}
}
