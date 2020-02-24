using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battlestation : VehicleBase
{
    public Turret turret;
    public GameObject turret_base;
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
    }
}
