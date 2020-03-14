using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SquadListItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public VehicleBase squadLeaderVehicle;
    public GameObject squadLeaderButton;
    public List<VehicleBase> squadMembersList = new List<VehicleBase>();
    private bool isHandlingClick = false;
    private float buttonClickTime = 0.0f;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Transform originalParent;

    public GameObject SquadMember;
    private void Awake()
    {
      rectTransform = GetComponent<RectTransform>();
      canvasGroup = GetComponent<CanvasGroup>();
    }
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
    public void OnPointerDown(PointerEventData eventData)
    {
      // Debug.Log("OnPointerDown test");
    }
    public void OnDrag(PointerEventData eventData)
    {
      // Debug.Log("OnDrag test");
      rectTransform.anchoredPosition += eventData.delta;
      canvasGroup.alpha = 0.6f;
      canvasGroup.blocksRaycasts = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
      // Debug.Log("OnBeginDrag test");
      // get handle on UI
      PlayerController playercontroller = FindObjectOfType<PlayerController>();
      Transform canvasTransform;
      Transform[] ts = playercontroller.PlayerUI.transform.GetComponentsInChildren<Transform>(true);
      foreach(Transform t in ts)
      {
        if (t.gameObject.name == "Canvas")
        {
          transform.parent = t;
          break;
        }
      }
      // SpawnUnitsCursor.active = false;
      // transform.parent = canvasTransform;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
      Debug.Log("OnEndDrag test");
      transform.parent = originalParent;
      canvasGroup.alpha = 1.0f;
      canvasGroup.blocksRaycasts = true;
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
        proj.GetComponent<SquadMemberS>().vehicleReference = vehicleBase;
        proj.transform.parent = this.gameObject.transform;
        squadMembersList.Add(vehicleBase);
        vehicleBase.ui_element = proj;
        vehicleBase.ui_element_name = "SquadMember";

    }
    public void SetSquadLeader(VehicleBase vehicleBase)
    {

      // set parent
      GameObject playercontroller = FindObjectOfType<PlayerController>().gameObject;
      playercontroller.GetComponent<PlayerController>().playerSquads.Add(gameObject.GetComponent<SquadListItem>());
      transform.parent = playercontroller.GetComponent<PlayerController>().VehicleReferenceButtonList.transform;

      squadLeaderVehicle = vehicleBase;
      // find the button text
      Text squadLeaderText = squadLeaderButton.GetComponentInChildren<Text>();
      squadLeaderText.text = vehicleBase.name;
      originalParent = transform.parent;


      vehicleBase.ui_element = gameObject;
      vehicleBase.ui_element_name = "SquadListItemO";





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
