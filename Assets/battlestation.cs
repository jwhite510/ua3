using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class battlestation : VehicleBase
{
    public Turret turret;
    public GameObject turret_base;
    public Transform navagent_transform;
    public NavMeshAgent navagent;

    public Transform downReference;
    public bool MoveToWayPoint = false;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
      if(team == 1)
      {
        vehicle_base.GetComponent<Renderer>().material.color = Color.green;
        turret_base.GetComponent<Renderer>().material.color = Color.green;
      }
      else if (team == 2)
      {
        vehicle_base.GetComponent<Renderer>().material.color = Color.red;
        turret_base.GetComponent<Renderer>().material.color = Color.red;
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
      if(!player_controlled)
      {
        GameObject closestobject = FindNearestEnemyVehicle();
        if(closestobject)
        {
          turret.autoRotateTurret(closestobject);
        }
      }
      // update position of nav mesh
      Ray ray = new Ray(downReference.position, new Vector3(0,-1,0));
      // RaycastHit hit;
      bool HitSomething = Physics.Raycast(ray, out hit);
      if(HitSomething)
      {
        Debug.DrawLine(
            downReference.position,
            hit.point,
            Color.green,
            0.0f
            );
      }
      navagent_transform.position = hit.point;

      if(MoveToWayPoint)
      {
        if(navagent.path.corners.Length>1)
        {
          Debug.DrawLine(
              navagent.path.corners[1],
              navagent.path.corners[1] + new Vector3(0.1f,10.0f,0.1f),
              Color.green,
              0.0f
              );
          Vector3 move_direction = navagent.path.corners[1] - transform.position;
          if(true)
          {
            move_direction.Normalize();
            move_direction.y = 0;
            transform.position += move_direction;
          }
          // else if(navagent.path.corners.Length>2)
          // {
            // move_direction = navagent.path.corners[2] - transform.position;
            // move_direction.Normalize();
            // move_direction.y = 0;
            // transform.position += move_direction;
          // }



        }



      }


    }
    void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(hit.point, 1);
    }
}
