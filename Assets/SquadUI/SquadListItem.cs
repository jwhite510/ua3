using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SquadListItem : MonoBehaviour, IDropHandler
{
    public VehicleBase squadLeaderVehicle;
    public GameObject squadLeaderButton;

    public GameObject SquadMember;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
        proj.transform.parent = this.gameObject.transform;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.UpdateUnitsUI();
    }
    public void SetSquadLeader(VehicleBase vehicleBase)
    {
      squadLeaderVehicle = vehicleBase;
      // find the button text
      Text squadLeaderText = squadLeaderButton.GetComponentInChildren<Text>();
      squadLeaderText.text = vehicleBase.name;

    }

}
