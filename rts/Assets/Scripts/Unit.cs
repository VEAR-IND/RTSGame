using UnityEngine;
using System.Collections;

/*
 * This script should be attached to all controllable units in the game, wether they are walkable or not!!
 */

public class Unit : MonoBehaviour 
{
	//For Mouse_Controll.cs
	public Vector2 ScreenPos;
	public bool OnScreen;
	public bool Selected = false;

	public bool isWalkable = true;


	void Awake()
	{
		Physics.IgnoreLayerCollision (9, 9, true);
	}

	void Update()
	{
		//if unit not selected, get screen space
		if (!Selected) 
		{
			//track the screen position
			ScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);

			//if within the screen space
			if (Mouse_Control.UnitWithinScreenSpace (ScreenPos)) {
				//and not already added to UnitsOnScreen, add it

				if (!OnScreen) 
				{
					Mouse_Control.UnitsOnScreen.Add (this.gameObject);
					OnScreen = true;
				}

				//unit isnt in screen space..
			}
			else 
			{ //remove if previously on the screen
				if (OnScreen) 
				{
					Mouse_Control.RemoveFromOnScreenUnits (this.gameObject);
				}
			}
		}

	}
}
