using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamController : MonoBehaviour
{
	public float maxDistance = 10;
	public float beamWidth = 0.5f;
	public Color initialColor;

    private enum ColorState { Red, Orange, Blue, Green, Yellow, Purple };

    [SerializeField]
    private ColorState startingColor = ColorState.Red;

    [SerializeField]
    private Material[] beamMatArray;

	//This prefab object should have the "LightBeam" tag, a renderer, a trigger collider and kinematic rigidbody
	public GameObject lightBeamPrefab;

	//Current number of rays
	private int rayNumbers = 0;

	//Max rays allowed
	private int maxRays = 5;

	//Raycast offset in order to avoid multiple collisions with same surface
	private float rayOffset = 0.02f;

	private List<Beam> rays = new List<Beam>();
	private GameObject[] beamObjects; 



	public struct Beam
    {


		//This abstract structure holds all positional and rendereing data for each beam segment
		public Beam(Vector3 startPoint, Vector3 endPoint, Color beamColor, Collider collider = null)
        {
			start = startPoint;
			end = endPoint;
			color = beamColor;
			collisionObject = collider;
		}

		public Vector3 start;
		public Vector3 end;
		public Collider collisionObject;
		public Color color;
	}

	// Use this for initialization
	void Start ()
    {
		//Pre-load a series of beam segments to avoid instantiating multiple times at run-time
		beamObjects = new GameObject[maxRays];
		for (int i = 0; i < maxRays; i++)
        {
			GameObject beamClone = Instantiate(lightBeamPrefab);
			beamClone.transform.parent = transform;
			beamClone.name = "LightBeam " + (i+1);
			beamObjects[i] = beamClone;
			beamClone.SetActive(false);
		}
	}

	void Update ()
    {
		//As we wish to recalculate all beams each frame, begin by clearing and de-activating all related variables
		foreach(GameObject beam in beamObjects)
        {
			beam.SetActive(false);
		}
		rayNumbers = 0;
		rays.Clear();
		//Call function to draw the first beam segment
		DrawNewRay();
	}

	void DrawNewRay()
    {
		//Determine where this ray should start and how it should be colored based on whether it is the first ray
		Vector3 rayStart = (rays.Count > 0 ? rays[rayNumbers-1].end : transform.position) + transform.forward*0.1f;
		Color rayColor = (rays.Count > 0 && rays[rayNumbers-1].collisionObject.GetComponent<Renderer>()) ? rays[rayNumbers-1].collisionObject.GetComponent<Renderer>().material.color : initialColor;
		bool drawNewRay = false;
		bool raycastHit = false;

		//Perform raycast and detect whether it has hit anything, and whether that thing was a LightBeam
		Ray ray = new Ray(rayStart, transform.forward);
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray,out hit,maxDistance))
        {
			raycastHit = true;
			if(hit.collider.gameObject.tag == "LightBeam")
            {
                drawNewRay = true;
            }
				
		}

		//Determine where the ray ends based on it's collision
		Vector3 rayEnd = raycastHit ? hit.point : (rayStart + transform.forward * maxDistance);
		//Debug.DrawLine(ray.origin, rayEnd, rayColor);

		//Populate all related information into a Beam structure and save it to a list
		rays.Add(new Beam(ray.origin ,rayEnd, rayColor, hit.collider));

		//Call function to position the beamObject prefab that corresponds to this ray
		PositionRayObject();
		rayNumbers++;

		//If this ray hit a LightBeam and we havent reached the max amount of rays, run this function again for the next ray
		if(drawNewRay && rayNumbers < maxRays)
			DrawNewRay();
	}

	void PositionRayObject(){
		//Activate the beam object correspoinding to the current ray, and determine its correct position, color and scale
		beamObjects[rayNumbers].SetActive(true);
        CheckColorState();
        beamObjects[rayNumbers].GetComponent<Renderer>().material.color = rays[rayNumbers].color;
		beamObjects[rayNumbers].transform.LookAt(rays[rayNumbers].end);
		beamObjects[rayNumbers].transform.position = FindMidPoint(rays[rayNumbers].start,rays[rayNumbers].end);
		beamObjects[rayNumbers].transform.localScale = new Vector3 (beamWidth, beamWidth, (rays[rayNumbers].start - rays[rayNumbers].end).magnitude);
	}

	private Vector3 FindMidPoint(Vector3 vector1, Vector3 vector2){
		//Middlepoint formula - defines the mid point between two 3d positions
		return new Vector3 ((vector1.x + vector2.x)/2,(vector1.y + vector2.y)/2,(vector1.z + vector2.z)/2);
	}

    void CheckColorState()
    {
        switch(startingColor)
        {
            case ColorState.Red:
                beamObjects[rayNumbers].GetComponent<Renderer>().material = beamMatArray[0];
                break;

            case ColorState.Orange:
                beamObjects[rayNumbers].GetComponent<Renderer>().material = beamMatArray[1];
                break;

            case ColorState.Blue:
                beamObjects[rayNumbers].GetComponent<Renderer>().material = beamMatArray[2];
                break;

            case ColorState.Green:
                beamObjects[rayNumbers].GetComponent<Renderer>().material = beamMatArray[3];
                break;

            case ColorState.Yellow:
                beamObjects[rayNumbers].GetComponent<Renderer>().material = beamMatArray[4];
                break;

            case ColorState.Purple:
                beamObjects[rayNumbers].GetComponent<Renderer>().material = beamMatArray[5];
                break;

            default:
                beamObjects[rayNumbers].GetComponent<Renderer>().material = beamMatArray[0];
                break;
        }
    }
}
