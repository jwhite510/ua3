﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VehicleListItemS : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{


    public GameObject SquadListItemO;
    public Text buttonText;
    public VehicleBase vehicleReference;
    public PlayerController playerController;

    private bool isHandlingClick = false;
    private float buttonClickTime = 0.0f;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Transform originalParent;

    private void Awake()
    {
      rectTransform = GetComponent<RectTransform>();
      canvasGroup = GetComponent<CanvasGroup>();
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
      if(eventData.pointerDrag != null)
      {
        // squad leader
        Debug.Log("vehicleReference.name => "+vehicleReference.name);

        // new squad member
        if(eventData.pointerDrag.GetComponent<VehicleListItemS>())
        {
          VehicleListItemS v = eventData.pointerDrag.GetComponent<VehicleListItemS>();
          // create a squad
          GameObject newSquad = Instantiate(SquadListItemO, new Vector3(0,0,0), new Quaternion(0,0,0,0));
          newSquad.GetComponent<SquadListItem>().SetSquadLeader(vehicleReference);
          newSquad.GetComponent<SquadListItem>().AddSquadMember(v.vehicleReference);

          Destroy(eventData.pointerDrag.gameObject);
          Destroy(gameObject);
        }
        else if(eventData.pointerDrag.GetComponent<SquadListItem>())
        {
          SquadListItem squadListItem = eventData.pointerDrag.GetComponent<SquadListItem>();
          // create a squad
          GameObject newSquad = Instantiate(SquadListItemO, new Vector3(0,0,0), new Quaternion(0,0,0,0));
          newSquad.GetComponent<SquadListItem>().SetSquadLeader(vehicleReference);

          newSquad.GetComponent<SquadListItem>().AddSquadMember(squadListItem.squadLeaderVehicle);
          foreach(VehicleBase veh in squadListItem.squadMembersList)
          {
            newSquad.GetComponent<SquadListItem>().AddSquadMember(veh);
          }

          Destroy(eventData.pointerDrag.gameObject);
          Destroy(gameObject);
        }
        else if(eventData.pointerDrag.GetComponent<SquadMemberS>())
        {
          // Debug.Log("VehicleListItemS pointerDrag called on drop");
          SquadMemberS s = eventData.pointerDrag.GetComponent<SquadMemberS>();
          // create a squad
          GameObject newSquad = Instantiate(SquadListItemO, new Vector3(0,0,0), new Quaternion(0,0,0,0));
          // remove this squad member from the current squad
          s.vehicleReference.ui_element.GetComponent<SquadMemberS>().squadListItem.squadMembersList.Remove(s.vehicleReference);

          newSquad.GetComponent<SquadListItem>().SetSquadLeader(vehicleReference);
          newSquad.GetComponent<SquadListItem>().AddSquadMember(s.vehicleReference);

          Destroy(eventData.pointerDrag.gameObject);
          Destroy(gameObject);
        }
      }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
      // Debug.Log("OnPointerDown test");
    }

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
