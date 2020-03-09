using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VehicleListItemS : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{


    public Text buttonText;
    public VehicleBase vehicleReference;
    public PlayerController playerController;

    private bool isHandlingClick = false;
    private float buttonClickTime = 0.0f;
    private RectTransform rectTransform;

    private void Awake()
    {
      rectTransform = GetComponent<RectTransform>();
    }


    public void OnDrag(PointerEventData eventData)
    {
      Debug.Log("OnDrag test");
      rectTransform.anchoredPosition += eventData.delta;

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
    }
    public void OnPointerDown(PointerEventData eventData)
    {
      Debug.Log("OnPointerDown test");
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
