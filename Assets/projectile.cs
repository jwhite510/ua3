using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private float collisionTime;
    private bool firstCollisionHappened = false;

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
      Tank tankhit = collisionInfo.collider.GetComponentInParent<Tank>();
      if(tankhit)
      {
        Debug.Log("hit tank:"+tankhit.name);
      }

      collisionTime = Time.time;
      firstCollisionHappened = true;

    }
}
