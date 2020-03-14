using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class battlestation : VehicleBase
{
    public Turret turret;
    public GameObject turret_base;
    public Transform navagent_transform;
    public NavMeshAgent navagent;

    public Transform downReference;
    public bool MoveToWayPoint = false;
    public bool AI_controlled;
    public float energy = 0;
    public int ownedtiles = 0;
    public GameObject SpawnVehicle;
    private RaycastHit point_below_station;
    private float last_resources_collected_time = 0;
    // Start is called before the first frame update
    void Start()
    {
      base.Start();
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
      CollectResources();

      if(AI_controlled)
      {
        // AI_Control();
      }
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
      // RaycastHit point_below_station;
      bool HitSomething = Physics.Raycast(ray, out point_below_station);
      if(HitSomething)
      {
        // Debug.DrawLine(
            // downReference.position,
            // point_below_station.point,
            // Color.green,
            // 0.0f
            // );
      }
      navagent_transform.position = point_below_station.point;

      if(MoveToWayPoint)
      {
        if(navagent.path.corners.Length>1)
        {
          // Debug.DrawLine(
              // navagent.path.corners[1],
              // navagent.path.corners[1] + new Vector3(0.1f,10.0f,0.1f),
              // Color.green,
              // 0.0f
              // );
          Vector3 move_direction = navagent.path.corners[1] - navagent_transform.position;


          float dist = Vector3.Distance(navagent.path.corners[1], navagent_transform.position);

          Debug.Log("dist => "+dist);
          move_direction.y = 0;
          move_direction.Normalize();
          if(dist > 1)
          {
            transform.position += (move_direction * 0.1f);
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
      Gizmos.DrawSphere(point_below_station.point, 1);
      Handles.Label(transform.position, "energy:"+energy);
    }
    void AI_Control()
    {
      // Debug.Log("AI_Control running "+team);
      // get all vehicles
      VehicleBase[] all_vehicles = FindObjectsOfType<VehicleBase>();
      List<VehicleBase> teamvehicles = new List<VehicleBase>();
      foreach(VehicleBase veh in all_vehicles)
      {
        // Debug.Log("veh.name => "+veh.name);
        if(veh.team == team)
        {
          if(veh is battlestation)
          {
            // Debug.Log(veh.name+" is battlestation");
          }
          else
          {
            teamvehicles.Add(veh);
          }
        }
      }
      foreach(VehicleBase veh in teamvehicles)
      {
        // Debug.Log("veh.name => "+veh.name);
        // get vehicle location
        GameObject closesttile = veh.FindNearestCapturableTile();
        if(closesttile)
        {
          // Debug.DrawLine(
              // closesttile.transform.position,
              // closesttile.transform.position + new Vector3(0.1f,5.0f,0.1f),
              // Color.red,
              // 0.0f
              // );

          // Debug.DrawLine(
              // veh.transform.position,
              // veh.transform.position + new Vector3(0.1f,5.0f,0.1f),
              // Color.red,
              // 0.0f
              // );
          if(veh is Tank)
          {
            Tank thistank = (Tank)veh;
            thistank.MoveToWayPoint = true;
            thistank.agent.SetDestination(closesttile.transform.position);
          }
          else if(veh is Mech)
          {
            Mech thismech = (Mech)veh;
            thismech.MoveToWayPoint = true;
            thismech.agent.SetDestination(closesttile.transform.position);
          }
        }


      }
      // spawn vehicles
      if(energy > 10)
      {
        float r1 = Random.Range(-3,3);
        float r2 = Random.Range(-3,3);
        spawnTank(point_below_station.point + new Vector3(r1, 0, r2));
      }



    }
    public void spawnTank(Vector3 spawnLocation)
    {
      if(energy >= 10)
      {
        energy -= 10;
        Quaternion e1 = new Quaternion(0,0,0,0);
        e1.eulerAngles = new Vector3(0,0,0);
        GameObject spawnVehicle = Instantiate(SpawnVehicle, spawnLocation + new Vector3(0,3,0), e1);
        Tank tanks = spawnVehicle.GetComponent<Tank>();
        // set the tank to this host station team
        tanks.team = team;
        if(team == 1)
        {
          // update player ui
          PlayerController playerController = FindObjectOfType<PlayerController>();

        }
      }

    }
    private void CollectResources()
    {
      if((Time.time - last_resources_collected_time) > 3.0)
      {
        energy += 1;
        energy += 1*ownedtiles;
        last_resources_collected_time = Time.time;
      }
    }
}
