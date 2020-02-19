using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
     public Wheel LeftWheel1;
     public Wheel LeftWheel2;
     public Wheel LeftWheel3;
     public Wheel RightWheel1;
     public Wheel RightWheel2;
     public Wheel RightWheel3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DriveForward()
    {

      Debug.Log("DriveForward called");

    }

    void FixedUpdate()
    {
      if(Input.GetKey("w"))
      {
        // Debug.Log("drive forward");
        LeftWheel1.Drive(1000);
        LeftWheel2.Drive(1000);
        LeftWheel3.Drive(1000);
        RightWheel1.Drive(1000);
        RightWheel2.Drive(1000);
        RightWheel3.Drive(1000);
      }
      else if(Input.GetKey("s"))
      {
        LeftWheel1.Drive(-1000);
        LeftWheel2.Drive(-1000);
        LeftWheel3.Drive(-1000);
        RightWheel1.Drive(-1000);
        RightWheel2.Drive(-1000);
        RightWheel3.Drive(-1000);
      }
      else if(Input.GetKey("d"))
      {
        LeftWheel1.Drive(1000);
        LeftWheel2.Drive(1000);
        LeftWheel3.Drive(1000);
        RightWheel1.Drive(-1000);
        RightWheel2.Drive(-1000);
        RightWheel3.Drive(-1000);
      }
      else if(Input.GetKey("a"))
      {
        LeftWheel1.Drive(-1000);
        LeftWheel2.Drive(-1000);
        LeftWheel3.Drive(-1000);
        RightWheel1.Drive(1000);
        RightWheel2.Drive(1000);
        RightWheel3.Drive(1000);
      }
    }


}
