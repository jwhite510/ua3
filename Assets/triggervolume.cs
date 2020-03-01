using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggervolume : MonoBehaviour
{
    public capturabletile capturabletile_o;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
      if(other.tag == "Vehicle")
      {
        VehicleBase vehicle = other.gameObject.GetComponentInParent<VehicleBase>();
        if(vehicle)
        {
          capturabletile_o.VehicleEnteredTile(vehicle);
        }
      }
    }
    void OnTriggerExit(Collider other)
    {
      if(other.tag == "Vehicle")
      {
        VehicleBase vehicle = other.gameObject.GetComponentInParent<VehicleBase>();
        if(vehicle)
        {
          capturabletile_o.VehicleExitTile(vehicle);
        }
      }
    }
}
