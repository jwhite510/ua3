using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMap : MonoBehaviour
{



    private bool isDraggingMap;
    public GameObject miniMapCamera;
    public GameObject minimapImage;
    private RectTransform rectTransform;
    private Vector2 mapMousePosition;
    private bool handling_mouseclick = false;
    private float mouseDownTime = 0;
    private bool isMouseDragging = false;
    private Vector3 cameraOriginalLocation;
    // Start is called before the first frame update
    void Start()
    {
      rectTransform = minimapImage.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
      // miniMapCamera.transform.position+= new Vector3(0,0,30);
      if(handling_mouseclick)
      {
        if((Time.time - mouseDownTime) > 0.2)
        {
          // mouse is dragging
          // Debug.Log("mouse is dragging");
          isMouseDragging = true;
          HandleMouseDrag();
        }
      }
    }

    public void MiniMapMouseDown(BaseEventData baseData)
    {
      PointerEventData pointerEventData = baseData as PointerEventData;


      if(pointerEventData.button == PointerEventData.InputButton.Left)
      {
        // LEFT mouse down
        mapMousePosition = GetMousePosition();
        cameraOriginalLocation = miniMapCamera.transform.position;
        mouseDownTime = Time.time;
        handling_mouseclick = true;
      }
      else
      {
        // RIGHT mouse down
        Debug.Log("right mouse click");
      }
    }


    public void MiniMapMouseUp()
    {
      Debug.Log("mouse up");
      if(isMouseDragging)
      {
        // Debug.Log("handled isMouseDragging true");
      }
      else
      {
        HandleSingleMouseClick();
      }
      handling_mouseclick = false;
      isMouseDragging = false;
    }

    private Vector2 GetMousePosition()
    {

      Vector3[] corners = new Vector3[4];
      rectTransform.GetWorldCorners(corners);

      Vector2 top_left = new Vector2(0,0);
      top_left[0] = corners[1][0];
      top_left[1] = corners[1][1];
      float height = corners[1][1] - corners[0][1];
      float width = corners[2][0] - corners[1][0];

      Vector2 mapPosition = new Vector2(0,0);
      mapPosition[0] = Input.mousePosition[0] - top_left[0];
      mapPosition[1] = -1*(Input.mousePosition[1] - top_left[1]);
      mapPosition[0] /= width;
      mapPosition[1] /= height;

      // center at 0
      mapPosition[0] -= 0.5f;
      mapPosition[1] -= 0.5f;

      // Debug.Log("mapPosition => "+mapPosition);
      return mapPosition;
    }

    private void HandleSingleMouseClick()
    {
      Debug.Log("HandleSingleMouseClick called");
      Debug.Log("mapMousePosition => "+mapMousePosition);
    }
    private void HandleMouseDrag()
    {
      Debug.Log("HandleMouseDrag called");
      // mapMousePosition
      Vector2 mouseDeltaPosition = new Vector2(0,0);
      mouseDeltaPosition = GetMousePosition() - mapMousePosition;

      // miniMapCamera.transform.position+= new Vector3(-mouseDeltaPosition[0],0,mouseDeltaPosition[1]);
      miniMapCamera.transform.position = cameraOriginalLocation + 20*(new Vector3(-mouseDeltaPosition[0],0,mouseDeltaPosition[1]));

    }
}
