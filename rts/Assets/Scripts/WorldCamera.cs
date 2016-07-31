using UnityEngine;
using System.Collections;

public class WorldCamera : MonoBehaviour {

	#region structs

	//box limits struct
	public struct BoxLimit 
	{
		public float LeftLimit;
		public float RightLimit;
		public float TopLimit;
		public float BottomLimit;

	}
	#endregion

	#region class variables

	public static BoxLimit cameraLimits = new BoxLimit();
	public static BoxLimit mouseScrollLimits = new BoxLimit();
	public static WorldCamera Instance;

	public GameObject MainCamera;
	private GameObject ScrollAngle;

	private float cameraMoveSpeed = 10f;
	private float shiftBonus = 15f;
	private float mouseBoundary = 25f;

	public Terrain WorldTerrain;
	public float WorldTerrainPadding = 0f;

	[HideInInspector]public float cameraHeight; //Only for scrolling or zooming
	[HideInInspector]public float cameraY; //Changes relatively to the terrain high
	private float maxCameraHeight = 5f;
	public LayerMask TerrainOnly;
	private float minDistanceToObject = 2f;


	#endregion

	void Awake()
	{
		Instance = this;
	}

	void Start ()
	{
		cameraLimits.LeftLimit = WorldTerrain.transform.position.x + WorldTerrainPadding;
		cameraLimits.RightLimit = WorldTerrain.terrainData.size.x - WorldTerrainPadding;
		cameraLimits.TopLimit = WorldTerrain.terrainData.size.z - WorldTerrainPadding;
		cameraLimits.BottomLimit = WorldTerrain.transform.position.z + WorldTerrainPadding;

		mouseScrollLimits.LeftLimit = mouseBoundary;
		mouseScrollLimits.RightLimit = mouseBoundary;
		mouseScrollLimits.TopLimit = mouseBoundary;
		mouseScrollLimits.BottomLimit = mouseBoundary;

		cameraHeight = transform.position.y;

		ScrollAngle = new GameObject ();
	}

	void LateUpdate ()
	{
		ApplyScroll ();
		if (CheckIfUserCameraInput ()) 
		{
			Vector3 cameraDesiredMove = GetDesiredTranslation ();

			if (!isDesiredPositionOverBoundaries (cameraDesiredMove)) 
			{
				Vector3 desiredPosition = transform.position + cameraDesiredMove;

				UpdateCameraY (desiredPosition);

				this.transform.Translate (cameraDesiredMove);
			}
		}

		ApplyCameraY ();

	}


	//calculate the minimum camera height
	public float MinCameraHeight()
	{
		RaycastHit hit;
		float MinCameraHeight = WorldTerrain.transform.position.y;

		if (Physics.Raycast (transform.position, Vector3.down, out hit, Mathf.Infinity, TerrainOnly)) 
		{
			MinCameraHeight = hit.point.y + minDistanceToObject;

		}
		return MinCameraHeight;

	} 




	public void ApplyScroll()
	{
		float deadZone = 0.01f;
		float easeFactor = 50f;

		float ScrollWheelValue = Input.GetAxis ("Mouse ScrollWheel") * easeFactor * Time.deltaTime;

		//check deadzone
		if ((ScrollWheelValue > -deadZone && ScrollWheelValue < deadZone) || ScrollWheelValue == 0f)
			return;

		float EulerAngleX = MainCamera.transform.localEulerAngles.x;

		//configure the scrollangle
		ScrollAngle.transform.position = transform.position;
		ScrollAngle.transform.eulerAngles = new Vector3 (EulerAngleX, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
		ScrollAngle.transform.Translate (Vector3.forward * ScrollWheelValue);

		Vector3 desiredScrollPosition = ScrollAngle.transform.position;

		//check if in boundaries
		if (desiredScrollPosition.x < cameraLimits.LeftLimit || desiredScrollPosition.x > cameraLimits.RightLimit)return;
		if (desiredScrollPosition.z > cameraLimits.TopLimit || desiredScrollPosition.z < cameraLimits.BottomLimit)return;
		if (desiredScrollPosition.y > maxCameraHeight || desiredScrollPosition.y < MinCameraHeight())			  return;

		//Update the cameraHeight and the cameraY

		float heightDifference = desiredScrollPosition.y - this.transform.position.y;
		cameraHeight += heightDifference;
		UpdateCameraY (desiredScrollPosition);

		//update the camera position

		this.transform.position = desiredScrollPosition;

		return;


	}


	//Update the new height for the camera based on the terrain height
	public void UpdateCameraY(Vector3 desiredPosition)
	{
		RaycastHit hit;
		float deadZone = 0.1f;

		if(Physics.Raycast(desiredPosition, Vector3.down, out hit, Mathf.Infinity))
			{
				float newHeight = cameraHeight + hit.point.y;
				float heightDifferense = newHeight - cameraY;
				
			if (heightDifferense > -deadZone && heightDifferense < deadZone)return;
			if (newHeight > maxCameraHeight || newHeight < MinCameraHeight())return;

			cameraY = newHeight;

			}

		return;

	}

	//Apply camera Y to a smooth damp, and update camera Y position
	public void ApplyCameraY()
	{
		if (cameraY == transform.position.y || cameraY == 0)
			return;

		//smooth damp
		float smoothTime = 0.2f;
		float yVelocity = 0.0f;

		float newPositionY = Mathf.SmoothDamp (transform.position.y, cameraY, ref yVelocity, smoothTime);

		if (newPositionY < maxCameraHeight) 
		{
			transform.position = new Vector3 (transform.position.x, newPositionY, transform.position.z);
		}

		return;

	}


	public bool CheckIfUserCameraInput()
	{
		bool keyboardMove;
		bool mouseMove;
		bool canMove;

		if (WorldCamera.AreCameraKeyboardButtonsPressed ()) {
			keyboardMove = true;
		} 
		else 
		{
			keyboardMove = false;
		}

		if (WorldCamera.IsMousePositionWithinBoundaries ()) {
			mouseMove = true;
		} 
		else 
		{
			mouseMove = false;
		}

		if (keyboardMove || mouseMove)
			canMove = true;
		else
			canMove = false;

		return canMove;
	}

	//Works out the cameras desired location depending on the players input
	public Vector3 GetDesiredTranslation()
	{
		float moveSpeed = 0f;
		Vector3 desiredTranslation = new Vector3 ();

		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			moveSpeed = (cameraMoveSpeed + shiftBonus) * Time.deltaTime;
		} 
		else 
		{
			moveSpeed = cameraMoveSpeed * Time.deltaTime;
		}

		//move via keyboard
		if (Input.GetKey (KeyCode.UpArrow) || Input.mousePosition.y > (Screen.height - mouseScrollLimits.TopLimit))
			desiredTranslation += Vector3.forward * moveSpeed;

		if (Input.GetKey (KeyCode.DownArrow) ||Input.mousePosition.y < mouseScrollLimits.BottomLimit )
			desiredTranslation += Vector3.back * moveSpeed;

		if (Input.GetKey (KeyCode.LeftArrow) ||Input.mousePosition.x < mouseScrollLimits.LeftLimit)
			desiredTranslation += Vector3.left * moveSpeed;

		if (Input.GetKey (KeyCode.RightArrow) || Input.mousePosition.x > (Screen.width - mouseScrollLimits.RightLimit))
			desiredTranslation += Vector3.right * moveSpeed;

	
		return desiredTranslation;
	}


	//checks if the desired position crosses boundaries
	public bool isDesiredPositionOverBoundaries(Vector3 desiredPosition)
	{
		bool overBoundaries = false;
		//check boundaries
		if ((this.transform.position.x + desiredPosition.x) < cameraLimits.LeftLimit)
			overBoundaries = true;

		if ((this.transform.position.x + desiredPosition.x) > cameraLimits.RightLimit)
			overBoundaries = true;

		if ((this.transform.position.z + desiredPosition.z) > cameraLimits.TopLimit)
			overBoundaries = true;

		if ((this.transform.position.z + desiredPosition.z) < cameraLimits.BottomLimit)
			overBoundaries = true;

		return overBoundaries;
	}

	#region Helper functions

	public static bool AreCameraKeyboardButtonsPressed()
	{
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow))
			return true;
		else
			return false;
	}

	public static bool IsMousePositionWithinBoundaries()
	{
		if (
			(Input.mousePosition.x < mouseScrollLimits.LeftLimit && Input.mousePosition.x > -5) ||
			(Input.mousePosition.x > (Screen.width - mouseScrollLimits.RightLimit) && Input.mousePosition.x < (Screen.width + 5)) ||
			(Input.mousePosition.y < mouseScrollLimits.BottomLimit && Input.mousePosition.y > -5) ||
			(Input.mousePosition.y > (Screen.height - mouseScrollLimits.TopLimit) && Input.mousePosition.y < (Screen.height + 5)))
			return true;
		else
			return false;

	}

	#endregion

}
