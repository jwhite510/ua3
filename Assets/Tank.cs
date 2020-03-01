using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : VehicleBase
{
     public Wheel LeftWheel1;
     public Wheel LeftWheel2;
     public Wheel LeftWheel3;
     public Wheel RightWheel1;
     public Wheel RightWheel2;
     public Wheel RightWheel3;
     // public Transform camera_location;
     public NavMeshAgent agent;
     public Transform tank_barrel;

     public GameObject turret_hinge;

     // wheels
     public Renderer LeftWheel1Renderer;
     public Renderer LeftWheel2Renderer;
     public Renderer LeftWheel3Renderer;
     public Renderer RightWheel1Renderer;
     public Renderer RightWheel2Renderer;
     public Renderer RightWheel3Renderer;
     public Turret thistankturret;
     public bool MoveToWayPoint = false;

     public healthbar thehealthbar;

     // public int team = 0;


    // Start is called before the first frame update
    void Start()
    {
      if(team == 1)
      {
        vehicle_base.GetComponent<Renderer>().material.color = Color.green;
        turret_hinge.GetComponent<Renderer>().material.color = Color.green;
        LeftWheel1Renderer.GetComponent<Renderer>().material.color = Color.green;
        LeftWheel2Renderer.GetComponent<Renderer>().material.color = Color.green;
        LeftWheel3Renderer.GetComponent<Renderer>().material.color = Color.green;
        RightWheel1Renderer.GetComponent<Renderer>().material.color = Color.green;
        RightWheel2Renderer.GetComponent<Renderer>().material.color = Color.green;
        RightWheel3Renderer.GetComponent<Renderer>().material.color = Color.green;
      }
      else if (team == 2)
      {
        vehicle_base.GetComponent<Renderer>().material.color = Color.red;
        turret_hinge.GetComponent<Renderer>().material.color = Color.red;
        LeftWheel1Renderer.GetComponent<Renderer>().material.color = Color.red;
        LeftWheel2Renderer.GetComponent<Renderer>().material.color = Color.red;
        LeftWheel3Renderer.GetComponent<Renderer>().material.color = Color.red;
        RightWheel1Renderer.GetComponent<Renderer>().material.color = Color.red;
        RightWheel2Renderer.GetComponent<Renderer>().material.color = Color.red;
        RightWheel3Renderer.GetComponent<Renderer>().material.color = Color.red;
      }
    }
    // Update is called once per frame
    void Update()
    {
      // Debug.Log("tank update called");
    }
    void DriveForward()
    {
      // Debug.Log("DriveForward called");
    }
    void FixedUpdate()
    {
      // set tank position
      // transform.position = vehicle_base.transform.position;
      if(MoveToWayPoint && !player_controlled)
      {
        // Debug.Log("GetType:"+agent.path.corners.GetType());
        float height = 10.0f;
        foreach(Vector3 vec in agent.path.corners)
        {
          // Debug.Log(vec.ToString());
          // Debug.DrawLine(vec, vec + new Vector3(0,height,0), Color.red, 0.0f);
          height *= 0.7f;
        }

        // Debug.DrawLine(agent.transform.position, agent.transform.position+new Vector3(0,1,0), Color.blue, 0.0f);

        // drive wheels to this location
        // agent.path.corners[0]
        // Debug.DrawLine(
          // vehicle_base.transform.position,
          // vehicle_base.transform.position + 10*vehicle_base.transform.right,
          // Color.blue,
          // 0.0f
            // );

        // get direction from current position
        // agent.path.corners[0] // nearest point to move to
        // Debug.Log("Count:"+agent.path.corners.Length);
        if(agent.path.corners.Length>1)
        {
          // move to this point
          // Debug.DrawLine(
              // agent.path.corners[1],
              // agent.path.corners[1] + new Vector3(0.1f,10.0f,0.1f),
              // Color.green,
              // 0.0f
              // );

          // movement direction
          Vector3 move_direction = agent.path.corners[1] - vehicle_base.transform.position;
          move_direction.Normalize();

          // get dot product
          // move_direction
          // vehicle_base.transform.right
          float dotprod = Vector3.Dot(move_direction, vehicle_base.transform.right);
          // Debug.Log("forwarddrive"+dotprod);
          Vector3 crossprod = Vector3.Cross(move_direction, vehicle_base.transform.right);

          // get dot product of crossprod
          float turnvalue = Vector3.Dot(vehicle_base.transform.up, crossprod);

          // Debug.Log("turn"+turnvalue);

          crossprod*=10;

          // draw crossprod
          // Debug.DrawLine(
              // vehicle_base.transform.position,
              // vehicle_base.transform.position + crossprod,
              // Color.green,
              // 0.0f
              // );



          move_direction *= 10;
          // Debug.DrawLine(
              // vehicle_base.transform.position,
              // vehicle_base.transform.position + move_direction,
              // Color.green,
              // 0.0f
              // );


          // drive wheels
          if(Mathf.Abs(turnvalue)>0.5)
          {
            DriveWheels(-10*turnvalue, 10*turnvalue);
          }
          else
          {
            DriveWheels(10*dotprod, 10*dotprod);
          }

        }
      }
      // move barrel to ai at nearest enemy
      if(!player_controlled)
      {

        GameObject closestobject = FindNearestEnemyVehicle();
        if(closestobject)
        {
          thistankturret.autoRotateTurret(closestobject);
        }


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
