using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleListItemS : MonoBehaviour
{


    public Text buttonText;
    public VehicleBase vehicleReference;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetVehicle(VehicleBase vehicleReferenceIn, PlayerController playerControllerIn)
    {
      vehicleReference = vehicleReferenceIn;
      playerController = playerControllerIn;
    }

    public void SetText(string text)
    {
      // Debug.Log("button did something DoSomething");
      buttonText.text = text;
    }

    public void SetButtonText(string buttontext)
    {

    }

    public void ButtonClicked()
    {
      Debug.Log("possess " + vehicleReference.name);
      playerController.ControlVehicle(vehicleReference);
    }
}
