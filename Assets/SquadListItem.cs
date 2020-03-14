using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SquadListItem : MonoBehaviour, IDropHandler
{
    public VehicleBase vehicleReference;

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
      Debug.Log("OnDrop called");
      // get the object that was dropped
      if(eventData.pointerDrag != null)
      {
        VehicleListItemS v = eventData.pointerDrag.GetComponent<VehicleListItemS>();
        vehicleReference = v.vehicleReference;
        Destroy(v.gameObject);
        // Debug.Log("vehicleReference.name => "+vehicleReference.name);
        GameObject proj = Instantiate(SquadMember, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        proj.GetComponent<SquadMemberS>().SetName(vehicleReference.name);
        proj.transform.parent = this.gameObject.transform;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.UpdateUnitsUI();
      }
    }

}
