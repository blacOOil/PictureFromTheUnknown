using Unity.VisualScripting;
using UnityEngine;

public class EventTriggerer : MonoBehaviour
{
    public GameObject SceneEvent;
    public int StateforTrigNum,NextStateNum;
    public float PlayerCheckerRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsplayerClose())
        {
            if(SceneEvent.GetComponent<Scene1_Event>().GameStateNum == StateforTrigNum)
            {
                SceneEvent.GetComponent<Scene1_Event>().GameStateNum = NextStateNum;
            }
        }
        }
    public bool IsplayerClose()
    {
        return CheckProximity("Player");
    }
    private bool CheckProximity(string tag)
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
