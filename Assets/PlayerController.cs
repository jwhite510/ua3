using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

  public Transform playerCamera;
  public VehicleBase controlled_vehicle;
  public Camera cam;
  public Text SelectedVehicleText;
  public Tank SelectedTank;

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
    SelectedVehicleText.text = controlled_vehicle.name;
    controlled_vehicle.player_controlled = true;

  }

  // Update is called once per frame
  void Update()
  {

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
      }
    }
    // if cursor toggle key is pressed
    if(Input.GetKeyDown("space"))
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

  }

  void HandleSingleLeftClick()
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
      Tank tankclicked = hit.transform.gameObject.GetComponentInParent<Tank>();

      if(tankclicked)
      {
        // Debug.Log(tankclicked.name);
        SelectedVehicleText.text = tankclicked.name;
        SelectedTank = tankclicked;
      }
    }
  }
  void HandleDoubleLeftClick()
  {
    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    bool HitSomething = Physics.Raycast(ray, out hit);
    if(HitSomething)
    {
      VehicleBase vehicle_clicked = hit.transform.gameObject.GetComponentInParent<VehicleBase>();
      if(vehicle_clicked)
      {
        controlled_vehicle.player_controlled = false;
        controlled_vehicle = vehicle_clicked;
        controlled_vehicle.player_controlled = true;
        ControlModeMouse = false; // locked
        Cursor.lockState = CursorLockMode.Locked;
      }
    }
  }
  void HandleSingleRightClick()
  {
    // Debug.Log("Right Click");
    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    bool HitSomething = Physics.Raycast(ray, out hit);
    if(HitSomething)
    {
      // Debug.Log(hit.point.ToString());
      // Debug.DrawLine(hit.point, hit.point+new Vector3(0,1,0), Color.red, 1.0f);
      if(SelectedTank)
      {
        SelectedTank.MoveToWayPoint = true;
        SelectedTank.agent.SetDestination(hit.point);
      }

    }
  }
}

