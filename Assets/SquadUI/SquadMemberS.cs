using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadMemberS : MonoBehaviour
{

    public Text SquadMemberName;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetName(string name)
    {
      Debug.Log("setting name to "+name);
      // SquadMemberName = GetComponent<Text>();
      SquadMemberName.text = name;
    }
}
