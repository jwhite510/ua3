using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MiniMapMouseDown(BaseEventData baseData)
    {
      PointerEventData pointerEventData = baseData as PointerEventData;


      if(pointerEventData.button == PointerEventData.InputButton.Left)
      {
        Debug.Log("left mouse click");
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
