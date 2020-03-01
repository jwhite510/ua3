using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private float collisionTime;
    private bool firstCollisionHappened = false;
    public VehicleBase whoshotthis;

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
        Debug.Log(whoshotthis.name+"(TEAM)["+whoshotthis.team+"]hit vehicle:"+vehiclehit.name+"(TEAM)["+vehiclehit.team+"]");
        // apply damage
        if(vehiclehit.team != whoshotthis.team)
        {
          vehiclehit.current_health -= 0.1f;
          vehiclehit.thishealthbar.SetHealth(vehiclehit.current_health);
        }

      }

      collisionTime = Time.time;
      firstCollisionHappened = true;

    }
}
