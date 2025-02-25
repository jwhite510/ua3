﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class MiniMap : MonoBehaviour
{



    private bool isDraggingMap;
    public GameObject miniMapCamera;
    public GameObject minimapImage;
    public PlayerController playerController;
    private RectTransform rectTransform;
    private Vector2 mapMousePosition;
    private bool handling_mouseclick = false;
    private float mouseDownTime = 0;
    private bool isMouseDragging = false;
    private Vector3 cameraOriginalLocation;
    private Vector3 spherePosition = new Vector3(0,0,0);
    private Vector3 rayCastPosition = new Vector3(0, 0, 0);
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
        mapMousePosition = GetMousePosition();
        HandleSingleMouseClick(1);
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
        HandleSingleMouseClick(0);
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

    private void HandleSingleMouseClick(int mouseButton)
    {
      RaycastHit hit;
      if(ProjectMouseToMiniMapWorldLocation(mapMousePosition, out hit))
      {
        spherePosition = hit.point;


        if(mouseButton == 0){
          // LEFT CLICK
          VehicleBase vehicleclicked = hit.transform.gameObject.GetComponentInParent<VehicleBase>();
          if(vehicleclicked)
          {
            playerController.SelectVehicle(vehicleclicked);
          }
        }
        else if(mouseButton == 1)
        {
          // RIGHT CLICK
          playerController.MoveOrderSelectedVehicle(hit.point);

        }




      }
    }
    private void HandleMouseDrag()
    {
      // Debug.Log("HandleMouseDrag called");
      // mapMousePosition
      Vector2 mouseDeltaPosition = new Vector2(0,0);
      mouseDeltaPosition = GetMousePosition() - mapMousePosition;

      // miniMapCamera.transform.position+= new Vector3(-mouseDeltaPosition[0],0,mouseDeltaPosition[1]);

      Vector3 deltaCameraPosition = new Vector3(-mouseDeltaPosition[0],0,mouseDeltaPosition[1]);
      deltaCameraPosition *= miniMapCamera.GetComponent<Camera>().orthographicSize;

      miniMapCamera.transform.position = cameraOriginalLocation + 5*deltaCameraPosition;

    }
    public void MiniMapScroll(BaseEventData baseData)
    {
      // Debug.Log("MiniMouseScroll");
      PointerEventData pointerEventData = baseData as PointerEventData;
      Vector2 scrollDelta = pointerEventData.scrollDelta;
      Debug.Log("scrollDelta => "+scrollDelta);
      miniMapCamera.transform.position += new Vector3(0,0,0);
      miniMapCamera.GetComponent<Camera>().orthographicSize-=scrollDelta[1];
    }
    private bool ProjectMouseToMiniMapWorldLocation(Vector2 mapLocation, out RaycastHit hit)
    {
      // get position of camera
      rayCastPosition = miniMapCamera.transform.position;
      rayCastPosition[0] += 2*mapLocation[0] * miniMapCamera.GetComponent<Camera>().orthographicSize;
      rayCastPosition[2] -= 2*mapLocation[1] * miniMapCamera.GetComponent<Camera>().orthographicSize;

      Ray ray = new Ray(rayCastPosition, new Vector3(0,-1,0));

      bool HitSomething = Physics.Raycast(ray, out hit);

      if(HitSomething)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    void OnDrawGizmos()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawSphere(spherePosition, 0.2f);
    }
}
