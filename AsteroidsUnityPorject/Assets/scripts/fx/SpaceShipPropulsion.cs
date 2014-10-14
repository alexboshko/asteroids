using UnityEngine;
using System.Collections;

/// <summary>
/// This class is controlling propulsion jet particles to represent current acceleration according player input
/// </summary>
public class SpaceShipPropulsion : MonoBehaviour 
{
	public ParticleSystem RearLeftPropulsion;
	public ParticleSystem RearRightPropulsion;
	public ParticleSystem FrontLeftPropulsion;
	public ParticleSystem FrontRightPropulsion;

	public void UpdateWithInput(Vector3? localAcceleration, float? angularAcceleration)
	{
		RearLeftPropulsion.enableEmission = (localAcceleration != null && localAcceleration.Value.z > 0.0f) || (angularAcceleration != null && angularAcceleration > 0.0f);
		RearRightPropulsion.enableEmission = (localAcceleration != null && localAcceleration.Value.z > 0.0f) || (angularAcceleration != null && angularAcceleration < 0.0f);
		FrontLeftPropulsion.enableEmission = (localAcceleration != null && localAcceleration.Value.z < 0.0f) || (angularAcceleration != null && angularAcceleration < 0.0f);
		FrontRightPropulsion.enableEmission = (localAcceleration != null && localAcceleration.Value.z < 0.0f) || (angularAcceleration != null && angularAcceleration > 0.0f);
	}
}
