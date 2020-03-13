using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SquadListItem : MonoBehaviour, IDropHandler
{

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
        Debug.Log("v.buttonText => "+v.buttonText.text);
        Debug.Log("v.gameObject.name => "+v.gameObject.name);
        Destroy(v.gameObject);
      }
      GameObject proj = Instantiate(SquadMember, new Vector3(0,0,0), new Quaternion(0,0,0,0));
      // proj.transform.parent = this.gameObject;
      proj.transform.parent = this.gameObject.transform;
    }

}
