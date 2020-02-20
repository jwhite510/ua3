using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform playerCamera;
    public Tank tank;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
      // FindObjectOfType<GameManager>()

    }

    // Update is called once per frame
    void Update()
    {

      // playerCamera.position = tank.camera_location.position;
      playerCamera.position = tank.camera_location.transform.position;
      playerCamera.rotation = tank.camera_location.transform.rotation;


      // check for player mouse button click
      if(Input.GetMouseButtonDown(0))
      {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool HitSomething = Physics.Raycast(ray, out hit);
        if(HitSomething)
        {
          Debug.Log(hit.point.ToString());
          Debug.DrawLine(hit.point, hit.point+new Vector3(0,1,0), Color.red, 1.0f);
          Debug.Log("draw debug line");
          Debug.Log(hit.transform.gameObject.name);
          Tank tankclicked = hit.transform.gameObject.GetComponentInParent<Tank>();
          Debug.Log(tankclicked.name);
          tank = tankclicked;
        }


      }

    }

    void FixedUpdate()
    {


    }
}
