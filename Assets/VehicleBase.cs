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
    public healthbar thishealthbar;
    public float current_health;
    public bool isBeingDestroyed = false;
    // Start is called before the first frame update
    public void Start()
    {
      // Debug.Log("VehicleBase Start called");
      current_health = 1.0f;
      thishealthbar.SetHealth(current_health);
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
    public GameObject FindNearestCapturableTile()
    {
      // Debug.Log(name+" find nearest enemy");
      GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("CapturableTileTag");
      float closest_distance = 1000.0f;
      GameObject closestobject = null;

      foreach(GameObject gameo in gameobjects)
      {

        // Debug.DrawLine(
            // gameo.transform.position + new Vector3(0.5f, 0.0f, 0.5f),
            // gameo.transform.position + new Vector3(0.5f,5.0f,0.5f),
            // Color.red,
            // 0.0f
            // );

        float dist = Vector3.Distance(vehicle_base.transform.position, gameo.transform.position);

        if(dist < closest_distance)
        {
          capturabletile thiscapturabletile = gameo.GetComponentInParent<capturabletile>();
          if(thiscapturabletile.owningteam != team)
          {

            Debug.DrawLine(
                gameo.transform.position + new Vector3(-0.5f, 0.0f, -0.5f),
                gameo.transform.position + new Vector3(-0.5f,5.0f,-0.5f),
                Color.yellow,
                0.0f
                );

            closestobject = gameo;
            closest_distance = dist;
          }
        }
      }
      return closestobject;
    }
    public void DestroyThisVehicle()
    {
      Debug.Log("DestroyThisVehicle called");
      Destroy(gameObject);
    }


}
