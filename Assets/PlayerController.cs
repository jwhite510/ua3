using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  public Transform playerCamera;
  public Tank tank;
  public Camera cam;

  bool ControlModeMouse;


  // Start is called before the first frame update
  void Start()
  {
    // FindObjectOfType<GameManager>()
    Cursor.lockState = CursorLockMode.Locked;
    ControlModeMouse = true; // locked

  }

  // Update is called once per frame
  void Update()
  {

    // playerCamera.position = tank.camera_location.position;
    playerCamera.position = tank.camera_location.transform.position;
    playerCamera.rotation = tank.camera_location.transform.rotation;


    // check for player mouse button click
    // Debug.Log("ControlModeMouse"+ControlModeMouse);
    if(Input.GetMouseButtonDown(0) && ControlModeMouse)
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
          Debug.Log(tankclicked.name);
          tank = tankclicked;
        }
      }
    }

  }

  void FixedUpdate()
  {

    if(Input.GetKey("w"))
    {
      tank.DriveWheels(10,10);
    }
    else if(Input.GetKey("s"))
    {
      tank.DriveWheels(-10,-10);
    }
    else if(Input.GetKey("d"))
    {
      tank.DriveWheels(10,-10);
    }
    else if(Input.GetKey("a"))
    {
      tank.DriveWheels(-10,10);
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
}
