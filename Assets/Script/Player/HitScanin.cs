using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanin : MonoBehaviour
{
    public List<string> TargetTage;
    public List<GameObject> ObjthatCamhit;
    public float CheckingRadius;
    public CamSwicher camSwicher;
    public CameraController cameraController;
    public GameManager gameManager;

    private bool CheckProximity(string tag)
    {
        // Check all layers to capture all objects tagged with the given tag
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CheckingRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((camSwicher.Camera_Index == 1) && (Input.GetMouseButtonDown(0)))
        {
            
            if (CheckProximity("DefaultTarget"))
            {
                Debug.Log("wewr");
                gameManager.TaskListCleared++;
            }
        }
    }

}
