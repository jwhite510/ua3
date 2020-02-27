using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : VehicleBase
{


    public ConfigurableJoint legjoint;
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

      float sinval = Mathf.Sin(0.5f*Time.time);
      float cosval = Mathf.Cos(0.5f*Time.time);
      sinval*=30;
      cosval*=30;
      Debug.Log("sinval => "+sinval);

      Quaternion e1 = new Quaternion(0,0,0,0);
      e1.eulerAngles = new Vector3(sinval,cosval,0);
      legjoint.targetRotation = e1;


    }
}
