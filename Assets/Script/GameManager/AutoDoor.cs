using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public float PlayerCheckerRadius = 1.2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (IsplayerClosed())
        {
            Opendoor();
        }
        else 
        {
            Closedoor();   
        }
    }
    public void Opendoor()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    public void Closedoor()
    {

    }
    public bool IsplayerClosed()
    {
        return CheckProximity("Player");
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


