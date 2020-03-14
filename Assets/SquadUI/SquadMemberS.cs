using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadMemberS : MonoBehaviour
{

    private Text SquadMemberName;
    // Start is called before the first frame update
    void Start()
    {
      // Debug.Log("GetComponent<Text> called");
      // SquadMemberName = GetComponent<Text>();
      // SquadMemberName.text = "text 123";
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetName(string name)
    {
      SquadMemberName = GetComponent<Text>();
      SquadMemberName.text = name;
    }
}
