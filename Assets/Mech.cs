using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : VehicleBase
{


    public ConfigurableJoint L1_leg;
    public ConfigurableJoint L2_leg;
    public ConfigurableJoint L3_leg;

    public ConfigurableJoint R1_leg;
    public ConfigurableJoint R2_leg;
    public ConfigurableJoint R3_leg;
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

      float movescalar = 5.5f;
      float sinval = Mathf.Sin(movescalar*Time.time);
      float cosval = Mathf.Cos(movescalar*Time.time);
      sinval*=15;
      cosval*=15;
      // Debug.Log("sinval => "+sinval);

      Quaternion e1 = new Quaternion(0,0,0,0);
      e1.eulerAngles = new Vector3(sinval,cosval,0);
      L1_leg.targetRotation = e1;
      L3_leg.targetRotation = e1;
      R1_leg.targetRotation = e1;
      R3_leg.targetRotation = e1;

      sinval = Mathf.Sin((movescalar*Time.time) + Mathf.PI);
      cosval = Mathf.Cos((movescalar*Time.time) + Mathf.PI);
      sinval*=15;
      cosval*=15;
      e1 = new Quaternion(0,0,0,0);
      e1.eulerAngles = new Vector3(sinval,cosval,0);
      L2_leg.targetRotation = e1;
      R2_leg.targetRotation = e1;


    }
}
