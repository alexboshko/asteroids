using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages state of the game; Switch levels, determines gameover conditions, process collisions; In-game objects owner
/// </summary>
public class GameController : MonoBehaviour 
{
	public static GameController Instance
	{
		get { return SingletonUtils<GameController>.Instance; }
	}

	public SpaceShip PlayerShip
	{
		get { return playerShip; }
	}

	public int CurrentScore
	{
		get { return currentScore; }
	}

	// how long it takes to destroy asteroid completely
	public int AsteroidMaxLevel;

	private int currentScore = 0;
	private int currentLevel = 0;
	private List<Asteroid> asteroids = new List<Asteroid>();
	private SpaceShip playerShip;
	private KeyboardInput controls;

	void Awake()
	{
		playerShip = Object.FindObjectOfType<SpaceShip>();
		controls = Object.FindObjectOfType<KeyboardInput>();
	}

	void Start() 
	{
		this.ResetGameState();
		this.StartCoroutine(NextLevelCoroutine());
	}

	public void ResetGameState()
	{
		foreach (var asteroid in asteroids)
		{
			Object.Destroy(asteroid.gameObject);
		}
		asteroids.Clear();
		playerShip.Ressurect();

		currentScore = 0;
		currentLevel = 0;
	}

	private IEnumerator NextLevelCoroutine()
	{
		currentLevel++;
		controls.BlockControls = true;

		yield return StartCoroutine(TransitionController.Instance.Show(false, "Level " + currentLevel));

		playerShip.Velocity = Vector3.zero;
		playerShip.AngularVelocity = 0.0f;
		playerShip.transform.position = Vector3.zero;
		playerShip.Model.transform.rotation = Quaternion.identity;

		asteroids.AddRange(AsteroidFactory.GetAsteroids(currentLevel, playerShip.Model));
		yield return StartCoroutine(TransitionController.Instance.Hide());

		controls.BlockControls = false;
	}

	// this is called from input controller
	public void FinishGame()
	{
		this.StartCoroutine(FinishGameCoroutine(0.0f, () =>
		{
			ApplicationManager.Instance.SwitchScene(ApplicationManager.MENU_SCENE);
		}));
	}

	private IEnumerator FinishGameCoroutine(float delay, System.Action callback = null)
	{
		controls.BlockControls = true;
		// delay to let player watch their's destruction animation
		yield return new WaitForSeconds(delay);

		string message = "Game over";
		if (currentScore > ApplicationManager.Instance.Highscore)
		{
			message += "\nNew highscore: " + currentScore;
		}
		ApplicationManager.Instance.UpdateHighscore(currentScore);

		yield return StartCoroutine(TransitionController.Instance.Show(false, message));
		yield return new WaitForSeconds(0.5f);

		if (callback != null)
			callback();
	}

	public void ProcessCollision(SpaceObject firstObject, SpaceObject secondObject)
	{
		System.Func<System.Type, SpaceObject, SpaceObject, SpaceObject> select = (type, first, second) =>
			{
				if (firstObject != null && !firstObject.Destroyed && firstObject.GetType() == type) return firstObject;
				if (secondObject != null && !secondObject.Destroyed && secondObject.GetType() == type) return secondObject;
				return null;
			};

		SpaceShip ship = select(typeof(SpaceShip), firstObject, secondObject) as SpaceShip;
		Asteroid asteroid = select(typeof(Asteroid), firstObject, secondObject) as Asteroid;
		LaserShot shot = select(typeof(LaserShot), firstObject, secondObject) as LaserShot;

		if (shot != null && asteroid != null)
		{
			asteroids.Remove(asteroid);
			shot.Destroy();
			asteroid.Destroy();

			currentScore += asteroid.ScorePrice;

			if (asteroid.AsteroidLevel < AsteroidMaxLevel)
			{
				asteroids.AddRange(AsteroidFactory.SplitAsteroid(asteroid));
			}

			if (asteroids.Count == 0)
			{
				StartCoroutine(NextLevelCoroutine());
			}
		}

		if (asteroid != null && ship != null)
		{
			ship.Destroy();
			StartCoroutine(FinishGameCoroutine(1.5f, () =>
				{
					this.ResetGameState();
					this.StartCoroutine(NextLevelCoroutine());
				}));
		}
	}
}
