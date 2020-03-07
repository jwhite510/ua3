using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private float collisionTime;
    private bool firstCollisionHappened = false;
    public int owningteam = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(firstCollisionHappened)
      {
        if((Time.time - collisionTime) > 0.3)
        {
          // Debug.Log("destroy object");
          Destroy(gameObject);
        }

      }

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
      VehicleBase vehiclehit = collisionInfo.collider.GetComponentInParent<VehicleBase>();
      if(vehiclehit)
      {
        // apply damage
        if(vehiclehit.team != owningteam)
        {

          if(vehiclehit is Tank)
          {
            vehiclehit.current_health -= 0.05f;
          }
          else if(vehiclehit is battlestation)
          {
            vehiclehit.current_health -= 0.01f;
          }

          vehiclehit.thishealthbar.SetHealth(vehiclehit.current_health);
          // if the vehicle is destroyed
          if(vehiclehit.current_health < 0)
          {
            // if the vehicle is player controlled
            vehiclehit.gameObject.transform.position = new Vector3(0,-100,0);
            // Debug.Log("Destroy vehiclehit Invoked");
            vehiclehit.Invoke("DestroyThisVehicle", 5);
            vehiclehit.isBeingDestroyed = true;
          }
        }

      }

      collisionTime = Time.time;
      firstCollisionHappened = true;

    }
}
