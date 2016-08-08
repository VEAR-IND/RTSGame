using UnityEngine;
using System.Collections;

public class Mouse_Control : MonoBehaviour {

	#region Class Variables

	RaycastHit hit;


	public static Vector3 RightClickPoint;
	public static ArrayList CurrentlySelectedUnits = new ArrayList(); //of Objects
	public static ArrayList UnitsOnScreen = new ArrayList(); //of GameObject
	public static ArrayList UnitsInDrag = new ArrayList(); //of GameObject
	private bool FinishedDragOnThisFrame;

	public GUIStyle MouseDragSkin;

	public GameObject Target;

	private static Vector3 mouseDownPoint;
	private static Vector3 currentMousePoint; //in world space

	public static bool UserIsDragging;
	private static float TimeLimitBeforeDeclareDrag = 1f;
	private static float TimeLeftBeforeDeclareDrag;
	private static Vector2 MouseDragStart;

	private static float clickDragZone = 1.4f;

	//GUI
	private float boxWidth;
	private float boxHeight;
	private float boxTop;
	private float boxLeft;
	public static Vector2 boxStart;
	public static Vector2 boxFinish;

	#endregion


	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) 
		{
			currentMousePoint = hit.point;
			
			//store point at mouse button down
			if (Input.GetMouseButtonDown (0))
			{
				
				mouseDownPoint = hit.point;
				TimeLeftBeforeDeclareDrag = TimeLimitBeforeDeclareDrag;
				MouseDragStart = Input.mousePosition;

				if(!Common.ShiftKeysDown())
					DeselectGameObjectsIfSelected();

				
			} 
			else if (Input.GetMouseButton (0)) 
			{
				//if the user isnt dragging, lets do the test!
				if (!UserIsDragging) 
				{
					TimeLeftBeforeDeclareDrag -= Time.deltaTime;

					if (TimeLeftBeforeDeclareDrag <= 0f || UserDraggingByPosition(MouseDragStart, Input.mousePosition)) 
					{
						UserIsDragging = true;
					}
				}

				//Ok user is dragging, lets compete (GUI)
				if (UserIsDragging) 
				{
					Debug.Log ("Dragging !!!!!");
				}

			} 
			else if (Input.GetMouseButtonUp (0)) 
			{
				if (UserIsDragging)
					FinishedDragOnThisFrame = true;
				UserIsDragging = false;
				
			}



			//Mouse Click

			if (!UserIsDragging) {
				if (hit.collider.name == "Terrain") {
					//0 left
					//1 right
					//2middle

					if (Input.GetMouseButtonDown (1)) {
						GameObject TargetObj = Instantiate (Target, hit.point, Quaternion.identity) as GameObject;
						TargetObj.name = "Target Instantiated";
						RightClickPoint = hit.point;

					} 
					else if (Input.GetMouseButtonUp (0) && DidUserClickedLeftMouse (mouseDownPoint)) {
						if (!Common.ShiftKeysDown ())
							DeselectGameObjectsIfSelected ();

					}
				
				} //end of terrain!

			else {
					//hitting other objects
					if (Input.GetMouseButtonUp (0) && DidUserClickedLeftMouse (mouseDownPoint)) {
						
						//is the user hitting a unit &
						if (hit.collider.gameObject.GetComponent<Unit>()) 
						{
							//found a unit that we can select!


							//are we selecting different object ?
							if (!UnitAlreadyInCurrentlySelectedUnits (hit.collider.gameObject)) {
							

								//if the shift key isnt down , remove the rest of the units.
								if (!Common.ShiftKeysDown ())
									DeselectGameObjectsIfSelected ();

								GameObject SelectedObj = hit.collider.transform.FindChild ("Selected").gameObject;
								SelectedObj.active = true;

								//add unit to currently selected units
								CurrentlySelectedUnits.Add (hit.collider.gameObject);

								//Change the unit Selected value to true
								hit.collider.gameObject.GetComponent<Unit>().Selected = true;

							} else {
								//unit is already in the currently selected units arrayList
								//remove the unit!
								if (Common.ShiftKeysDown ())
									RemoveUnitFromCurrentlySelectedUnits (hit.collider.gameObject);
								else {
									DeselectGameObjectsIfSelected ();
									GameObject SelectedObj = hit.collider.transform.FindChild ("Selected").gameObject;
									SelectedObj.active = true;
									CurrentlySelectedUnits.Add (hit.collider.gameObject);
								}

							}
							
						} else {
							//If this object isnt a unit
							if (!Common.ShiftKeysDown ())
								DeselectGameObjectsIfSelected ();
						}
					}
				}
			} 
			else 
			{
				if (Input.GetMouseButtonUp (0) && DidUserClickedLeftMouse (mouseDownPoint))
				if (!Common.ShiftKeysDown ())
					DeselectGameObjectsIfSelected ();
				
			}	// end of raycast Hit
		}//end of - isDragging?

		Debug.DrawRay (ray.origin, ray.direction * 2000, Color.red);

		if (UserIsDragging) 
		{
			//GUI Variables

			boxWidth = Camera.main.WorldToScreenPoint (mouseDownPoint).x - Camera.main.WorldToScreenPoint (currentMousePoint).x;
			boxHeight = Camera.main.WorldToScreenPoint (mouseDownPoint).y - Camera.main.WorldToScreenPoint (currentMousePoint).y;

			boxLeft = Input.mousePosition.x;
			boxTop = (Screen.height - Input.mousePosition.y) - boxHeight;

			if(Common.FloatToBool(boxWidth))
				if(Common.FloatToBool(boxHeight))
					boxStart = new Vector2 (Input.mousePosition.x, Input.mousePosition.y + boxHeight);
			else
				boxStart = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			else
				if(!Common.FloatToBool(boxWidth))
					if(Common.FloatToBool(boxHeight))
						boxStart = new Vector2 (Input.mousePosition.x + boxWidth, Input.mousePosition.y + boxHeight);
				else
						boxStart = new Vector2 (Input.mousePosition.x + boxWidth, Input.mousePosition.y);


			boxFinish = new Vector2 (
				boxStart.x + Common.Unsigned(boxWidth),
				boxStart.y - Common.Unsigned(boxHeight)
			);
		}
			
	} 

    public ArrayList GetSelectedPersons()
    {
        return CurrentlySelectedUnits;
    }

	void LateUpdate()
	{
		UnitsInDrag.Clear ();

		//if user is dragging, or finished on this frame, AND there are units to select on the screen
		if ((UserIsDragging || FinishedDragOnThisFrame) && UnitsOnScreen.Count > 0) 
		{
			//loop through those units on screen
			for (int i = 0; i < UnitsOnScreen.Count; i++) 
			{
				GameObject UnitObj = UnitsOnScreen [i] as GameObject;
				Unit UnitScript = UnitObj.GetComponent<Unit> ();
				GameObject SelectedObj = UnitObj.transform.FindChild ("Selected").gameObject;

				//if not already in dragged units
				if (!UnitAlreadyInDraggedUnits (UnitObj)) 
				{
					if (UnitInsideDrag (UnitScript.ScreenPos)) {
						SelectedObj.active = true;
						UnitsInDrag.Add (UnitObj);
					}

					//unit isnt in drag
					else 
					{
						//remove the selected graphic, if unit isnt already in currently selected units
						if (!UnitAlreadyInCurrentlySelectedUnits (UnitObj))
							SelectedObj.active = false;
					}
				}
			}

		}


		if (FinishedDragOnThisFrame) 
		{
			FinishedDragOnThisFrame = false;
			PutDraggedUnitsInCurrentlySelectedUnits ();
		}

	}

	void OnGUI()
	{
		//box width, height, top,left corners 
		if (UserIsDragging) 
		{
			GUI.Box (new Rect (boxLeft, boxTop, boxWidth, boxHeight), "", MouseDragSkin);
		}
	}


	#region Helper function



	//is the user dragging, relative to the mouse drag start point

	public bool UserDraggingByPosition(Vector2 DragStartPoint, Vector2 NewPoint)
	{
		if(
			(NewPoint.x > DragStartPoint.x + clickDragZone || NewPoint.x < DragStartPoint.x - clickDragZone) ||
			(NewPoint.y > DragStartPoint.y + clickDragZone || NewPoint.y < DragStartPoint.y - clickDragZone) 
			)
		return true; else return false;
	}



	public bool DidUserClickedLeftMouse(Vector3 hitPoint)
	{
		
		if (
			(mouseDownPoint.x < hitPoint.x + clickDragZone && mouseDownPoint.x > hitPoint.x - clickDragZone) &&
			(mouseDownPoint.y < hitPoint.y + clickDragZone && mouseDownPoint.y > hitPoint.y - clickDragZone) &&
			(mouseDownPoint.z < hitPoint.z + clickDragZone && mouseDownPoint.z > hitPoint.z - clickDragZone))
			return true;
		else
			return false;
	}

	//deselect gameobject if selected
	public static void DeselectGameObjectsIfSelected()
	{
		if (CurrentlySelectedUnits.Count > 0) 
		{
			for (int i = 0; i < CurrentlySelectedUnits.Count; i++) 
			{
				GameObject ArrayListUnit = CurrentlySelectedUnits [i] as GameObject;
				ArrayListUnit.transform.FindChild("Selected").gameObject.active = false;
				ArrayListUnit.GetComponent<Unit>().Selected = false;
                PersonShowStats.DeletePersonStatsText(ArrayListUnit);
            }

			CurrentlySelectedUnits.Clear();
		}
	}

	//check if a unit is already in the currently selected units arrayList
	public static bool UnitAlreadyInCurrentlySelectedUnits(GameObject Unit)
	{
		if (CurrentlySelectedUnits.Count > 0) 
		{
			for (int i = 0; i < CurrentlySelectedUnits.Count; i++) {
				GameObject ArrayListUnit = CurrentlySelectedUnits [i] as GameObject;
				if (ArrayListUnit == Unit)
					return true;
			}
			return false;
		

		} else 
		{
			return false;
		}

	}

	//remove a unit from the currently selected units arrayList

	public void RemoveUnitFromCurrentlySelectedUnits(GameObject Unit)
	{
		if (CurrentlySelectedUnits.Count > 0) {
			for (int i = 0; i < CurrentlySelectedUnits.Count; i++) {
				GameObject ArrayListUnit = CurrentlySelectedUnits [i] as GameObject;
				if (ArrayListUnit == Unit) 
				{
					CurrentlySelectedUnits.RemoveAt (i);
					ArrayListUnit.transform.FindChild ("Selected").gameObject.active = false;
                    PersonShowStats.DeletePersonStatsText(ArrayListUnit);
                }
			}

			return;

		} else 
		{
			return;
		}
	}
		


	//check if a unit is within the screen space to deal with mouse drag selecting
	public static bool UnitWithinScreenSpace(Vector2 UnitScreenPos)
	{
		if(
			(UnitScreenPos.x < Screen.width && UnitScreenPos.y < Screen.height)&&
			(UnitScreenPos.x > 0f && UnitScreenPos.y > 0f)
		)
		return true; else return false;
	}

	//Remove a unit from screen units UnitsOnScreen arrayList
	public static void RemoveFromOnScreenUnits(GameObject Unit)
	{
		for (int i = 0; i < UnitsOnScreen.Count; i++) 
		{
			GameObject UnitObj = UnitsOnScreen [i] as GameObject;
			if (Unit == UnitObj) 
			{
				UnitsOnScreen.RemoveAt (i);
				UnitObj.GetComponent<Unit>().OnScreen = false;
				return;
			}
		}
		return;
	}

	//Is unit is inside of dragbox?
	public static bool UnitInsideDrag(Vector2 UnitScreenPos)
	{
		if(

			(UnitScreenPos.x > boxStart.x && UnitScreenPos.y < boxStart.y)&&
			(UnitScreenPos.x < boxFinish.x && UnitScreenPos.y > boxFinish.y)
			) return true; else return false;
		
	}

	//check if unit is in UnitsInDrag array list
	public static bool UnitAlreadyInDraggedUnits(GameObject Unit)
	{
		if (UnitsInDrag.Count > 0) 
		{
			for (int i = 0; i < UnitsInDrag.Count; i++) 
			{
				GameObject ArrayListUnit = UnitsInDrag [i] as GameObject;
				if (ArrayListUnit == Unit)
					return true;
			}
			return false;
		} 
		else 
		{
			return false;
		}
	}

	//take all units from UnitsInDrag, into currentlySelectedUnits
	public static void PutDraggedUnitsInCurrentlySelectedUnits()
	{
		
		if (UnitsInDrag.Count > 0)
		{
			for (int i = 0; i < UnitsInDrag.Count; i++) 
			{
				GameObject UnitObj = UnitsInDrag [i] as GameObject;

				//if unit is not already in CurrentlySelectedUnits, add it !
				if(!UnitAlreadyInCurrentlySelectedUnits(UnitObj))
				{
					CurrentlySelectedUnits.Add (UnitObj);
					UnitObj.GetComponent<Unit> ().Selected = true;
				}
			}

			UnitsInDrag.Clear ();
		}
		
	}
		
	#endregion
}
