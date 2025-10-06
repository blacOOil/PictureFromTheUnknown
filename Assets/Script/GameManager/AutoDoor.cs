using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public float PlayerCheckerRadius = 1.2f;
    public Transform DoorController;

    public float openY = -90f; // Y rotation when open
    public float closeY = 0f;  // Y rotation when closed
    public float speed = 50f;  // Degrees per second

    private bool rotating = false;
    private float targetY;

    void Update()
    {
        if (IsPlayerClose())
        {
            targetY = openY;
        }
        else
        {
            targetY = closeY;
        }

        // Rotate toward targetY
        float newY = Mathf.MoveTowardsAngle(DoorController.eulerAngles.y, targetY, speed * Time.deltaTime);
        DoorController.eulerAngles = new Vector3(DoorController.eulerAngles.x, newY, DoorController.eulerAngles.z);

        // Optional: Disable collider when open
        if (Mathf.Abs(Mathf.DeltaAngle(DoorController.eulerAngles.y, openY)) < 1f)
        {
           
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
          
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    bool IsPlayerClose()
    {
        return CheckProximity("Player");
    }

    bool CheckProximity(string tag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(tag))
                return true;
        }
        return false;
    }
}
