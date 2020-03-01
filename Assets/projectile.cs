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
          vehiclehit.current_health -= 0.05f;
          vehiclehit.thishealthbar.SetHealth(vehiclehit.current_health);
          if(vehiclehit.current_health < 0)
          {
            // if the vehicle is player controlled
            if(vehiclehit.player_controlled == true)
            {
              PlayerController playercontroller = FindObjectOfType<PlayerController>();
              battlestation[] battlestations = FindObjectsOfType<battlestation>();
              playercontroller.ControlVehicle(battlestations[0]);
            }
            vehiclehit.gameObject.transform.position = new Vector3(0,100,0);
          }
        }

      }

      collisionTime = Time.time;
      firstCollisionHappened = true;

    }
}
