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
    public Transform turretBase;
    public Transform lturretBarrelMarker;
    public Transform rturretBarrelMarker;
    public GameObject projectile;
    private float last_fire_time;
    private bool last_fire_side_right = false;

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
      if(!player_controlled)
      {
        // Debug.Log("mech aim at target");
        GameObject closestobject = FindNearestEnemyVehicle();
        if(closestobject)
        {
          Debug.DrawLine(
            lturretBarrelMarker.position,
            lturretBarrelMarker.position + (-4*lturretBarrelMarker.up),
            Color.red,
            0.0f
              );

          Vector3 intend_aim_direction = closestobject.transform.position - turretBase.position;
          intend_aim_direction.Normalize();

          Debug.DrawLine(
            lturretBarrelMarker.position,
            lturretBarrelMarker.position + (4*intend_aim_direction),
            Color.yellow,
            0.0f
              );

          float left_right = Vector3.Dot(intend_aim_direction, turretBase.forward);

          // from barrel tip
          float up_down = Vector3.Dot(intend_aim_direction, lturretBarrelMarker.right);

          float aim_dot_prod = Vector3.Dot(intend_aim_direction, (-lturretBarrelMarker.up));

          if(aim_dot_prod > 0.95)
          {
            Debug.Log("mech fire cannon");
            FireCannons();
          }

          RotateMechTurret(left_right, -up_down);

        }


      }
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

    public void RotateMechTurret(float x, float y)
    {
      // Debug.Log("mech RotateTurrret"+x+" "+y);
      Quaternion e1 = turretJoint.targetRotation;

      // Debug.Log("e1.eulerAngles.ToString() => "+e1.eulerAngles.ToString());
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
    public void FireCannons()
    {

      if((Time.time - last_fire_time) > 0.4)
      {
        last_fire_time = Time.time;
        if(last_fire_side_right == true)
        {
          GameObject proj = Instantiate(projectile, lturretBarrelMarker.position-1*lturretBarrelMarker.up, lturretBarrelMarker.rotation);
          // rturretBarrelMarker.position + 1*rturretBarrelMarker.up
          proj.GetComponent<Rigidbody>().velocity = -20*lturretBarrelMarker.up;
          last_fire_side_right = false;
        }
        else{
          GameObject proj = Instantiate(projectile, rturretBarrelMarker.position-1*rturretBarrelMarker.up, rturretBarrelMarker.rotation);
          // rturretBarrelMarker.position + 1*rturretBarrelMarker.up
          proj.GetComponent<Rigidbody>().velocity = -20*rturretBarrelMarker.up;
          last_fire_side_right = true;
        }


      }
    }
}
