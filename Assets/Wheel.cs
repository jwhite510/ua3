using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    public Rigidbody wheelrigidbody;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
      if(Input.GetKey("w"))
      {
        wheelrigidbody.AddForce(Time.deltaTime*transform.right * 1000);
        // Color color = new Color(0.0f, 0.0f, 1.0f);
        // Debug.DrawLine(transform.position, transform.position+(Time.deltaTime*1000*transform.right), color);
      }
      else if(Input.GetKey("s"))
      {
        wheelrigidbody.AddForce(-Time.deltaTime*transform.right * 1000);
      }
    }



}
