using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
     public Wheel LeftWheel1;
     public Wheel LeftWheel2;
     public Wheel LeftWheel3;
     public Wheel RightWheel1;
     public Wheel RightWheel2;
     public Wheel RightWheel3;
     public Transform camera_location;
     public Transform cube_location;

     public Vector3 ThisNavWaypoint;
     public bool MoveToWayPoint = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log("tank update called");
      // Debug.Log(name+" "+ThisNavWaypoint.ToString());
    }

    void DriveForward()
    {

      // Debug.Log("DriveForward called");

    }

    void FixedUpdate()
    {
      // set tank position
      // transform.position = cube_location.position;
      if(MoveToWayPoint)
      {
        Debug.Log(name+" moving to "+ThisNavWaypoint.ToString());
        Debug.DrawLine(ThisNavWaypoint, ThisNavWaypoint+new Vector3(0,1,0), Color.red, 1.0f);
      }



    }
    public void DriveWheels(float LeftSide, float RightSide)
    {
        LeftWheel1.Drive(LeftSide);
        LeftWheel2.Drive(LeftSide);
        LeftWheel3.Drive(LeftSide);
        RightWheel1.Drive(RightSide);
        RightWheel2.Drive(RightSide);
        RightWheel3.Drive(RightSide);

    }


}
