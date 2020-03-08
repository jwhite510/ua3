using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleListItemS : MonoBehaviour
{


    public Text buttonText;
    public VehicleBase vehicleReference;
    public PlayerController playerController;

    private bool isHandlingClick = false;
    private float buttonClickTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

      if(isHandlingClick == true)
      {
        if((Time.time - buttonClickTime) > 0.2)
        {
          HandleSingleClick();
          isHandlingClick = false;
        }

      }

    }


    public void SetVehicle(VehicleBase vehicleReferenceIn, PlayerController playerControllerIn)
    {
      vehicleReference = vehicleReferenceIn;
      playerController = playerControllerIn;
    }

    public void SetText(string text)
    {
      buttonText.text = text;
    }

    public void SetButtonText(string buttontext)
    {

    }

    public void ButtonClicked()
    {
      if(isHandlingClick == true)
      {
        HandleDoubleClick();
        isHandlingClick = false;
      }
      else
      {
        buttonClickTime = Time.time;
        // Debug.Log("possess " + vehicleReference.name);
        isHandlingClick = true;
      }
    }
    private void HandleDoubleClick()
    {
      playerController.ControlVehicle(vehicleReference);
    }
    private void HandleSingleClick()
    {
      playerController.SelectVehicle(vehicleReference);
    }
}
