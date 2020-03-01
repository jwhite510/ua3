using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capturabletile : MonoBehaviour
{



    private List<VehicleBase> vehicles_on_tile = new List<VehicleBase>();
    public Renderer TileBaseRender;

    Color greenc = Color.green;
    Color redc = Color.red;


    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
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
      // make weighted average
      if(vehicles_on_tile.Count > 0)
      {
        Debug.Log("team1 => "+team1);
        Debug.Log("team2 => "+team2);
        Debug.Log("vehicles_on_tile.Count => "+vehicles_on_tile.Count);
        float wtsum = (team1*0 + team2*1);
        wtsum /= vehicles_on_tile.Count;
        Debug.Log("wtsum => "+wtsum);
        Color tilecolor = Color.Lerp(greenc, redc, wtsum);
        TileBaseRender.GetComponent<Renderer>().material.color = tilecolor;
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
