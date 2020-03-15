using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SquadMemberS : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private bool isHandlingClick = false;
    public SquadListItem squadListItem;
    private float buttonClickTime = 0.0f;
    public VehicleBase vehicleReference;
    public Text SquadMemberName;
    // Start is called before the first frame update
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Transform originalParent;
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
    public void OnDrag(PointerEventData eventData)
    {
      // Debug.Log("OnDrag test");
      rectTransform.anchoredPosition += eventData.delta;
      canvasGroup.alpha = 0.6f;
      canvasGroup.blocksRaycasts = false;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
      originalParent = transform.parent;
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

      // remove from squad vehicle list
      squadListItem.squadMembersList.Remove(vehicleReference);

      PlayerController playerController = FindObjectOfType<PlayerController>();
      playerController.AddSingleVehicleToUI(vehicleReference);

      // destroy the object
      Destroy(gameObject);
    }
    public void OnDrop(PointerEventData eventData)
    {
    }
}
