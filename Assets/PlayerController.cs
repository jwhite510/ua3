using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

  public Transform playerCamera;
  public Tank controlled_tank;
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
    SelectedVehicleText.text = controlled_tank.name;
    controlled_tank.player_controlled = true;

  }

  // Update is called once per frame
  void Update()
  {

    // playerCamera.position = controlled_tank.camera_location.position;
    playerCamera.position = controlled_tank.camera_location.transform.position;
    playerCamera.rotation = controlled_tank.camera_location.transform.rotation;
    // playerCamera.forward = playerCamera.right;

    // Vector3 rotcamera = playerCamera.rotation.eulerAngles;
    // rotcamera.y += drotx;
    // rotcamera.x -= droty;

    // playerCamera.rotation = Quaternion.Euler(rotcamera);
    // check for player mouse button click
    // Debug.Log("ControlModeMouse"+ControlModeMouse);

    // Debug.Log(Time.time);
    if(Input.GetMouseButtonDown(0) && ControlModeMouse)
    {
      // Debug.Log("mouse down");
      if(!handling_mouseclick)
      {
        ClickTime = Time.time;
        handling_mouseclick = true;
      }
      else if(handling_mouseclick)
      {
        // Debug.Log("Handle Double Click");
        // possess the clicked unit
        HandleDoubleClick();
        handling_mouseclick = false;
      }
    }
    if(handling_mouseclick)
    {
      if(Time.time > (ClickTime + 0.2))
      {
        // Debug.Log("Handle Single Click");
        // select the clicked unit
        HandleSingleClick();
        handling_mouseclick = false;
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
    if(Input.GetMouseButtonDown(1) && ControlModeMouse)
    {
      HandleSingleRightClick();
    }
  }

  void FixedUpdate()
  {

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
      controlled_tank.RotateTurret(mouseX, mouseY);
    }
  }

  void HandleSingleClick()
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
  void HandleDoubleClick()
  {
    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    bool HitSomething = Physics.Raycast(ray, out hit);
    if(HitSomething)
    {
      Tank tankclicked = hit.transform.gameObject.GetComponentInParent<Tank>();
      if(tankclicked)
      {
        controlled_tank.player_controlled = false;
        controlled_tank = tankclicked;
        controlled_tank.player_controlled = true;
        // drotx = 0;
        // droty = 0;
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

