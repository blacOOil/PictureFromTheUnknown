using UnityEngine;


public class RedRoomControll : MonoBehaviour
{
    public bool IsredroomActived = false;
    public float PlayerCheckerRadius;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckProximity("Player"))
        {
            IsredroomActived=true;
        }
        else
        {
            IsredroomActived=false;
        }
        if (IsredroomActived)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed!");
            }
        }

        
    }
    private bool CheckProximity(string tag)
    {
        // Check all layers to capture all objects tagged with the given tag
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(tag))
            {

                return true;
            }
        }
        return false;
    }
}
