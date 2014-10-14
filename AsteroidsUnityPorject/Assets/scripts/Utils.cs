using UnityEngine;
using System.Collections;

/// <summary>
/// Few utility classes to simplify our lives
/// </summary>
public class SingletonUtils<T> where T : MonoBehaviour
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<T>();
			}
			return instance;
		}
	}
}

public class InstantiationUtils<T> where T : MonoBehaviour
{
	public static T Instantiate(GameObject prefab)
	{
		var result = (Object.Instantiate(prefab) as GameObject).GetComponent<T>();
		if (result == null)
		{
			Debug.LogError("You've tried to get nonexisting component " + typeof(T).Name + " from prefab " + prefab.name);
		}
		return result;
	}
}

#if UNITY_EDITOR

public class UnityExtensions
{
	[UnityEditor.MenuItem ("Tools/Clear player prefs %&c")]
	public static void ClearPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}
}

#endif
