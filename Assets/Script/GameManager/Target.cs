using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public string Targetname;
    public string TargetType;
    public float Score;
   
  
    private void Update()
    {
        if(TargetType == "red")
        {
        Score = 10;
        }
        if(TargetType == "yellow")
        {
        Score = 100;
        }
        if(TargetType == "green")
        {
        Score = 1000;
        }
    }
}
