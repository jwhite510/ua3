using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform playerCamera;
    public Tank tank;
    // Start is called before the first frame update
    void Start()
    {
      // FindObjectOfType<GameManager>()

    }

    // Update is called once per frame
    void Update()
    {

      // playerCamera.position = tank.camera_location.position;
      playerCamera.position = tank.transform.position;

    }

    void FixedUpdate()
    {


    }
}
