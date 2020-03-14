using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadMemberS : MonoBehaviour
{
    private bool isHandlingClick = false;
    public SquadListItem squadListItem;
    private float buttonClickTime = 0.0f;
    public VehicleBase vehicleReference;
    public Text SquadMemberName;
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
          isHandlingClick = false;
        }

      }

    }
    public void SetName(string name)
    {
      // Debug.Log("setting name to "+name);
      // SquadMemberName = GetComponent<Text>();
      SquadMemberName.text = name;
    }
    public void Clicked()
    {
      if(isHandlingClick == true)
      {
        HandleDoubleClick();
        isHandlingClick = false;
      }
      else
      {
        buttonClickTime = Time.time;
        isHandlingClick = true;
      }
    }
    void HandleDoubleClick()
    {
      Debug.Log("squad member handle double click");
      // player control this
      PlayerController playercontroller = FindObjectOfType<PlayerController>();
      playercontroller.ControlVehicle(vehicleReference);

    }
}
