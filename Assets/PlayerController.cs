using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

  public Transform playerCamera;
  public VehicleBase controlled_vehicle;
  public Camera cam;
  public Text SelectedVehicleText;
  public Text ControlledVehicleText;
  public Text ResourcesText;
  public VehicleBase selectedVehicle;
  public GameObject PlayerUI;
  private GameObject SpawnUnitsCursor;
  public battlestation playerBattleStation;
  public healthbar playeruihealthbar;

  bool ControlModeMouse;
  // the delta rotation
  // float drotx = 0;
  // float droty = 0;

  float ClickTime = 0.0f;
  bool handling_mouseclick = false;


  // Start is called before the first frame update
  void Start()
  {
    // FindObjectOfType<GameManager>()
    Cursor.lockState = CursorLockMode.Locked;
    ControlModeMouse = false; // locked
    SelectedVehicleText.text = "";
    controlled_vehicle.player_controlled = true;
    SetPlayerUI(controlled_vehicle);

    // find the spawn units cursor
    Transform[] ts = PlayerUI.transform.GetComponentsInChildren<Transform>(true);
    foreach(Transform t in ts)
    {
      if (t.gameObject.name == "SpawnUnitsCursor")
      {
        SpawnUnitsCursor = t.gameObject;
        break;
      }
    }
    SpawnUnitsCursor.active = false;
    // SpawnUnitsCursor.transform.position = new Vector3(50,50,0);
  }

  // Update is called once per frame
  void Update()
  {
    UpdateUI();
    if(SpawnUnitsCursor.active)
    {
      Vector3 SpawnUnitsCursor_position = Input.mousePosition;
      SpawnUnitsCursor_position.x +=30;
      SpawnUnitsCursor_position.y -=30;
      SpawnUnitsCursor.transform.position = SpawnUnitsCursor_position;
    }

    // playerCamera.position = controlled_vehicle.cameralocation.position;
    playerCamera.position = controlled_vehicle.cameralocation.transform.position;
    playerCamera.rotation = controlled_vehicle.cameralocation.transform.rotation;
    if(ControlModeMouse)
    {
      if(Input.GetMouseButtonDown(0))
      {
        if(!handling_mouseclick)
        {
          ClickTime = Time.time;
          handling_mouseclick = true;
        }
        else if(handling_mouseclick)
        {
          HandleDoubleLeftClick();
          handling_mouseclick = false;
        }
      }
      if(handling_mouseclick)
      {
        if(Time.time > (ClickTime + 0.2))
        {
          HandleSingleLeftClick();
          handling_mouseclick = false;
        }
      }
      if(Input.GetMouseButtonDown(1))
      {
        HandleSingleRightClick();
      }
    }
    else if(!ControlModeMouse)
    {
      if(Input.GetMouseButtonDown(0))
      {
        if(controlled_vehicle is Tank)
        {
          Tank controlled_tank = (Tank)controlled_vehicle;
          controlled_tank.thistankturret.FireCannon();
        }
        else if(controlled_vehicle is battlestation)
        {
          battlestation controlled_battlestation = (battlestation)controlled_vehicle;
          controlled_battlestation.turret.FireCannon();
        }
        else if(controlled_vehicle is Mech)
        {
          Mech controlledMech = (Mech)controlled_vehicle;
          controlledMech.FireCannons();

        }
      }
    }
    // if cursor toggle key is pressed
    if(Input.GetKeyDown("e"))
    {
      // Debug.Log("unlock cursor");
      if(Cursor.lockState == CursorLockMode.Locked)
      {
        Cursor.lockState = CursorLockMode.None;
        ControlModeMouse = true; // unlocked
      }
      else if (Cursor.lockState == CursorLockMode.None)
      {
        Cursor.lockState = CursorLockMode.Locked;
        ControlModeMouse = false; // locked
        SpawnUnitsCursor.active = false;
      }
    }
  }

  void FixedUpdate()
  {
    if(controlled_vehicle is Tank)
    {
      Tank controlled_tank = (Tank)controlled_vehicle;
      if(Input.GetKey("w"))
      {
        controlled_tank.DriveWheels(10,10);
      }
      else if(Input.GetKey("s"))
      {
        controlled_tank.DriveWheels(-10,-10);
      }
      else if(Input.GetKey("d"))
      {
        controlled_tank.DriveWheels(10,-10);
      }
      else if(Input.GetKey("a"))
      {
        controlled_tank.DriveWheels(-10,10);
      }
      // if in no mouse mode
      if(!ControlModeMouse)
      {
        // Debug.Log("get mouse axis");
        // Debug.Log(Input.mousePosition);
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        // drotx+=mouseX;
        // droty+=mouseY;
        controlled_tank.thistankturret.RotateTurret(mouseX, mouseY);
      }
    }
    else if(controlled_vehicle is battlestation)
    {
      battlestation controlled_battlestation = (battlestation)controlled_vehicle;
      if(!ControlModeMouse)
      {
        // Debug.Log("get mouse axis");
        // Debug.Log(Input.mousePosition);
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        // drotx+=mouseX;
        // droty+=mouseY;
        controlled_battlestation.turret.RotateTurret(mouseX, mouseY);
      }
    }
    else if(controlled_vehicle is Mech)
    {
      Mech controlled_mech = (Mech)controlled_vehicle;
      if(Input.GetKey("w"))
      {
        controlled_mech.DriveLegs(1.0f,1.0f);
      }
      else if(Input.GetKey("s"))
      {
        controlled_mech.DriveLegs(-1.0f,-1.0f);
      }
      else if(Input.GetKey("d"))
      {
        controlled_mech.DriveLegs(-1.0f,1.0f);
      }
      else if(Input.GetKey("a"))
      {
        controlled_mech.DriveLegs(1.0f,-1.0f);
      }
      else
      {
        controlled_mech.StopLegs();
      }

      if(!ControlModeMouse)
      {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        controlled_mech.RotateMechTurret(mouseX, mouseY);
      }
    }

  }

  void HandleSingleLeftClick()
  {
    if(EventSystem.current.IsPointerOverGameObject())
    {
      return;
    }
    if(!SpawnUnitsCursor.active)
    {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      bool HitSomething = Physics.Raycast(ray, out hit);
      if(HitSomething)
      {
        // Debug.Log(hit.point.ToString());
        // Debug.DrawLine(hit.point, hit.point+new Vector3(0,1,0), Color.red, 1.0f);
        // Debug.Log("draw debug line");
        // Debug.Log(hit.transform.gameObject.name);
        VehicleBase vehicleclicked = hit.transform.gameObject.GetComponentInParent<VehicleBase>();

        if(vehicleclicked)
        {
          // Debug.Log(vehicleclicked.name);
          SelectedVehicleText.text = vehicleclicked.name;
          selectedVehicle = vehicleclicked;
        }
      }
    }
    else if(SpawnUnitsCursor.active)
    {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      bool HitSomething = Physics.Raycast(ray, out hit);
      if(HitSomething)
      {
        Debug.Log("hit.point.ToString()"+hit.point.ToString());
        // Debug.DrawLine(hit.point, hit.point+new Vector3(0,1,0), Color.red, 1.0f);
        if(controlled_vehicle is battlestation)
        {
          battlestation controlled_battlestation = (battlestation)controlled_vehicle;
          controlled_battlestation.spawnTank(hit.point);
        }

      }

    }
  }
  void HandleDoubleLeftClick()
  {
    if(EventSystem.current.IsPointerOverGameObject())
    {
      return;
    }
    if(!SpawnUnitsCursor.active)
    {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      bool HitSomething = Physics.Raycast(ray, out hit);
      if(HitSomething)
      {
        VehicleBase vehicle_clicked = hit.transform.gameObject.GetComponentInParent<VehicleBase>();
        if(vehicle_clicked)
        {
          ControlVehicle(vehicle_clicked);
        }
      }
    }
  }
  void HandleSingleRightClick()
  {
    if(EventSystem.current.IsPointerOverGameObject())
    {
      return;
    }
    if(!SpawnUnitsCursor.active)
    {
      // Debug.Log("Right Click");
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      bool HitSomething = Physics.Raycast(ray, out hit);
      if(HitSomething)
      {
        // Debug.Log(hit.point.ToString());
        // Debug.DrawLine(hit.point, hit.point+new Vector3(0,1,0), Color.red, 1.0f);
        if(selectedVehicle)
        {
          if(selectedVehicle is Tank)
          {
            Tank SelectedTank = (Tank)selectedVehicle;
            if(SelectedTank)
            {
              SelectedTank.MoveToWayPoint = true;
              SelectedTank.agent.SetDestination(hit.point);
            }
          }
          else if(selectedVehicle is battlestation)
          {
            battlestation Selectedbattlestation = (battlestation)selectedVehicle;
            if(Selectedbattlestation)
            {
              Selectedbattlestation.MoveToWayPoint = true;
              Selectedbattlestation.navagent.SetDestination(hit.point);
            }
          }
          else if(selectedVehicle is Mech)
          {
            Mech selectedMech = (Mech)selectedVehicle;
            if(selectedMech)
            {
              selectedMech.MoveToWayPoint = true;
              selectedMech.agent.SetDestination(hit.point);
            }
          }
        }

      }
    }
    else if(SpawnUnitsCursor.active)
    {
      SpawnUnitsCursor.active = false;
    }
  }

  public void SpawnUnitsButtonClicked()
  {
    Debug.Log("SpawnUnitsButtonClicked called");
    if(SpawnUnitsCursor.active)
    {
      SpawnUnitsCursor.active = false;
    }
    else
    {
      SpawnUnitsCursor.active = true;
    }


    // Debug.Log("SpawnUnitsButtonClicked called");
    // Transform pui = PlayerUI.transform.Find("SpawnUnitsButton");
    // Debug.Log("pui.name"+pui.name);
  }

  void SetPlayerUI(VehicleBase vehiclebase)
  {
    if(vehiclebase is battlestation)
    {
      ActivateButton(PlayerUI, "spawnUnitsButton", true);
    }
    else
    {
      ActivateButton(PlayerUI, "spawnUnitsButton", false);
    }
  }
  void ActivateButton(GameObject UIObject, string buttonName, bool status)
  {
    Transform[] ts = UIObject.transform.GetComponentsInChildren<Transform>(true);
    GameObject spawnUnitsButton = null;
    foreach(Transform t in ts)
    {
      if (t.gameObject.name == "SpawnUnitsButton")
      {
        spawnUnitsButton = t.gameObject;
      }
    }
    if(spawnUnitsButton)
    {
      Debug.Log("spawnUnitsButton.name => "+spawnUnitsButton.name);
      spawnUnitsButton.active = status;
    }
  }
  public void ControlVehicle(VehicleBase controlthisvehicle)
  {
    controlled_vehicle.player_controlled = false;
    controlled_vehicle = controlthisvehicle;
    controlled_vehicle.player_controlled = true;
    ControlledVehicleText.text =controlled_vehicle.name;
    ControlModeMouse = false; // locked
    Cursor.lockState = CursorLockMode.Locked;
    SetPlayerUI(controlled_vehicle);
  }
  private void UpdateUI()
  {
    ResourcesText.text = "energy: "+playerBattleStation.energy;
    playeruihealthbar.SetHealth(controlled_vehicle.current_health);

  }
}

