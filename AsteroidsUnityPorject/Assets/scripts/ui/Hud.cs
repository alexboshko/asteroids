using UnityEngine;
using System.Collections;

/// <summary>
/// Class for managing in-game ui
/// </summary>
public class Hud : MonoBehaviour 
{
	public UILabel ScoresLabel;
	public UILabel WeaponLabel;
	
	void Update () 
	{
		// its better to update these values only when they had changed, but let's keep code simple for now
		ScoresLabel.text = GameController.Instance.CurrentScore.ToString();
		WeaponLabel.text = GameController.Instance.PlayerShip.CurrentWeapon.DisplayName;
	}
}
