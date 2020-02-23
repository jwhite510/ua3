using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
     public NavMeshAgent agent;

     public Transform turret_hinge;

     public bool MoveToWayPoint = false;
     public bool player_controlled = false;



    // Start is called before the first frame update
    void Start()
    {
        
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
      // transform.position = cube_location.position;
      if(MoveToWayPoint && !player_controlled)
      {
        // Debug.Log("GetType:"+agent.path.corners.GetType());
        float height = 10.0f;
        foreach(Vector3 vec in agent.path.corners)
        {
          // Debug.Log(vec.ToString());
          Debug.DrawLine(vec, vec + new Vector3(0,height,0), Color.red, 0.0f);
          height *= 0.7f;
        }

        Debug.DrawLine(agent.transform.position, agent.transform.position+new Vector3(0,1,0), Color.blue, 0.0f);

        // drive wheels to this location
        // agent.path.corners[0]
        Debug.DrawLine(
          cube_location.position,
          cube_location.position + 10*cube_location.right,
          Color.blue,
          0.0f
            );

        // get direction from current position
        // agent.path.corners[0] // nearest point to move to
        // Debug.Log("Count:"+agent.path.corners.Length);
        if(agent.path.corners.Length>1)
        {
          // move to this point
          Debug.DrawLine(
              agent.path.corners[1],
              agent.path.corners[1] + new Vector3(0.1f,10.0f,0.1f),
              Color.green,
              0.0f
              );

          // movement direction
          Vector3 move_direction = agent.path.corners[1] - cube_location.position;
          move_direction.Normalize();

          // get dot product
          // move_direction
          // cube_location.right
          float dotprod = Vector3.Dot(move_direction, cube_location.right);
          Debug.Log("forwarddrive"+dotprod);
          Vector3 crossprod = Vector3.Cross(move_direction, cube_location.right);

          // get dot product of crossprod
          float turnvalue = Vector3.Dot(cube_location.up, crossprod);

          Debug.Log("turn"+turnvalue);

          crossprod*=10;

          // draw crossprod
          Debug.DrawLine(
              cube_location.position,
              cube_location.position + crossprod,
              Color.green,
              0.0f
              );



          move_direction *= 10;
          Debug.DrawLine(
              cube_location.position,
              cube_location.position + move_direction,
              Color.green,
              0.0f
              );


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
    public void RotateTurret(float x, float y)
    {
      Vector3 rot_turret = turret_hinge.rotation.eulerAngles;
      rot_turret.y += x;
      rot_turret.z += y;
      if(rot_turret.z > 350 || rot_turret.z < 80)
      {
        turret_hinge.rotation = Quaternion.Euler(rot_turret);
      }
      else
      {
        rot_turret.z -= y;
        turret_hinge.rotation = Quaternion.Euler(rot_turret);
      }

      // Debug.Log("rot_turret.y"+rot_turret.y);
      // Debug.Log("rot_turret.z"+rot_turret.z);
      // Debug.Log(hello1);
      // Debug.Log(hello2);
      // Debug.Log( hello2||hello1 );
    }
}
