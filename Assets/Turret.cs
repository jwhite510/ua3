using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Turret : MonoBehaviour
{
  public GameObject turret_hinge;
  public Transform tank_barrel;
  public Transform projectile_spawn_point;
  public GameObject projectile;
  public GameObject vehicle_base;
  public VehicleBase ownerOfThisTurret;
  private float last_fire_time = 0;

  // Start is called before the first frame update
  void Start()
  {
  }
  // Update is called once per frame
  void Update()
  {
    // Debug.Log("tank update called");
  }
  void FixedUpdate()
  {
  }
  public void RotateTurret(float x, float y)
  {
    Vector3 rot_turret = turret_hinge.transform.localRotation.eulerAngles;
    rot_turret.y += x;
    rot_turret.z += y;

    if(rot_turret.z > 350 || rot_turret.z < 80)
    {
      turret_hinge.transform.localRotation = Quaternion.Euler(rot_turret);
    }
    else
    {
      rot_turret.z -= y;
      turret_hinge.transform.localRotation = Quaternion.Euler(rot_turret);
    }

    // Debug.Log("rot_turret.y"+rot_turret.y);
    // Debug.Log("rot_turret.z"+rot_turret.z);
    // Debug.Log(hello1);
    // Debug.Log(hello2);
    // Debug.Log( hello2||hello1 );
  }

  public void autoRotateTurret(GameObject target)
  {
    Vector3 current_barrel_direction = -1*tank_barrel.up;
    // direction to target
    Vector3 intend_aim_direction = target.transform.position - tank_barrel.position;
    intend_aim_direction.Normalize();

    float right_dot = Vector3.Dot(intend_aim_direction, tank_barrel.right);
    float forward_dot = Vector3.Dot(intend_aim_direction, tank_barrel.forward);
    float on_target = -Vector3.Dot(intend_aim_direction, tank_barrel.up);

    if(on_target > 0.9)
    {
      FireCannon();
    }
    RotateTurret(-300*forward_dot*Time.deltaTime, 300*right_dot*Time.deltaTime);
  }
  public void FireCannon()
  {
    if((Time.time - last_fire_time) > 1)
    {
      GameObject proj = Instantiate(projectile, projectile_spawn_point.position, projectile_spawn_point.rotation);


      if(vehicle_base.GetComponent<Rigidbody>())
      {
        proj.GetComponent<Rigidbody>().velocity = 20*projectile_spawn_point.right + vehicle_base.GetComponent<Rigidbody>().velocity;
        proj.GetComponent<projectile>().owningteam = ownerOfThisTurret.team;
      }
      else
      {
        proj.GetComponent<Rigidbody>().velocity = 20*projectile_spawn_point.right;
        proj.GetComponent<projectile>().owningteam = ownerOfThisTurret.team;
      }


      last_fire_time = Time.time;
    }
  }
}
