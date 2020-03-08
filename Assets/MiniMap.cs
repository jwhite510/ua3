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
    // Start is called before the first frame update
    void Start()
    {
      rectTransform = minimapImage.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
      // miniMapCamera.transform.position+= new Vector3(0,0,30);
    }

    public void MiniMapMouseDown(BaseEventData baseData)
    {
      PointerEventData pointerEventData = baseData as PointerEventData;


      if(pointerEventData.button == PointerEventData.InputButton.Left)
      {
        Debug.Log("left mouse click");
        Vector2 mapMousePosition = GetMousePosition();
        Debug.Log("mapMousePosition => "+mapMousePosition);
      }
      else
      {
        Debug.Log("right mouse click");
      }
    }


    public void MiniMapMouseUp()
    {
      Debug.Log("mouse up");
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
}
