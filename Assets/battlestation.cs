﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battlestation : VehicleBase
{
    public Turret turret;
    // Start is called before the first frame update
    void Start()
    {

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
