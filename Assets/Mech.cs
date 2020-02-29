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

    public ConfigurableJoint turretJoint;
    public ConfigurableJoint turretBarrelJoint1;
    public ConfigurableJoint turretBarrelJoint2;
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
    }

    public void DriveLegs(float Right, float Left)
    {

      float movescalar = 9.0f;
      float stridescalar = 15;
      float rightspeed_scalar = Right;
      float leftspeed_scalar = Left;


      float sinval = Mathf.Sin(leftspeed_scalar*movescalar*Time.time + Mathf.PI);
      float cosval = Mathf.Cos(leftspeed_scalar*movescalar*Time.time + Mathf.PI);
      sinval*=stridescalar;
      cosval*=stridescalar;
      Quaternion e1 = new Quaternion(0,0,0,0);
      e1.eulerAngles = new Vector3(sinval,cosval,0);
      L1_leg.targetRotation = e1;
      L3_leg.targetRotation = e1;

      sinval = Mathf.Sin((leftspeed_scalar*movescalar*Time.time));
      cosval = Mathf.Cos((leftspeed_scalar*movescalar*Time.time));
      sinval*=stridescalar;
      cosval*=stridescalar;
      e1 = new Quaternion(0,0,0,0);
      e1.eulerAngles = new Vector3(sinval,cosval,0);
      L2_leg.targetRotation = e1;


      sinval = Mathf.Sin(-((rightspeed_scalar*movescalar*Time.time)));
      cosval = Mathf.Cos(-((rightspeed_scalar*movescalar*Time.time)));
      sinval*=stridescalar;
      cosval*=stridescalar;
      e1 = new Quaternion(0,0,0,0);
      e1.eulerAngles = new Vector3(sinval,cosval,0);
      R2_leg.targetRotation = e1;


      sinval = Mathf.Sin(-((rightspeed_scalar*movescalar*Time.time)) + Mathf.PI);
      cosval = Mathf.Cos(-((rightspeed_scalar*movescalar*Time.time)) + Mathf.PI);
      sinval*=stridescalar;
      cosval*=stridescalar;
      e1 = new Quaternion(0,0,0,0);
      e1.eulerAngles = new Vector3(sinval,cosval,0);
      R1_leg.targetRotation = e1;
      R3_leg.targetRotation = e1;

    }
    public void StopLegs()
    {
      Quaternion e1 = new Quaternion(0,0,0,0);
      e1.eulerAngles = new Vector3(0,0,0);
      L1_leg.targetRotation = e1;
      L3_leg.targetRotation = e1;
      L2_leg.targetRotation = e1;
      R2_leg.targetRotation = e1;
      R1_leg.targetRotation = e1;
      R3_leg.targetRotation = e1;
    }

    public void RotateTurret(float x, float y)
    {
      Debug.Log("mech RotateTurrret"+x+" "+y);
      Quaternion e1 = turretJoint.targetRotation;

      Debug.Log("e1.eulerAngles.ToString() => "+e1.eulerAngles.ToString());
      Vector3 newTargetRotation = e1.eulerAngles;
      // newTargetRotation.z += y;
      newTargetRotation.y -= x;

      Quaternion e2 = new Quaternion(0,0,0,0);
      e2.eulerAngles = newTargetRotation;

      turretJoint.targetRotation = e2;

      // rotate barrel up and down
      e1 = turretBarrelJoint1.targetRotation;
      newTargetRotation = e1.eulerAngles;
      newTargetRotation.z += y;
      e2.eulerAngles = newTargetRotation;
      turretBarrelJoint1.targetRotation = e2;
      turretBarrelJoint2.targetRotation = e2;


    }
}
