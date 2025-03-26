using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwicher : MonoBehaviour
{
    public MovementController movementController;
    public bool IsInFPS = false;

    public GameObject FPScam, TPScam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementController.IsInFPS = IsInFPS;
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleCameraChagne();
        }
        if (IsInFPS)
        {
            FPScam.SetActive(true);
            TPScam.SetActive(false);
        }
        else
        {
            FPScam.SetActive(false);
            TPScam.SetActive(true);
        }
    }
    void ToggleCameraChagne()
    {
        if (IsInFPS == true)
        {
            IsInFPS = false;
        }
        else
        {
            IsInFPS = true;
        }
    }
}
