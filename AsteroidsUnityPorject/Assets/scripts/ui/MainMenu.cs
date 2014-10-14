using UnityEngine;
using System.Collections;

/// <summary>
/// Class for handling main menu logic and presenting game intro;
/// </summary>
public class MainMenu : MonoBehaviour 
{
	public GameObject StartButton;
	public UILabel HighscoreLabel;

	public void Start() 
	{
		if (!ApplicationManager.Instance.IntroFinished)
		{
			StartCoroutine(Intro());
		}
		else
		{
			StartCoroutine(TransitionController.Instance.Hide(() => InitCallbacks()));
		}

		if (ApplicationManager.Instance.Highscore != 0)
		{
			HighscoreLabel.text = "Highscore: " + ApplicationManager.Instance.Highscore;
		}
		else
		{
			HighscoreLabel.text = null;
		}
	}

	public void InitCallbacks()
	{
		UIEventListener.Get(StartButton).onClick += (button) =>
		{
			StartButton.collider.enabled = false;
			ApplicationManager.Instance.SwitchScene(ApplicationManager.GAME_SCENE);
		};
	}

	private IEnumerator Intro()
	{
		TransitionController controller = TransitionController.Instance;
		controller.TextDisappearDuration = 1.0f;
		controller.TextTypingSpeed = 15.0f;

		yield return StartCoroutine(controller.Show(true, "Home Made Production"));
		yield return StartCoroutine(controller.Show(false, "Directed by Alex Boshko"));
		yield return StartCoroutine(controller.Show(false, "Presents task do domu"));
		controller.FontSize = 40;
		yield return StartCoroutine(controller.Show(false, "Asteroids", null, false));
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine(controller.Hide());
		ApplicationManager.Instance.IntroFinished = true;
		InitCallbacks();
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !ApplicationManager.Instance.IntroFinished)
		{
			StopAllCoroutines();
			Object.Destroy(TransitionController.Instance.gameObject);
			ApplicationManager.Instance.IntroFinished = true;
			InitCallbacks();
		}
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
