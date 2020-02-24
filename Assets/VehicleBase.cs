using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleBase : MonoBehaviour
{
     public Transform cameralocation;
     public GameObject vehicle_base;
     public int team = 0;
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
    void FixedUpdate()
    {
    }

    public GameObject FindNearestEnemyVehicle()
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
          VehicleBase thisVehicle = gameo.GetComponentInParent<VehicleBase>();
          if(thisVehicle && thisVehicle.team != team)
          {
            closestobject = gameo;
            closest_distance = dist;
          }
        }
      }
      return closestobject;
    }


}
