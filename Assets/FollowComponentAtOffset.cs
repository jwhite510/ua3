using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowComponentAtOffset : MonoBehaviour
{

  public Transform followComponent;

  public Vector3 offset;


  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // transform.position = followComponent.transform.position + new Vector3(0,1,0);
    transform.position = followComponent.transform.position;
    transform.position += offset.y*followComponent.transform.up;
    transform.position += offset.z*followComponent.transform.forward;
    transform.position += offset.x*followComponent.transform.right;

    transform.forward = followComponent.transform.forward;
    // transform.rotation = followComponent.transform.rotation;
    // transform.Rotate(transform.right);
    // transform.rotation = followComponent.transform.right;

  }
}
