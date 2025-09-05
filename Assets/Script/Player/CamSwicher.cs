using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwicher : MonoBehaviour
{
    public GameObject FPScam, TPScam;
    public List<GameObject> Usable_Camera;
    public int Camera_Index;
    // Start is called before the first frame update
    void Start()
    {
        Camera_Index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Camera_Index++;
            
             if(Camera_Index + 1 > Usable_Camera.Count)
            {
                Camera_Index = 0;

            }
            ToggleCameraChagne();
        }
    }
    void ToggleCameraChagne()
    {

        for (int i = 0; i < Usable_Camera.Count; i++)
        {
            bool isActive = (i == Camera_Index);
            Usable_Camera[i].SetActive(isActive);
        }
    }
}
