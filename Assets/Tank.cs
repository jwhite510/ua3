﻿using System.Collections;
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
      base.Start();

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
      base.FixedUpdate();
      // set tank position
      // transform.position = vehicle_base.transform.position;
      if(ui_element_name == "SquadListItemO")
      {
        int squadMembers = ui_element.GetComponent<SquadListItem>().squadMembersList.Count;
        Debug.DrawLine(
        vehicle_base.transform.position,
        vehicle_base.transform.position + -10* vehicle_base.transform.right,
        Color.red,
        0.0f
        );
        // position behind
        Vector3 squadFollowPosition = vehicle_base.transform.position + -6*vehicle_base.transform.right;
        float increment = 10.0f;
        float rangeval = (increment/2) * (squadMembers-1);
        int vehicle_number = 0;
        for(float i=-rangeval; i<= rangeval; i+=increment)
        {
          Debug.Log("vehicle_number => "+vehicle_number);
          Debug.DrawLine(
              squadFollowPosition + i*vehicle_base.transform.forward,
              squadFollowPosition  + i*vehicle_base.transform.forward + new Vector3(0,3,0),
              Color.red,
              0.0f
              );
          if(ui_element.GetComponent<SquadListItem>().squadMembersList[vehicle_number] is Tank)
          {
            Tank thistank = (Tank)ui_element.GetComponent<SquadListItem>().squadMembersList[vehicle_number];
            thistank.MoveToWayPoint = true;
            thistank.agent.SetDestination(squadFollowPosition + i*vehicle_base.transform.forward);
          }
          else if(ui_element.GetComponent<SquadListItem>().squadMembersList[vehicle_number] is battlestation)
          {
            battlestation thisbattlestation = (battlestation)ui_element.GetComponent<SquadListItem>().squadMembersList[vehicle_number];
            thisbattlestation.MoveToWayPoint = true;
            thisbattlestation.navagent.SetDestination(squadFollowPosition + i*vehicle_base.transform.forward);
          }



          vehicle_number++;
        }
      }
      if(!player_controlled)
      {
        GameObject closestobject = FindNearestEnemyVehicle();
        if(closestobject)
        {
          thistankturret.autoRotateTurret(closestobject);
        }
        // see if there is a squad leader
        if(MoveToWayPoint)
        {
          TankMoveToWaypoint();
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
    private void TankMoveToWaypoint()
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
}
