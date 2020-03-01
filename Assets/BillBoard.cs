using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    private Transform cam;

    void Start()
    {

      GameObject playercontroller = FindObjectOfType<PlayerController>().gameObject;
      cam = playercontroller.GetComponentInChildren<Camera>().transform;

    }

    // Update is called once per frame
    void LateUpdate() // always called after update function
    {
      transform.LookAt(transform.position + cam.forward);
    }
}
