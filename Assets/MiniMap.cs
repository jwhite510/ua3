using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMap : MonoBehaviour
{



    private bool isDraggingMap;
    public GameObject miniMapCamera;
    public GameObject minimapImage;
    // Start is called before the first frame update
    void Start()
    {
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
        // Debug.Log("left mouse click");
        // translate to world coordinates

        RectTransform rectTransform = minimapImage.GetComponent<RectTransform>();

        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        // foreach(Vector3 vec3 in corners)
        // {
          // Debug.Log("vec3 => "+vec3.ToString());
        // }

        Vector2 top_left = new Vector2(0,0);
        top_left[0] = corners[1][0];
        top_left[1] = corners[1][1];
        float height = corners[1][1] - corners[0][1];
        float width = corners[2][0] - corners[1][0];

        // Debug.Log("top_left.ToString() => "+top_left.ToString());
        // Debug.Log("height => "+height);
        // Debug.Log("width => "+width);
        // Debug.Log("pointerEventData.position => "+pointerEventData.position);

        Vector2 mapPosition = new Vector2(0,0);
        mapPosition[0] = pointerEventData.position[0] - top_left[0];
        mapPosition[1] = -1*(pointerEventData.position[1] - top_left[1]);
        mapPosition[0] /= width;
        mapPosition[1] /= height;

        // center at 0
        mapPosition[0] -= 0.5f;
        mapPosition[1] -= 0.5f;

        Debug.Log("mapPosition => "+mapPosition);




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
}
