using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Simple factories to generate asteroids; Main purpose is to keep logic code clean from bulky initialization
/// </summary>
public class AsteroidFactory  
{
	public static IEnumerable<Asteroid> GetAsteroids(int asteroidsCount, params Model[] modelsToAvoid)
	{
		List<Asteroid> asteroids = new List<Asteroid>();

		for (int i = 0; i < asteroidsCount; ++i)
		{
			Asteroid newAsteroid = InstantiationUtils<Asteroid>.Instantiate(PrefabsHolder.Instance.Asteroid);
			Bounds asteroidBounds;
			do
			{
				Vector3 randomPosition = MainCamera.Instance.camera.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
				randomPosition.y = 0.0f;
				newAsteroid.transform.position = randomPosition;

				asteroidBounds = newAsteroid.Model.Bounds;
				asteroidBounds.Expand(asteroidBounds.size * 5.0f);
			}
			// that's actually is a rather poor method to avoid restricted areas, but still is quite fast to implement and passable considering small ship size;
			while (modelsToAvoid.FirstOrDefault(model => asteroidBounds.Intersects(model.Bounds)) != null);
			
			Vector3 randomVelocity = Random.insideUnitCircle.normalized * newAsteroid.MaxVelocity;
			randomVelocity.z = randomVelocity.y;
			randomVelocity.y = 0.0f;

			newAsteroid.Velocity = randomVelocity;
			newAsteroid.AngularVelocity = newAsteroid.MaxAngularVelocity;

			asteroids.Add(newAsteroid);
		}

		return asteroids;
	}

	public static IEnumerable<Asteroid> SplitAsteroid(Asteroid parentAsteroid)
	{
		List<Asteroid> asteroids = new List<Asteroid>();

		float childDiverseAngle = 45.0f;
		Quaternion[] rotations = new Quaternion[] { Quaternion.AngleAxis(childDiverseAngle, Vector3.up), Quaternion.AngleAxis(-childDiverseAngle, Vector3.up) };

		for (int i = 0; i < rotations.Length; ++i)
		{
			Asteroid newAsteroid = InstantiationUtils<Asteroid>.Instantiate(PrefabsHolder.Instance.Asteroid);
			newAsteroid.transform.position = parentAsteroid.transform.position;
			newAsteroid.Velocity = rotations[i] * parentAsteroid.Velocity;
			newAsteroid.AngularVelocity = newAsteroid.MaxAngularVelocity;
			newAsteroid.AsteroidLevel = parentAsteroid.AsteroidLevel + 1;

			asteroids.Add(newAsteroid);
		}

		return asteroids;
	}
}
