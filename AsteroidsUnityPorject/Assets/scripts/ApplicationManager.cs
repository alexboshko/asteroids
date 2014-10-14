using UnityEngine;
using System.Collections;

/// <summary>
/// Here is all functionality and data that is required throughout all lifetime of applicaiton; Always stays in memory
/// </summary>
public class ApplicationManager : MonoBehaviour 
{
	public static ApplicationManager Instance
	{
		get { return SingletonUtils<ApplicationManager>.Instance; }
	}

	public static readonly string SCORE_KEY = "scores";
	public static readonly string GAME_SCENE = "GameScene";
	public static readonly string MENU_SCENE = "MenuScene";

	public int Highscore
	{
		get { return PlayerPrefs.GetInt(SCORE_KEY, 0); }
		set { PlayerPrefs.SetInt(SCORE_KEY, value); }
	}

	[System.NonSerialized]
	public bool IntroFinished = false;

	public void UpdateHighscore(int newScore)
	{
		if (newScore > this.Highscore)
		{
			this.Highscore = newScore;
		}
	}

	public void SwitchScene(string sceneName)
	{
		TransitionController.Instance.StartCoroutine(TransitionController.Instance.Show(false, "Loading...", () =>
		{
			Application.LoadLevel(sceneName);
		}));
	}

	void Awake()
	{
		Application.targetFrameRate = 60;
		Object.DontDestroyOnLoad(this.gameObject);
		if (this != ApplicationManager.Instance)
		{
			Object.Destroy(this.gameObject);
		}
	}
}
