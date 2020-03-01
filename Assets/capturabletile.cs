﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capturabletile : MonoBehaviour
{



    private List<VehicleBase> vehicles_on_tile = new List<VehicleBase>();
    public Renderer TileBaseRender;

    private float target_color = 0.5f;
    private float current_color = 0.5f;
    private bool captured = false;
    Color greenc = Color.green;
    Color redc = Color.red;
    public int owningteam = 0;


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
      // Debug.Log("vehicles_on_tile:");
      int team1 = 0;
      int team2 = 0;
      foreach(VehicleBase veh in vehicles_on_tile)
      {
        // Debug.Log("veh.name => "+veh.name+" team:"+veh.team);
        if(veh.team == 1)
        {
          team1++;
        }
        else if(veh.team == 2)
        {
          team2++;
        }
      }
      if(vehicles_on_tile.Count > 0)
      {
        // make weighted average
        target_color = (team1*0 + team2*1);
        target_color /= vehicles_on_tile.Count;
      }
      else
      {
        target_color = 0.5f;
      }

      if(!captured)
      {
        if(current_color < target_color)
        {
          current_color += 0.1f*Time.deltaTime;
        }
        else if(current_color > target_color)
        {
          current_color -= 0.1f*Time.deltaTime;
        }
        if(current_color>1)
        {
          captured = true;
          current_color = 1;
          owningteam = 2; // red team
        }
        else if(current_color < 0)
        {
          captured = true;
          current_color = 0;
          owningteam = 1; // green team
        }
        Color tilecolortarget = Color.Lerp(greenc, redc, current_color);
        Color tilecolor = Color.Lerp(tilecolortarget, Color.white, 0.5f);
        TileBaseRender.GetComponent<Renderer>().material.color = tilecolor;
      }
      else if(captured)
      {
        // Debug.Log("i am captured and owned by "+owningteam);
        // Debug.Log("team1 vehicles on me:"+team1);
        // Debug.Log("team2 vehicles on me:"+team2);
        // if there are no owning vehicles and atleast one enemy vehicle
        if(owningteam == 1 && team1 == 0 && team2>0)
        {
          captured = false;
        }
        else if(owningteam == 2 && team2 == 0 && team1>0)
        {
          captured = false;
        }
      }
    }
    public void VehicleEnteredTile(VehicleBase vehicle)
    {
      // Debug.Log("capturabletile VehicleEnteredTile called");
      // Debug.Log("vehicle.name => "+vehicle.name);
      vehicles_on_tile.Add(vehicle);
    }
    public void VehicleExitTile(VehicleBase vehicle)
    {
      // Debug.Log("capturabletile VehicleExitTile called");
      // Debug.Log("vehicle.name => "+vehicle.name);
      vehicles_on_tile.Remove(vehicle);
    }

}
