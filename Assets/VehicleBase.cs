using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleBase : MonoBehaviour
{
     public Transform cameralocation;
     public int team = 0;
     public bool player_controlled = false;
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
}
