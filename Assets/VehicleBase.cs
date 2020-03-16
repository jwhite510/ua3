using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class VehicleBase : MonoBehaviour
{
    public Transform cameralocation;
    public GameObject vehicle_base;
    public int team = 0;
    public bool player_controlled = false;
    public healthbar thishealthbar;
    public float current_health;
    public bool isBeingDestroyed = false;
    public GameObject ui_element;
    public string ui_element_name = "";
    private bool destroy_vehicle_has_been_called = false;

    // Start is called before the first frame update
    public void Start()
    {
      // Debug.Log("VehicleBase Start called");
      current_health = 1.0f;
      thishealthbar.SetHealth(current_health);
      if(team == 1)
      {
         PlayerController playerController = FindObjectOfType<PlayerController>();
         playerController.playerSingleVehicles.Add(this);
         playerController.AddSingleVehicleToUI(this);
         // Debug.Log("playerController.playerSingleVehicles.Count => "+playerController.playerSingleVehicles.Count);
      }
      // Debug.Log("add vehilce to hud if on player team");
    }
    // Update is called once per frame
    void Update()
    {
      // Debug.Log("tank update called");
    }
    public void FixedUpdate()
    {
      // Debug.Log("VehicleBase FixedUpdate called");
      if(isBeingDestroyed && player_controlled)
      {
        PlayerController playercontroller = FindObjectOfType<PlayerController>();
        battlestation[] battlestations = FindObjectsOfType<battlestation>();
        foreach(battlestation bat in battlestations)
        {
          if(bat.team == 1)
          {
            playercontroller.ControlVehicle(bat);
            break;
          }
        }
      }
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
      // this function must run only once
      if(!destroy_vehicle_has_been_called)
      {
        // squad leader destroyed
        if(ui_element_name == "SquadListItemO")
        {
          // if there is only one other vehicle, set it to a single vehicle
          if(ui_element.GetComponent<SquadListItem>().squadMembersList.Count > 1)
          {

            // destroy ui element of squad member
            VehicleBase squad_member = ui_element.GetComponent<SquadListItem>().squadMembersList[0];
            Destroy(squad_member.ui_element);
            squad_member.ui_element = ui_element;
            squad_member.ui_element_name = "SquadListItemO";

            // set squad member as squad leader
            ui_element.GetComponent<SquadListItem>().SetSquadLeader(squad_member);
            ui_element.GetComponent<SquadListItem>().squadMembersList.Remove(squad_member);

          }
          else // there is only one other squad member
          {
            VehicleBase squad_member = ui_element.GetComponent<SquadListItem>().squadMembersList[0];
            Destroy(ui_element);
            PlayerController playercontroller = FindObjectOfType<PlayerController>();
            playercontroller.AddSingleVehicleToUI(squad_member);
          }
          // if there is more than one vehicle, set one as the new leader
        }
        // squad member destroyed
        else if(ui_element_name == "SquadMember")
        {
          // if there is more than one squad member
          if(ui_element.GetComponent<SquadMemberS>().squadListItem.squadMembersList.Count > 1)
          {
            ui_element.GetComponent<SquadMemberS>().squadListItem.squadMembersList.Remove(this);
            Destroy(ui_element);
          }
          else // set the squad leader to a single vehicle
          {
            Destroy(ui_element.GetComponent<SquadMemberS>().squadListItem.squadLeaderVehicle.ui_element);
            PlayerController playercontroller = FindObjectOfType<PlayerController>();
            playercontroller.AddSingleVehicleToUI(ui_element.GetComponent<SquadMemberS>().squadListItem.squadLeaderVehicle);
          }
        }
        // single vehicle destroyed
        else if(ui_element_name == "VehicleReferenceButton")
        {
          Debug.Log("VehicleReferenceButton");
          Destroy(ui_element);
        }

        // if there is only one vehicle left, demote to a single vehicle button
        Destroy(gameObject);
        destroy_vehicle_has_been_called = true;
      }

    }
    void OnDrawGizmos()
    {
      string debugtext = "";
      debugtext += gameObject.name;
      debugtext += "  ui_element_name: ";
      debugtext += ui_element_name;

      Handles.Label(transform.position, debugtext);
    }


}
