using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// This is main "Message Box" of the game; Fancy animation included as well as ability to present series of messages in a row;
/// </summary>
public class TransitionController : MonoBehaviour 
{
	private static TransitionController instance;
	public static TransitionController Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Object.FindObjectOfType<TransitionController>();
				if (instance == null)
				{
					instance = InstantiationUtils<TransitionController>.Instantiate(Resources.Load("prefabs/ui/TransitionWindow") as GameObject);
				}
			}
			return instance;
		}
	}

	public UILabel MessageLabel;
	public UISprite FadeSprite;

	public float AnimationDuration;
	public float TextDisappearDuration;
	public float TextTypingSpeed;
	public int FontSize;
	public AnimationCurve TransitionCurve;

	private enum ETransitionState
	{
		Initialized,
		Presented,
	}
	private ETransitionState state = ETransitionState.Initialized;

	public void Start()
	{
		Object.DontDestroyOnLoad(this);
	}

	/// <summary>
	/// Show message with animation; Make sure you don't call it unless previous call is completed;
	/// </summary>
	/// <param name="instantly">Skips fade in animation; Use it to make message looks like it has already been on the scene upon it's loading</param>
	/// <param name="message">Message to display</param>
	/// <param name="finishedCallback">Called when animation is finished</param>
	/// <param name="typeLetters">If false, letters won't appear with typing animation but with simple fade one</param>
	/// <returns></returns>
	public IEnumerator Show(bool instantly, string message = null, System.Action finishedCallback = null, bool typeLetters = true)
	{
		if (instantly)
		{
			FadeSprite.alpha = 1.0f;
		}
		else if (state == ETransitionState.Initialized)
		{
			FadeSprite.alpha = 0.0f;
			MessageLabel.text = null;
			yield return DOTween.To(() => FadeSprite.alpha, (alpha) => FadeSprite.alpha = alpha, 1.0f, AnimationDuration).SetEase(TransitionCurve).WaitForCompletion();
		}
		else
		{
			// this branch is used if message is requested and TransitionController already is showing something
			yield return DOTween.To(() => MessageLabel.alpha, (alpha) => MessageLabel.alpha = alpha, 0.0f, TextDisappearDuration).SetEase(TransitionCurve).WaitForCompletion();
		}

		MessageLabel.fontSize = FontSize;

		if (typeLetters)
		{
			MessageLabel.alpha = 1.0f;
			MessageLabel.text = null;
			var tweener = DOTween.To(() => MessageLabel.text.Length, (letter) => MessageLabel.text = message.Substring(0, letter), message.Length, TextTypingSpeed);
			yield return tweener.SetSpeedBased().SetEase(Ease.Linear).WaitForCompletion();
			MessageLabel.text = message;
		}
		else 
		{
			MessageLabel.text = message;
			MessageLabel.alpha = 0.0f;
			yield return DOTween.To(() => MessageLabel.alpha, (alpha) => MessageLabel.alpha = alpha, 1.0f, TextDisappearDuration).SetEase(TransitionCurve).WaitForCompletion();
		}

		yield return new WaitForSeconds(0.5f);
		state = ETransitionState.Presented;

		if (finishedCallback != null)
			finishedCallback();
	}

	public IEnumerator Hide(System.Action finishedCallback = null)
	{
		yield return DOTween.To(() => MessageLabel.alpha, (alpha) => MessageLabel.alpha = alpha, 0.0f, TextDisappearDuration).SetEase(TransitionCurve).WaitForCompletion();
		yield return DOTween.To(() => FadeSprite.alpha, (alpha) => FadeSprite.alpha = alpha, 0.0f, AnimationDuration).SetEase(TransitionCurve).WaitForCompletion();

		if (finishedCallback != null)
			finishedCallback();

		Object.Destroy(this.gameObject);
	}
}
