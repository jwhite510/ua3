using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public NavMeshAgent agent;
    public bool MoveToWayPoint = false;
    private float last_fire_time;
    private bool last_fire_side_right = false;
    private bool mech_start_turn = false;
    private enum TurnDirection {None, Left, Right};
    private TurnDirection turnDirection = TurnDirection.None;

    public healthbar thehealthbar;

    public Renderer BaseRenderer;
    public Renderer L1_joint;
    public Renderer L2_joint;
    public Renderer L3_joint;
    public Renderer R1_joint;
    public Renderer R2_joint;
    public Renderer R3_joint;
    public Renderer turret_base_render;
    public Renderer detail1;
    public Renderer detail2;
    public Renderer detail3;
    public Renderer detail4;

    // Start is called before the first frame update
    void Start()
    {
      base.Start();
      if(team == 1)
      {
        BaseRenderer.GetComponent<Renderer>().material.color = Color.green;
        L1_joint.GetComponent<Renderer>().material.color = Color.green;
        L2_joint.GetComponent<Renderer>().material.color = Color.green;
        L3_joint.GetComponent<Renderer>().material.color = Color.green;
        R1_joint.GetComponent<Renderer>().material.color = Color.green;
        R2_joint.GetComponent<Renderer>().material.color = Color.green;
        R3_joint.GetComponent<Renderer>().material.color = Color.green;
        turret_base_render.GetComponent<Renderer>().material.color = Color.green;
        detail1.GetComponent<Renderer>().material.color = Color.green;
        detail2.GetComponent<Renderer>().material.color = Color.green;
        detail3.GetComponent<Renderer>().material.color = Color.green;
        detail4.GetComponent<Renderer>().material.color = Color.green;


      }
      else if(team == 2)
      {
         BaseRenderer.GetComponent<Renderer>().material.color = Color.red;
        L1_joint.GetComponent<Renderer>().material.color = Color.red;
        L2_joint.GetComponent<Renderer>().material.color = Color.red;
        L3_joint.GetComponent<Renderer>().material.color = Color.red;
        R1_joint.GetComponent<Renderer>().material.color = Color.red;
        R2_joint.GetComponent<Renderer>().material.color = Color.red;
        R3_joint.GetComponent<Renderer>().material.color = Color.red;
        turret_base_render.GetComponent<Renderer>().material.color = Color.red;
        detail1.GetComponent<Renderer>().material.color = Color.red;
        detail2.GetComponent<Renderer>().material.color = Color.red;
        detail3.GetComponent<Renderer>().material.color = Color.red;
        detail4.GetComponent<Renderer>().material.color = Color.red;
       
      }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
      if(MoveToWayPoint && !player_controlled)
      {
        // Debug.Log("mech move to target");
        if(agent.path.corners.Length>1)
        {
          // move to this point
          // Debug.DrawLine(
              // agent.path.corners[1],
              // agent.path.corners[1] + new Vector3(0.1f,10.0f,0.1f),
              // Color.green,
              // 0.0f
              // );

          // movement direction
          Vector3 move_direction = agent.path.corners[1] - vehicle_base.transform.position;
          move_direction.Normalize();

          float waypoint_distance = Vector3.Distance(agent.path.corners[1], vehicle_base.transform.position);

          float dotprod = -Vector3.Dot(move_direction, vehicle_base.transform.right);
          Vector3 crossprod = Vector3.Cross(move_direction, vehicle_base.transform.right);

          // Debug.Log("dotprod => "+dotprod);
          // Debug.Log("crossprod => "+crossprod.ToString());

          float turnvalue = Vector3.Dot(vehicle_base.transform.up, crossprod);

          // Debug.Log("turnvalue => "+turnvalue);
          // Debug.Log("waypoint_distance => "+waypoint_distance);
          if(!mech_start_turn)
          {
            if(turnvalue>0.3)
            {
              // DriveLegs(-1, 1);
              mech_start_turn = true;
            }
            else if(turnvalue < -0.3)
            {
              // DriveLegs(1, -1);
              mech_start_turn = true;
            }
            else
            {
              // get distance to target
              if(dotprod>0 && waypoint_distance > 2)
              {
                DriveLegs(1, 1);
              }
              else if(dotprod<0 && waypoint_distance > 2)
              {
                DriveLegs(-1, -1);
              }
              else
              {
                StopLegs();
              }
            }
          }
          if(mech_start_turn)
          {
            if(turnvalue > 0)
            {
              turnDirection = TurnDirection.Left;
              mech_start_turn = false;
              // DriveLegs(-1, 1);
            }
            else if(turnvalue < 0)
            {
              turnDirection = TurnDirection.Right;
              mech_start_turn = false;
              // DriveLegs(1, -1);
            }
          }
          if(turnDirection!=TurnDirection.None)
          {
            if(turnDirection==TurnDirection.Left)
            {
              if(turnvalue > 0)
              {
                DriveLegs(-1, 1);
              }
              else
              {
                turnDirection = TurnDirection.None;
              }
            }
            else if(turnDirection==TurnDirection.Right)
            {
              if(turnvalue < 0)
              {
                DriveLegs(1, -1);
              }
              else
              {
                turnDirection = TurnDirection.None;
              }
            }

          }

        }
      }
      if(!player_controlled)
      {
        // Debug.Log("mech aim at target");
        GameObject closestobject = FindNearestEnemyVehicle();
        if(closestobject)
        {
          // Debug.DrawLine(
            // lturretBarrelMarker.position,
            // lturretBarrelMarker.position + (-4*lturretBarrelMarker.up),
            // Color.red,
            // 0.0f
              // );

          Vector3 intend_aim_direction = closestobject.transform.position - turretBase.position;
          intend_aim_direction.Normalize();

          // Debug.DrawLine(
            // lturretBarrelMarker.position,
            // lturretBarrelMarker.position + (4*intend_aim_direction),
            // Color.yellow,
            // 0.0f
              // );

          float left_right = Vector3.Dot(intend_aim_direction, turretBase.forward);

          // from barrel tip
          float up_down = Vector3.Dot(intend_aim_direction, lturretBarrelMarker.right);

          float aim_dot_prod = Vector3.Dot(intend_aim_direction, (-lturretBarrelMarker.up));

          if(aim_dot_prod > 0.95)
          {
            // Debug.Log("mech fire cannon");
            // FireCannons();
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

      if((Time.time - last_fire_time) > 0.8)
      {
        last_fire_time = Time.time;
        if(last_fire_side_right == true)
        {
          GameObject proj = Instantiate(projectile, lturretBarrelMarker.position-1*lturretBarrelMarker.up, lturretBarrelMarker.rotation);
          // rturretBarrelMarker.position + 1*rturretBarrelMarker.up
          proj.GetComponent<Rigidbody>().velocity = -20*lturretBarrelMarker.up;
          proj.GetComponent<projectile>().owningteam = this.team;
          last_fire_side_right = false;
        }
        else{
          GameObject proj = Instantiate(projectile, rturretBarrelMarker.position-1*rturretBarrelMarker.up, rturretBarrelMarker.rotation);
          // rturretBarrelMarker.position + 1*rturretBarrelMarker.up
          proj.GetComponent<Rigidbody>().velocity = -20*rturretBarrelMarker.up;
          proj.GetComponent<projectile>().owningteam = this.team;
          last_fire_side_right = true;
        }


      }
    }
}
