using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleListItemS : MonoBehaviour
{


    public Text buttonText;
    public VehicleBase vehicleReference;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void SetVehicle(VehicleBase vehicleReferenceIn)
    {
      vehicleReference = vehicleReferenceIn;
    }

    public void SetText(string text)
    {
      // Debug.Log("button did something DoSomething");
      buttonText.text = text;
    }

    public void SetButtonText(string buttontext)
    {

    }
}
