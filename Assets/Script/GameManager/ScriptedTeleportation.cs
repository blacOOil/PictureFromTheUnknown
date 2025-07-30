using UnityEditor.Rendering.LookDev;
using UnityEngine;
using System.Collections;

public class ScriptedTeleportation : MonoBehaviour
{
    public bool isTrigged = false;
    public GameObject Player;
    public Transform TargetTeleport;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.Find("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigged)
        {
            StartCoroutine(TeleportWithDelay());
        }
    }
    private IEnumerator TeleportWithDelay()
    {

        Player.transform.position = TargetTeleport.position;

        // Optional: double check player reached the target
        yield return new WaitForSeconds(1f); // Wait for 1 second

        isTrigged = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigged =true ;
        }
    }

}
