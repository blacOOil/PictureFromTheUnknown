using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskTarget : MonoBehaviour
{
    public string Targetname,taskname;
    public TextMeshProUGUI tasknameText;


    void Update()
    {
    tasknameText.text = taskname; 
    }
}
