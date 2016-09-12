using UnityEngine;
using System.Collections;
using Pathfinding;
using System.IO;

public class UnitPath : MonoBehaviour {

    private Seeker seeker;
    private CharacterController controller;
    public Pathfinding.Path path;
    private Unit unit;
    public float lookSpeed = 4.0f;

    public float speed;
    public float accelerationDefault = 0f;
    public float acceleration = 0f;
    public float maxSpeed = 300.0f;
    public float curSpeed = 0.0f;
    float y = 0;

    public int persentOffMaxSpeed
    {
        get
        {
            return (int)(y * 100);
        }
    }
    float x = 0.0f;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 1;

	//Current waypoint (always starts at index 0)
	private int currentWaypoint = 0;

   // Texture2D texture;

    public void Start()
	{
        // texture = new Texture2D(100, (int) maxSpeed, TextureFormat.ARGB32, true);
        
        seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();
		unit = GetComponent<Unit> ();
	}


	public void LateUpdate()
	{
		if (unit.Selected && unit.isWalkable) 
		{
			if (Input.GetMouseButtonDown (1)) 
			{
				seeker.StartPath (transform.position, Mouse_Control.RightClickPoint, OnPathComplete);                
            }
		}		
	}


	//Pathfinding Logic

	public void OnPathComplete(Pathfinding.Path p)
	{
		if (!p.error) 
		{            
            path = p;
			currentWaypoint = 0;
            x = 0f;
            curSpeed = 0.0f;

           // texture.Apply();
           // byte[] bytes = texture.EncodeToJPG();
           // texture = new Texture2D(100, (int)maxSpeed, TextureFormat.ARGB32, true);
           // File.WriteAllBytes(Application.dataPath + string.Format("/Resources/Grafic{0}.jpg", ItemDatabase.GenerateUniqueNumber()), bytes);
        }
	}



	public void FixedUpdate()
	{

		if (!unit.isWalkable)
			return;
		
		if (path == null)
			return;
		
		if (currentWaypoint >= path.vectorPath.Count)
        {
            curSpeed = 0;
            y = 0;
            return;
        }
			


        //Calculate direction of unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        if (curSpeed <= maxSpeed)
        {            
            y = x * x * (3 - 2 * x);
            if (x < 1)
            {
                x += Time.fixedDeltaTime /3.5f;
            }
            curSpeed = maxSpeed * y;
           // texture.SetPixel((int)(x * 100), (int)(curSpeed), Color.red);
        }

        dir *= curSpeed * Time.fixedDeltaTime;
        Vector3 prevLoc = new Vector3 (dir.x, 0, dir.z);

        controller.SimpleMove (dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(prevLoc), Time.fixedDeltaTime * lookSpeed);

        
        
        
        //Check if close enough to the current waypoin, if we are, proceed to next waypoint
        if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) 
		{
			currentWaypoint++;
			return;
		}
	    
	}
}
