using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SquadListItem : MonoBehaviour, IDropHandler
{
    public VehicleBase squadLeaderVehicle;
    public GameObject squadLeaderButton;
    public List<VehicleBase> squadMembersList = new List<VehicleBase>();
    private bool isHandlingClick = false;
    private float buttonClickTime = 0.0f;

    public GameObject SquadMember;
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

    public void OnDrop(PointerEventData eventData)
    {
      // Debug.Log("OnDrop called");
      // get the object that was dropped
      if(eventData.pointerDrag != null)
      {
        VehicleListItemS v = eventData.pointerDrag.GetComponent<VehicleListItemS>();
        AddSquadMember(v.vehicleReference);
        Destroy(v.gameObject);
      }
    }
    public void AddSquadMember(VehicleBase vehicleBase)
    {
        // vehicleReference = v.vehicleReference;
        // Debug.Log("vehicleReference.name => "+vehicleReference.name);
        GameObject proj = Instantiate(SquadMember, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        proj.GetComponent<SquadMemberS>().SetName(vehicleBase.name);
        proj.GetComponent<SquadMemberS>().squadListItem = this;
        proj.transform.parent = this.gameObject.transform;
        squadMembersList.Add(vehicleBase);
        vehicleBase.ui_element = proj;
        vehicleBase.ui_element_name = "SquadMember";

    }
    public void SetSquadLeader(VehicleBase vehicleBase)
    {
      squadLeaderVehicle = vehicleBase;
      // find the button text
      Text squadLeaderText = squadLeaderButton.GetComponentInChildren<Text>();
      squadLeaderText.text = vehicleBase.name;

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
        isHandlingClick = true;
      }
    }
    private void HandleDoubleClick()
    {

      PlayerController playercontroller = FindObjectOfType<PlayerController>();
      playercontroller.ControlVehicle(squadLeaderVehicle);
    }
    private void HandleSingleClick()
    {
      PlayerController playercontroller = FindObjectOfType<PlayerController>();
      playercontroller.SelectVehicle(squadLeaderVehicle);
    }
}
