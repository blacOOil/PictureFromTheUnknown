using UnityEditor.Rendering.LookDev;
using UnityEngine;
using System.Collections;
using System;

public class ScriptedTeleportation : MonoBehaviour
{
    public bool isTrigged = true;
    public GameObject Player;
    public Transform TargetTeleport;
    public float PlayerCheckerRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigged)
        {
             if (CheckProximity("Player"))
                {
                Debug.Log("Hit");
                StartCoroutine(TeleportWithDelay());
                }
        }
    }
    private IEnumerator TeleportWithDelay()
    {
        Player.transform.position = TargetTeleport.position;
        // Optional: double check player reached the target
        yield return new WaitForSeconds(1f); // Wait for 1 second
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
