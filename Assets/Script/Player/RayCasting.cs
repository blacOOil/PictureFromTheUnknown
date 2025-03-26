using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    public float ScoreGeted;
    public Transform cam;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartCasting()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.position,cam.forward,out hit, range))
        {
            if(hit.collider.GetComponent<Collider>() != null)
            {
            //    if (hit.collider.GetComponent<Collider>().GivePoint(Point, hit.point, hit.normal))
                {

                }
            }
        }
    }
}
