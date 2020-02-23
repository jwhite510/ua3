﻿using System.Collections;
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
     public GameObject vehicle_base;
     public NavMeshAgent agent;
     public Transform tank_barrel;

     public Transform turret_hinge;
     public GameObject projectile;

     public bool MoveToWayPoint = false;
     public bool player_controlled = false;

     public Transform projectile_spawn_point;

     private float last_fire_time = 0;

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
      // transform.position = vehicle_base.transform.position;
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

        // Debug.DrawLine(agent.transform.position, agent.transform.position+new Vector3(0,1,0), Color.blue, 0.0f);

        // drive wheels to this location
        // agent.path.corners[0]
        Debug.DrawLine(
          vehicle_base.transform.position,
          vehicle_base.transform.position + 10*vehicle_base.transform.right,
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
        // Debug.Log(name+" find nearest enemy");
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Vehicle");
        float closest_distance = 1000.0f;
        GameObject closestobject = null;

        foreach(GameObject gameo in gameobjects)
        {
          float dist = Vector3.Distance(vehicle_base.transform.position, gameo.transform.position);
          // Debug.Log(name+"  dist"+dist);
          // Debug.Log(name+"  closest_distance"+closest_distance);
          // Debug.Log(dist < closest_distance);
          if(dist < closest_distance && gameo.transform!=vehicle_base.transform)
          {
            closestobject = gameo;
            closest_distance = dist;
          }
        }

        if(closestobject)
        {
          // Debug.DrawLine(
              // closestobject.transform.position,
              // vehicle_base.transform.position,
              // Color.red,
              // 0.0f
              // );


          // move torret roward target
          // barrel aim direction
          // Debug.DrawLine(
              // tank_barrel.position,
              // tank_barrel.position+(-3)*tank_barrel.up,
              // Color.red,
              // 0.0f
              // );

          // barrel up and right
          // Debug.DrawLine(
              // tank_barrel.position,
              // tank_barrel.position+(-1)*tank_barrel.right,
              // Color.blue,
              // 0.0f
              // );
          // barrel up and right
          // Debug.DrawLine(
              // tank_barrel.position,
              // tank_barrel.position+(-1)*tank_barrel.forward,
              // Color.blue,
              // 0.0f
              // );

          Vector3 current_barrel_direction = -1*tank_barrel.up;
          // direction to target
          Vector3 intend_aim_direction = closestobject.transform.position - tank_barrel.position;
          intend_aim_direction.Normalize();

          // Debug.DrawLine(
              // tank_barrel.position,
              // tank_barrel.position+(3)*intend_aim_direction,
              // Color.blue,
              // 0.0f
              // );

          float right_dot = Vector3.Dot(intend_aim_direction, tank_barrel.right);
          float forward_dot = Vector3.Dot(intend_aim_direction, tank_barrel.forward);
          float on_target = -Vector3.Dot(intend_aim_direction, tank_barrel.up);


          // Debug.Log("on_target: "+on_target);
          if(on_target > 0.9)
          {
            FireCannon();
          }


          // float VerticalAim = (Vector3.Scale(tank_barrel.forward, intend_aim_direction)).magnitude;
          // Debug.Log("HorizontalAim"+HorizontalAim);
          // Debug.Log("VerticalAim"+VerticalAim);

          // Vector3 intend_aim_r = Quaternion.LookRotation(intend_aim_direction).eulerAngles;
          // Vector3 current_barrel_r = Quaternion.LookRotation(current_barrel_direction).eulerAngles;

          // RotateTurret()
          // Debug.Log("right_dot: "+right_dot);

          // Debug.Log("forward_dot: "+forward_dot); // right or left
          // Debug.Log("on_target: "+on_target); // is the target in front of or behind the barrel

          RotateTurret(-300*forward_dot*Time.deltaTime, 300*right_dot*Time.deltaTime);

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
      Vector3 rot_turret = turret_hinge.localRotation.eulerAngles;
      rot_turret.y += x;
      rot_turret.z += y;

      if(rot_turret.z > 350 || rot_turret.z < 80)
      {
        turret_hinge.localRotation = Quaternion.Euler(rot_turret);
      }
      else
      {
        rot_turret.z -= y;
        turret_hinge.localRotation = Quaternion.Euler(rot_turret);
      }

      // Debug.Log("rot_turret.y"+rot_turret.y);
      // Debug.Log("rot_turret.z"+rot_turret.z);
      // Debug.Log(hello1);
      // Debug.Log(hello2);
      // Debug.Log( hello2||hello1 );
    }
    public void FireCannon()
    {
      if((Time.time - last_fire_time) > 1)
      {
        GameObject proj = Instantiate(projectile, projectile_spawn_point.position, projectile_spawn_point.rotation);
        proj.GetComponent<Rigidbody>().velocity = 20*projectile_spawn_point.right + vehicle_base.GetComponent<Rigidbody>().velocity;
        last_fire_time = Time.time;
      }
    }
}
